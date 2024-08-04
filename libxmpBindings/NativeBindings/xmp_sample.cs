namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public unsafe partial struct xmp_sample
{
    [NativeTypeName("char[32]")]
    public _name_e__FixedBuffer name;

    public int len;

    public int lps;

    public int lpe;

    public int flg;

    [NativeTypeName("unsigned char *")]
    public byte* data;

    [InlineArray(32)]
    public partial struct _name_e__FixedBuffer
    {
        public sbyte e0;
    }
}
