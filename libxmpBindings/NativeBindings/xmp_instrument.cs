namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public unsafe partial struct xmp_instrument
{
    [NativeTypeName("char[32]")]
    public _name_e__FixedBuffer name;

    public int vol;

    public int nsm;

    public int rls;

    [NativeTypeName("struct xmp_envelope")]
    public xmp_envelope aei;

    [NativeTypeName("struct xmp_envelope")]
    public xmp_envelope pei;

    [NativeTypeName("struct xmp_envelope")]
    public xmp_envelope fei;

    [NativeTypeName("struct (anonymous struct)")]
    public _map_e__FixedBuffer map;

    [NativeTypeName("struct xmp_subinstrument *")]
    public xmp_subinstrument* sub;

    public void* extra;

    public partial struct _Anonymous_e__Struct
    {
        [NativeTypeName("unsigned char")]
        public byte ins;

        [NativeTypeName("signed char")]
        public sbyte xpo;
    }

    [InlineArray(32)]
    public partial struct _name_e__FixedBuffer
    {
        public sbyte e0;
    }

    [InlineArray(121)]
    public partial struct _map_e__FixedBuffer
    {
        public _Anonymous_e__Struct e0;
    }
}
