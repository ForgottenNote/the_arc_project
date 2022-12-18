using System.Text;
using System.Text.Json.Serialization;
using Arc.Core;
using Arc.Core.Extensions;
using Arc.Core.Types;
using Arc.Driver.Interfaces;
using Arc.Game.Apex.Core.Interfaces;

namespace Arc.Game.Apex.Core.Models
{
    public class LevelName : IUpdatable
    {
        private readonly Access<string> _value;

        #region Constructors

        public LevelName(IDriver driver, IOffsets offsets, ulong address)
        {
            _value = driver.Access(address + offsets.CoreLevelName, new StringType(32, Encoding.ASCII), 5000);
        }

        #endregion

        #region Properties

        [JsonPropertyName("value")]
        public string Value => _value.Get();

        #endregion

        #region Implementation of IUpdatable

        public void Update(DateTime frameTime)
        {
            _value.Update(frameTime);
        }

        #endregion
    }
}