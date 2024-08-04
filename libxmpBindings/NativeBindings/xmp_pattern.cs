namespace libxmpBindings.NativeBindings;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public partial struct xmp_pattern
{
    public int rows;

    [NativeTypeName("int[1]")]
    public _index_e__FixedBuffer index;

    public partial struct _index_e__FixedBuffer
    {
        public int e0;

        [UnscopedRef]
        public ref int this[int index]
        {
            get
            {
                return ref Unsafe.Add(ref e0, index);
            }
        }

        [UnscopedRef]
        public Span<int> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
    }
}
