namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public unsafe partial struct xmp_frame_info
{
    public int pos;

    public int pattern;

    public int row;

    public int num_rows;

    public int frame;

    public int speed;

    public int bpm;

    public int time;

    public int total_time;

    public int frame_time;

    public void* buffer;

    public int buffer_size;

    public int total_size;

    public int volume;

    public int loop_count;

    public int virt_channels;

    public int virt_used;

    public int sequence;

    [NativeTypeName("struct xmp_channel_info[64]")]
    public _channel_info_e__FixedBuffer channel_info;

    [InlineArray(64)]
    public partial struct _channel_info_e__FixedBuffer
    {
        public xmp_channel_info e0;
    }
}
