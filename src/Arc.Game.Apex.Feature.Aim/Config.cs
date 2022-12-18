using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Arc.Core.Extensions;

namespace Arc.Game.Apex.Feature.Aim
{
    public class Config
    {
        private readonly IConfigurationSection _config;

        #region Constructors

        public Config(IConfiguration config)
        {
            _config = config.GetSection(typeof(Config).Namespace);
        }

        #endregion

        #region Properties

        [JsonPropertyName("minDistance")]
        public int MinDistance => _config.GetProperty<int>();

        [JsonPropertyName("maxDistance")]
        public int MaxDistance => _config.GetProperty<int>();

        [JsonPropertyName("lockTime")]
        public int LockTime => _config.GetProperty<int>();

        [JsonPropertyName("exitZoneFov")]
        public int ExitZoneFov => _config.GetProperty<int>();

        [JsonPropertyName("experimentalFeatures")]
        public bool ExperimentalFeatures => _config.GetProperty<bool>();

        [JsonPropertyName("maxRecoilPunch")]
        public float MaxRecoilPunch => _config.GetProperty<float>();

        [JsonPropertyName("adsBot")]
        public bool AdsBot => _config.GetProperty<bool>();

        [JsonPropertyName("pitchAngle")]
        public int PitchAngle => _config.GetProperty<int>();

        [JsonPropertyName("pitchDeadzone")]
        public float PitchDeadzone => _config.GetProperty<float>();

        [JsonPropertyName("pitchSpeed")]
        public float PitchSpeed => _config.GetProperty<float>();

        [JsonPropertyName("recoil")]
        public float Recoil => _config.GetProperty<float>();

        [JsonPropertyName("releaseTime")]
        public int ReleaseTime => _config.GetProperty<int>();

        [JsonPropertyName("yawAngle")]
        public int YawAngle => _config.GetProperty<int>();

        [JsonPropertyName("yawDeadzone")]
        public float YawDeadzone => _config.GetProperty<float>();

        [JsonPropertyName("yawSpeed")]
        public float YawSpeed => _config.GetProperty<float>();

        #endregion
    }
}