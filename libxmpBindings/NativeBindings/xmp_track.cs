namespace libxmpBindings.NativeBindings;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public partial struct xmp_track
{
    public int rows;

    [NativeTypeName("struct xmp_event[1]")]
    public _event_e__FixedBuffer @event;

    public partial struct _event_e__FixedBuffer
    {
        public xmp_event e0;

        [UnscopedRef]
        public ref xmp_event this[int index]
        {
            get
            {
                return ref Unsafe.Add(ref e0, index);
            }
        }

        [UnscopedRef]
        public Span<xmp_event> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
    }
}
