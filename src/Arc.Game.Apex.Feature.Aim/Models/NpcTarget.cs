using System.Text.Json.Serialization;
using Arc.Core.Models;
using Arc.Game.Apex.Core.Models;
using Arc.Game.Apex.Feature.Aim.Interfaces;

namespace Arc.Game.Apex.Feature.Aim.Models
{
    public class NpcTarget : ITarget
    {
        private readonly Npc _npc;

        #region Constructors

        public NpcTarget(Npc npc)
        {
            _npc = npc;
        }

        #endregion

        #region Implementation of ITarget

        public bool IsSameTeam(Player localPlayer)
        {
            return false;
        }

        public bool IsValid(Player localPlayer)
        {
            return true;
        }

        #endregion

        #region Implementation of ITarget Properties

        [JsonPropertyName("bleedoutState")]
        public byte BleedoutState => 0;

        [JsonPropertyName("duckState")]
        public byte DuckState => 0;

        [JsonPropertyName("localOrigin")]
        public Vector LocalOrigin => _npc.LocalOrigin;

        [JsonPropertyName("visible")]
        public bool Visible => _npc.Visible;

        #endregion
    }
}