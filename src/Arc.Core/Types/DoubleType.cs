using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Arc.Core.Interfaces;

namespace Arc.Core.Types
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class DoubleType : IType<double>
    {
        public static readonly DoubleType Instance = new();

        #region Constructors

        private DoubleType()
        {
        }

        #endregion

        #region Implementation of IType<double>

        public double Get(byte[] buffer)
        {
            return Unsafe.ReadUnaligned<double>(ref buffer[0]);
        }

        public void Set(byte[] buffer, double value)
        {
            Unsafe.As<byte, double>(ref buffer[0]) = value;
        }

        public int Size()
        {
            return sizeof(double);
        }

        #endregion
    }
}