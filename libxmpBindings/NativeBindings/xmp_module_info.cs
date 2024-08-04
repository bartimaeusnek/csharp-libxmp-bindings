namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public unsafe partial struct xmp_module_info
{
    public string MD5
    {
        get
        {
            fixed (byte* ptr = &md5.e0)
            {
                return new string((sbyte*)ptr);
            }
        }
    }

    [NativeTypeName("unsigned char[16]")]
    public _md5_e__FixedBuffer md5;

    public int vol_base;

    [NativeTypeName("struct xmp_module *")]
    public xmp_module* mod;

    [NativeTypeName("char *")]
    public sbyte* comment;

    public string Comment
    {
        get
        {
            return new string(comment);
        }
    }
    
    public int num_sequences;

    [NativeTypeName("struct xmp_sequence *")]
    public xmp_sequence* seq_data;

    [InlineArray(16)]
    public partial struct _md5_e__FixedBuffer
    {
        public byte e0;
    }
}
