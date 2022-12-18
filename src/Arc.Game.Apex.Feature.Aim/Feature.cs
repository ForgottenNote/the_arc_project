using System.Diagnostics.CodeAnalysis;
using Arc.Core.Models;
using Arc.Game.Apex.Core;
using Arc.Game.Apex.Core.Interfaces;
using Arc.Game.Apex.Core.Models;
using Arc.Game.Apex.Feature.Aim.Enums;
using Arc.Game.Apex.Feature.Aim.Extensions;
using Arc.Game.Apex.Feature.Aim.Interfaces;
using Arc.Game.Apex.Feature.Aim.Utilities;

namespace Arc.Game.Apex.Feature.Aim
{
    public class Feature : IFeature
    {
        private readonly Config _config;
        private ITarget? _target;
        private long _targetLockTicks;
        private Vector? _targetPreviousOrigin;
        private long _targetPreviousTicks;
        private Vector? _targetPreviousVecPunchWeaponAngle;

        #region Constructors

        public Feature(Config config)
        {
            _config = config;
            StateExtensions.AdsBot = _config.AdsBot;
            StateExtensions.ExperimentalFeatures = _config.ExperimentalFeatures;
        }

        #endregion

        #region Methods

        private ITarget? Find(State state, Player localPlayer, bool scanSameTeam)
        {
            var bestEnemyScore = float.MaxValue;
            var bestEnemyTarget = null as ITarget;
            var bestTeamScore = float.MaxValue;
            var bestTeamTarget = null as ITarget;

            foreach (var target in state.IterateTargets().Where(x => x.IsValid(localPlayer) && x.Visible))
            {
                // Calculate the distance.
                var distance = localPlayer.LocalOrigin.Distance2(target.LocalOrigin) * Constants.UnitToMeter;
                if (distance >= _config.MaxDistance) continue;

                // Calculate the view angle delta.
                var desiredAngle = AdjustSelf(localPlayer).GetDesiredAngle(AdjustTarget(target));
                var deltaX = MathF.Abs(localPlayer.ViewAngle.X - desiredAngle.X);
                var deltaY = MathF.Abs(localPlayer.ViewAngle.Y - desiredAngle.Y);
                if (deltaX >= _config.PitchAngle || deltaY >= _config.YawAngle) continue;
                var targetScore = deltaX + deltaY + (target.BleedoutState != 0 ? 1000 : 0);

                // Prioritize the target.
                if (target.IsSameTeam(localPlayer))
                {
                    if (!scanSameTeam || targetScore >= bestTeamScore) continue;
                    bestTeamScore = targetScore;
                    bestTeamTarget = target;
                }
                else if (targetScore < bestEnemyScore)
                {
                    bestEnemyScore = targetScore;
                    bestEnemyTarget = target;
                }
            }

            return bestEnemyTarget ?? bestTeamTarget;
        }

        private float GetPercentage(DateTime frameTime)
        {
            var ticks = (float)(frameTime.Ticks - _targetLockTicks);
            var percentage = ticks / TimeSpan.TicksPerMillisecond / _config.LockTime;
            return MathF.Min(percentage, 1);
        }

        private Vector Target(DateTime frameTime, Player localPlayer, ITarget target)
        {
            var correctAngle = AdjustSelf(localPlayer).GetDesiredAngle(AdjustTarget(target));
            var currentAngle = localPlayer.ViewAngle + (_targetPreviousVecPunchWeaponAngle ?? Vector.Origin);
            var distance = localPlayer.LocalOrigin.Distance2(target.LocalOrigin) * Constants.UnitToMeter;

            if(distance <= _config.MinDistance) 
                return localPlayer.VecPunchWeaponAngle;
            
            var deadzoneAngle = new Deadzone(correctAngle, _config.PitchDeadzone / distance, _config.YawDeadzone / distance).ToVector(currentAngle);
            var smoothAngle = _config.GetSmoothAngle(currentAngle, deadzoneAngle, GetPercentage(frameTime));

            if(currentAngle.Distance2(correctAngle) > _config.ExitZoneFov)
                return localPlayer.VecPunchWeaponAngle;

            if(localPlayer.VecPunchWeaponAngle.X <= _config.MaxRecoilPunch && _config.ExperimentalFeatures) {
                localPlayer.ViewAngle = smoothAngle;
                return Vector.Origin;
            } else {
                localPlayer.ViewAngle = smoothAngle - localPlayer.VecPunchWeaponAngle * _config.Recoil;
                return localPlayer.VecPunchWeaponAngle;
            }
        }

        #endregion

        #region Statics

        private static Vector AdjustSelf(Player localPlayer)
        {
            return localPlayer.DuckState != 0
                ? new Vector(localPlayer.LocalOrigin.X, localPlayer.LocalOrigin.Y, localPlayer.LocalOrigin.Z - 27)
                : new Vector(localPlayer.LocalOrigin.X, localPlayer.LocalOrigin.Y, localPlayer.LocalOrigin.Z);
        }

        private static Vector AdjustTarget(ITarget target)
        {
            return target.DuckState != 0 || target.BleedoutState != 0
                ? new Vector(target.LocalOrigin.X, target.LocalOrigin.Y, target.LocalOrigin.Z - 18)
                : new Vector(target.LocalOrigin.X, target.LocalOrigin.Y, target.LocalOrigin.Z - 10);
        }

        private static bool Validate(Player localPlayer, ITarget target, Vector targetPreviousOrigin)
        {
            return target.IsValid(localPlayer)
                   && target.Visible
                   && target.LocalOrigin.Distance2(targetPreviousOrigin) * Constants.UnitToMeter < 5;
        }

        #endregion

        #region Implementation of IFeature

        public void Tick(DateTime frameTime, State state)
        {
            if (state.Players.TryGetValue(state.LocalPlayer, out var localPlayer))
            {
                var targetType = state.GetTargetType(localPlayer);
                if (targetType == TargetType.None)
                {
                    _target = null;
                    _targetLockTicks = 0;
                    _targetPreviousOrigin = null;
                    _targetPreviousTicks = 0;
                    _targetPreviousVecPunchWeaponAngle = null;
                }
                else if (_target == null || _targetPreviousOrigin == null)
                {
                    var target = Find(state, localPlayer, targetType == TargetType.All);
                    if (target == null) return;
                    _target = target;
                    _targetLockTicks = frameTime.Ticks;
                    _targetPreviousOrigin = target.LocalOrigin;
                    _targetPreviousTicks = frameTime.Ticks;

                    if(localPlayer.VecPunchWeaponAngle.X <= _config.MaxRecoilPunch && _config.ExperimentalFeatures) {
                        _targetPreviousVecPunchWeaponAngle = Vector.Origin;
                    } else {
                        _targetPreviousVecPunchWeaponAngle = localPlayer.VecPunchWeaponAngle;
                    }
                }
                else if (!Validate(localPlayer, _target, _targetPreviousOrigin))
                {
                    if (_targetPreviousTicks + _config.ReleaseTime * TimeSpan.TicksPerMillisecond >= frameTime.Ticks) return;
                    _target = null;
                    _targetLockTicks = 0;
                    _targetPreviousOrigin = null;
                    _targetPreviousTicks = 0;
                    _targetPreviousVecPunchWeaponAngle = null;
                }
                else
                {
                    Vector oPunch = Target(frameTime, localPlayer, _target);
                    _targetPreviousOrigin = _target.LocalOrigin;
                    _targetPreviousTicks = frameTime.Ticks;
                    _targetPreviousVecPunchWeaponAngle = oPunch;
                }
            }
        }

        #endregion
    }
}