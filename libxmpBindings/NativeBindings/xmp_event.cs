namespace libxmpBindings.NativeBindings;

public partial struct xmp_event
{
    [NativeTypeName("unsigned char")]
    public byte note;

    [NativeTypeName("unsigned char")]
    public byte ins;

    [NativeTypeName("unsigned char")]
    public byte vol;

    [NativeTypeName("unsigned char")]
    public byte fxt;

    [NativeTypeName("unsigned char")]
    public byte fxp;

    [NativeTypeName("unsigned char")]
    public byte f2t;

    [NativeTypeName("unsigned char")]
    public byte f2p;

    [NativeTypeName("unsigned char")]
    public byte _flag;
}
