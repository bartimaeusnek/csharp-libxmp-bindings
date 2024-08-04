namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;
/**
 * struct xmp_module {
 *     char name[XMP_NAME_SIZE];                Module title
 *     char type[XMP_NAME_SIZE];                Module format
 *     int pat;                                 Number of patterns
 *     int trk;                                 Number of tracks
 *     int chn;                                 Tracks per pattern
 *     int ins;                                 Number of instruments
 *     int smp;                                 Number of samples
 *     int spd;                                 Initial speed
 *     int bpm;                                 Initial BPM
 *     int len;                                 Module length in patterns
 *     int rst;                                 Restart position
 *     int gvl;                                 Global volume
 *     struct xmp_pattern **xxp;                Patterns
 *     struct xmp_track **xxt;                  Tracks
 *     struct xmp_instrument *xxi;              Instruments
 *     struct xmp_sample *xxs;                  Samples
 *     struct xmp_channel xxc[64];              Channel info
 *     unsigned char xxo[XMP_MAX_MOD_LENGTH];   Orders
 * };
 */
public unsafe partial struct xmp_module
{
    [NativeTypeName("char[64]")]
    public _name_e__FixedBuffer name;

    [NativeTypeName("char[64]")]
    public _type_e__FixedBuffer type;

    public int pat;

    public int trk;

    public int chn;

    public int ins;

    public int smp;

    public int spd;

    public int bpm;

    public int len;

    public int rst;

    public int gvl;

    [NativeTypeName("struct xmp_pattern **")]
    public xmp_pattern** xxp;

    [NativeTypeName("struct xmp_track **")]
    public xmp_track** xxt;

    [NativeTypeName("struct xmp_instrument *")]
    public xmp_instrument* xxi;

    [NativeTypeName("struct xmp_sample *")]
    public xmp_sample* xxs;

    [NativeTypeName("struct xmp_channel[64]")]
    public _xxc_e__FixedBuffer xxc;

    [NativeTypeName("unsigned char[256]")]
    public _xxo_e__FixedBuffer xxo;

    [InlineArray(64)]
    public partial struct _name_e__FixedBuffer
    {
        public sbyte e0;
    }

    [InlineArray(64)]
    public partial struct _type_e__FixedBuffer
    {
        public sbyte e0;
    }

    [InlineArray(64)]
    public partial struct _xxc_e__FixedBuffer
    {
        public xmp_channel e0;
    }

    [InlineArray(256)]
    public partial struct _xxo_e__FixedBuffer
    {
        public byte e0;
    }
}
