namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public partial struct xmp_envelope
{
    public int flg;

    public int npt;

    public int scl;

    public int sus;

    public int sue;

    public int lps;

    public int lpe;

    [NativeTypeName("short[64]")]
    public _data_e__FixedBuffer data;

    [InlineArray(64)]
    public partial struct _data_e__FixedBuffer
    {
        public short e0;
    }
}
