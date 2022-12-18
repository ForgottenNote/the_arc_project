using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Arc.Core.Extensions;

namespace Arc.Game.Apex.Feature.Sense
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

        [JsonPropertyName("distance")]
        public int Distance => _config.GetProperty<int>();

        [JsonPropertyName("glowColorRHidden")]
        public int GlowColorRHidden => _config.GetProperty<int>();

        [JsonPropertyName("glowColorGHidden")]
        public int GlowColorGHidden => _config.GetProperty<int>();

        [JsonPropertyName("glowColorBHidden")]
        public int GlowColorBHidden => _config.GetProperty<int>();

        [JsonPropertyName("glowColorRVisible")]
        public int GlowColorRVisible => _config.GetProperty<int>();

        [JsonPropertyName("glowColorGVisible")]
        public int GlowColorGVisible => _config.GetProperty<int>();

        [JsonPropertyName("glowColorBVisible")]
        public int GlowColorBVisible => _config.GetProperty<int>();

        [JsonPropertyName("healthBasedGlow")]
        public bool HealthBasedGlow => _config.GetProperty<bool>();

        #endregion
    }
}