﻿namespace Arc.Game.Apex.Core.Interfaces
{
    public interface IOffsets
    {
        #region Properties

        uint ButtonInAttack { get; }
        uint ButtonInSpeed { get; }
        uint ButtonInZoom { get; }
        uint CoreEntityList { get; }
        uint CoreLevelName { get; }
        uint CoreLocalPlayer { get; }
        uint EntityLastVisibleTime { get; }
        uint EntityLocalOrigin { get; }
        uint EntitySignifierName { get; }
        uint PlayerBleedoutState { get; }
        uint PlayerDuckState { get; }
        uint PlayerGlowEnable { get; }
        uint PlayerGlowThroughWall { get; }
        uint PlayerGlowColor { get; }
        uint PlayerLifeState { get; }
        uint PlayerName { get; }
        uint PlayerHealth { get; }
        uint PlayerShields { get; }
        uint PlayerShieldsMax { get; }
        uint PlayerTeamNum { get; }
        uint SelectedWeapons { get; }
        uint PlayerVecPunchWeaponAngle { get; }
        uint PlayerViewAngle { get; }

        #endregion
    }
}