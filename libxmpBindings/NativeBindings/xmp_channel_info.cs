namespace libxmpBindings.NativeBindings;

public partial struct xmp_channel_info
{
    [NativeTypeName("unsigned int")]
    public uint period;

    [NativeTypeName("unsigned int")]
    public uint position;

    public short pitchbend;

    [NativeTypeName("unsigned char")]
    public byte note;

    [NativeTypeName("unsigned char")]
    public byte instrument;

    [NativeTypeName("unsigned char")]
    public byte sample;

    [NativeTypeName("unsigned char")]
    public byte volume;

    [NativeTypeName("unsigned char")]
    public byte pan;

    [NativeTypeName("unsigned char")]
    public byte reserved;

    [NativeTypeName("struct xmp_event")]
    public xmp_event @event;
}
