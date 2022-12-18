using System.Text.Json.Serialization;
using Arc.Core;
using Arc.Core.Extensions;
using Arc.Core.Models;
using Arc.Core.Types;
using Arc.Driver.Interfaces;
using Arc.Game.Apex.Core.Interfaces;
using Arc.Game.Apex.Core.Utilities;

namespace Arc.Game.Apex.Core.Models
{
    public class Player : IUpdatable
    {
        private readonly Access<byte> _bleedoutState;
        private readonly Access<byte> _duckState;
        private readonly Access<byte> _glowEnable;
        private readonly Access<byte> _glowThroughWalls;
        private readonly Access<Vector> _glowColor;
        private readonly Access<float> _glowColorR;
        private readonly Access<float> _glowColorG;
        private readonly Access<float> _glowColorB;
        private readonly LastVisibleTime _lastVisibleTime;
        private readonly Access<byte> _lifeState;
        private readonly Access<Vector> _localOrigin;
        private readonly Access<ulong> _name;
        private readonly Access<uint> _health;
        private readonly Access<uint> _shields;
        private readonly Access<uint> _maxShields;
        private readonly Access<byte> _teamNum;
        private readonly Access<ulong> _selectedWeapons;
        private readonly Access<uint> _selectedWeapon;
        private readonly Access<Vector> _vecPunchWeaponAngle;
        private readonly Access<Vector> _viewAngle;

        #region Constructors

        public Player(IDriver driver, IOffsets offsets, ulong address)
        {
            _bleedoutState = driver.Access(address + offsets.PlayerBleedoutState, ByteType.Instance);
            _duckState = driver.Access(address + offsets.PlayerDuckState, ByteType.Instance);
            _glowEnable = driver.Access(address + offsets.PlayerGlowEnable, ByteType.Instance);
            _glowThroughWalls = driver.Access(address + offsets.PlayerGlowThroughWall, ByteType.Instance);
            _glowColor = driver.Access(address + offsets.PlayerGlowColor, VectorType.Instance);
            _glowColorR = driver.Access(address + offsets.PlayerGlowColor, FloatType.Instance);
            _glowColorG = driver.Access(address + offsets.PlayerGlowColor + 0x4, FloatType.Instance);
            _glowColorB = driver.Access(address + offsets.PlayerGlowColor + 0x8, FloatType.Instance);
            _lastVisibleTime = new LastVisibleTime(driver.Access(address + offsets.EntityLastVisibleTime, SingleType.Instance));
            _lifeState = driver.Access(address + offsets.PlayerLifeState, ByteType.Instance);
            _localOrigin = driver.Access(address + offsets.EntityLocalOrigin, VectorType.Instance);
            _name = driver.Access(address + offsets.PlayerName, UInt64Type.Instance);
            _health = driver.Access(address + offsets.PlayerHealth, UInt32Type.Instance);
            _shields = driver.Access(address + offsets.PlayerShields, UInt32Type.Instance);
            _maxShields = driver.Access(address + offsets.PlayerShieldsMax, UInt32Type.Instance);
            _teamNum = driver.Access(address + offsets.PlayerTeamNum, ByteType.Instance, 1000);
            _selectedWeapons = driver.Access(address + offsets.SelectedWeapons, UInt64Type.Instance);
            _selectedWeapon = driver.Access(address + SelectedWeapons + 0x166C, UInt32Type.Instance);
            _vecPunchWeaponAngle = driver.Access(address + offsets.PlayerVecPunchWeaponAngle, VectorType.Instance);
            _viewAngle = driver.Access(address + offsets.PlayerViewAngle, VectorType.Instance);
        }

        #endregion

        #region Methods

        public bool IsSameTeam(Player otherPlayer)
        {
            return TeamNum == otherPlayer.TeamNum;
        }

        public bool IsValid()
        {
            return GlowEnable != 0 && GlowEnable != 255 && LifeState == 0 && LocalOrigin != Vector.Origin && Name != 0;
        }

        #endregion

        #region Properties

        [JsonPropertyName("bleedoutState")]
        public byte BleedoutState
        {
            get => _bleedoutState.Get();
            set => _bleedoutState.Set(value);
        }

