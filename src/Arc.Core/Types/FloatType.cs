using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Arc.Core.Interfaces;

namespace Arc.Core.Types
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FloatType : IType<float>
    {
        public static readonly FloatType Instance = new();

        #region Constructors

        private FloatType()
        {
        }

        #endregion

        #region Implementation of IType<double>

        public float Get(byte[] buffer)
        {
            return Unsafe.ReadUnaligned<float>(ref buffer[0]);
        }

        public void Set(byte[] buffer, float value)
        {
            Unsafe.As<byte, float>(ref buffer[0]) = value;
        }

        public int Size()
        {
            return sizeof(float);
        }

        #endregion
    }
}