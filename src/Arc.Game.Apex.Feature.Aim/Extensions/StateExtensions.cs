using Arc.Game.Apex.Core;
using Arc.Game.Apex.Core.Models;
using Arc.Game.Apex.Feature.Aim.Enums;
using Arc.Game.Apex.Feature.Aim.Interfaces;
using Arc.Game.Apex.Feature.Aim.Models;

namespace Arc.Game.Apex.Feature.Aim.Extensions
{
    public static class StateExtensions
    {
        public static bool AdsBot = false;
        public static bool ExperimentalFeatures = false;

        #region Statics

        public static TargetType GetTargetType(this State state, Player localPlayer)
        {
            if (localPlayer.BleedoutState != 0)
            {
                return TargetType.None;
            }

            if ((AdsBot && ExperimentalFeatures) ? (state.Buttons.InZoom != 0) : (state.Buttons.InAttack != 0) && (state.Buttons.InZoom != 0 || localPlayer.VecPunchWeaponAngle.X != 0 || localPlayer.VecPunchWeaponAngle.Y != 0))
            {
                return state.Buttons.InSpeed != 0 ? TargetType.All : TargetType.Enemy;
            }

            if (state.Buttons.InSpeed != 0)
            {
                return TargetType.None;
            }

            return TargetType.None;
        }

        public static IEnumerable<ITarget> IterateTargets(this State state)
        {
            foreach (var (_, npc) in state.Npcs)
            {
                yield return new NpcTarget(npc);
            }

            foreach (var (_, player) in state.Players)
            {
                yield return new PlayerTarget(player);
            }
        }

        #endregion
    }
}