        [JsonPropertyName("duckState")]
        public byte DuckState
        {
            get => _duckState.Get();
            set => _duckState.Set(value);
        }

        [JsonPropertyName("glowEnable")]
        public byte GlowEnable
        {
            get => _glowEnable.Get();
            set => _glowEnable.Set(value);
        }

        [JsonPropertyName("glowThroughWalls")]
        public byte GlowThroughWalls
        {
            get => _glowThroughWalls.Get();
            set => _glowThroughWalls.Set(value);
        }

        [JsonPropertyName("glowColor")]
        public Vector GlowColor
        {
            get => _glowColor.Get();
            set => _glowColor.Set(value);
        }

        [JsonPropertyName("glowColorR")]
        public float GlowColorR
        {
            get => _glowColorR.Get();
            set => _glowColorR.Set(value);
        }

        [JsonPropertyName("glowColorG")]
        public float GlowColorG
        {
            get => _glowColorG.Get();
            set => _glowColorG.Set(value);
        }

        [JsonPropertyName("glowColorB")]
        public float GlowColorB
        {
            get => _glowColorB.Get();
            set => _glowColorB.Set(value);
        }

        [JsonPropertyName("lifeState")]
        public byte LifeState
        {
            get => _lifeState.Get();
            set => _lifeState.Set(value);
        }

        [JsonPropertyName("localOrigin")]
        public Vector LocalOrigin
        {
            get => _localOrigin.Get();
            set => _localOrigin.Set(value);
        }

        [JsonPropertyName("name")]
        public ulong Name
        {
            get => _name.Get();
            set => _name.Set(value);
        }

        [JsonPropertyName("health")]
        public uint Health
        {
            get => _health.Get();
            set => _health.Set(value);
        }

        [JsonPropertyName("shields")]
        public uint Shields
        {
            get => _shields.Get();
            set => _shields.Set(value);
        }

        [JsonPropertyName("maxShields")]
        public uint MaxShields
        {
            get => _health.Get();
            set => _health.Set(value);
        }

        [JsonPropertyName("teamNum")]
        public byte TeamNum
        {
            get => _teamNum.Get();
            set => _teamNum.Set(value);
        }

        [JsonPropertyName("selectedWeapons")]
        public ulong SelectedWeapons
        {
            get => _selectedWeapons.Get();
            set => _selectedWeapons.Set(value);
        }

        [JsonPropertyName("selectedWeapon")]
        public uint SelectedWeapon
        {
            get => _selectedWeapon.Get();
            set => _selectedWeapon.Set(value);
        }


        [JsonPropertyName("vecPunchWeaponAngle")]
        public Vector VecPunchWeaponAngle
        {
            get => _vecPunchWeaponAngle.Get();
            set => _vecPunchWeaponAngle.Set(value);
        }

        [JsonPropertyName("viewAngle")]
        public Vector ViewAngle
        {
            get => _viewAngle.Get();
            set => _viewAngle.Set(value);
        }

        [JsonPropertyName("visible")]
        public bool Visible => _lastVisibleTime.Visible;

        #endregion

        #region Implementation of IUpdatable

        public void Update(DateTime frameTime)
        {
            _bleedoutState.Update(frameTime);
            _duckState.Update(frameTime);
            _glowEnable.Update(frameTime);
            _glowThroughWalls.Update(frameTime);
            _glowColor.Update(frameTime);
            _glowColorR.Update(frameTime);
            _glowColorG.Update(frameTime);
            _glowColorB.Update(frameTime);
            _lastVisibleTime.Update(frameTime);
            _lifeState.Update(frameTime);
            _localOrigin.Update(frameTime);
            _name.Update(frameTime);
            _health.Update(frameTime);
            _shields.Update(frameTime);
            _maxShields.Update(frameTime);
            _teamNum.Update(frameTime);
            _selectedWeapons.Update(frameTime);
            _selectedWeapon.Update(frameTime);
            _vecPunchWeaponAngle.Update(frameTime);
            _viewAngle.Update(frameTime);
        }

        #endregion
    }
}