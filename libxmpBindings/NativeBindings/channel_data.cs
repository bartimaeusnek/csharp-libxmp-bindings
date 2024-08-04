namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public unsafe partial struct channel_data
{
    public int flags;

    public int per_flags;

    public int note_flags;

    public int note;

    public int key;

    public double period;

    public double per_adj;

    public int finetune;

    public int ins;

    public int old_ins;

    public int smp;

    public int mastervol;

    public int delay;

    public int keyoff;

    public int fadeout;

    public int ins_fade;

    public int volume;

    public int gvl;

    public int rvv;

    public int rpv;

    [NativeTypeName("uint8")]
    public byte split;

    [NativeTypeName("uint8")]
    public byte pair;

    public int v_idx;

    public int p_idx;

    public int f_idx;

    public int key_porta;

    [NativeTypeName("__AnonymousRecord_player_L121_C2")]
    public _vibrato_e__Struct vibrato;

    [NativeTypeName("__AnonymousRecord_player_L126_C2")]
    public _tremolo_e__Struct tremolo;

    [NativeTypeName("__AnonymousRecord_player_L132_C2")]
    public _panbrello_e__Struct panbrello;

    [NativeTypeName("__AnonymousRecord_player_L138_C2")]
    public _arpeggio_e__Struct arpeggio;

    [NativeTypeName("__AnonymousRecord_player_L145_C2")]
    public _insvib_e__Struct insvib;

    [NativeTypeName("__AnonymousRecord_player_L150_C2")]
    public _offset_e__Struct offset;

    [NativeTypeName("__AnonymousRecord_player_L156_C2")]
    public _retrig_e__Struct retrig;

    [NativeTypeName("__AnonymousRecord_player_L163_C2")]
    public _tremor_e__Struct tremor;

    [NativeTypeName("__AnonymousRecord_player_L169_C2")]
    public _vol_e__Struct vol;

    [NativeTypeName("__AnonymousRecord_player_L183_C2")]
    public _fine_vol_e__Struct fine_vol;

    [NativeTypeName("__AnonymousRecord_player_L188_C2")]
    public _gvol_e__Struct gvol;

    [NativeTypeName("__AnonymousRecord_player_L194_C2")]
    public _trackvol_e__Struct trackvol;

    [NativeTypeName("__AnonymousRecord_player_L200_C2")]
    public _freq_e__Struct freq;

    [NativeTypeName("__AnonymousRecord_player_L206_C2")]
    public _porta_e__Struct porta;

    [NativeTypeName("__AnonymousRecord_player_L214_C2")]
    public _fine_porta_e__Struct fine_porta;

    [NativeTypeName("__AnonymousRecord_player_L219_C2")]
    public _pan_e__Struct pan;

    [NativeTypeName("__AnonymousRecord_player_L227_C2")]
    public _invloop_e__Struct invloop;

    [NativeTypeName("__AnonymousRecord_player_L234_C2")]
    public _tempo_e__Struct tempo;

    [NativeTypeName("__AnonymousRecord_player_L238_C2")]
    public _filter_e__Struct filter;

    [NativeTypeName("__AnonymousRecord_player_L245_C2")]
    public _macro_e__Struct macro;

    [NativeTypeName("__AnonymousRecord_player_L256_C2")]
    public _noteslide_e__Struct noteslide;

    public void* extra;

    [NativeTypeName("struct xmp_event")]
    public xmp_event delayed_event;

    public int delayed_ins;

    public int info_period;

    public int info_pitchbend;

    public int info_position;

    public int info_finalvol;

    public int info_finalpan;

    public partial struct _vibrato_e__Struct
    {
        [NativeTypeName("struct lfo")]
        public lfo lfo;

        public int memory;
    }

    public partial struct _tremolo_e__Struct
    {
        [NativeTypeName("struct lfo")]
        public lfo lfo;

        public int memory;
    }

    public partial struct _panbrello_e__Struct
    {
        [NativeTypeName("struct lfo")]
        public lfo lfo;

        public int memory;
    }

    public partial struct _arpeggio_e__Struct
    {
        [NativeTypeName("int8[16]")]
        public _val_e__FixedBuffer val;

        public int size;

        public int count;

        public int memory;

        [InlineArray(16)]
        public partial struct _val_e__FixedBuffer
        {
            public sbyte e0;
        }
    }

    public partial struct _insvib_e__Struct
    {
        [NativeTypeName("struct lfo")]
        public lfo lfo;

        public int sweep;
    }

    public partial struct _offset_e__Struct
    {
        public int val;

        public int val2;

        public int memory;
    }

    public partial struct _retrig_e__Struct
    {
        public int val;

        public int count;

        public int type;

        public int limit;
    }

    public partial struct _tremor_e__Struct
    {
        [NativeTypeName("uint8")]
        public byte up;

        [NativeTypeName("uint8")]
        public byte down;

        [NativeTypeName("uint8")]
        public byte count;

        [NativeTypeName("uint8")]
        public byte memory;
    }

    public partial struct _vol_e__Struct
    {
        public int slide;

        public int fslide;

        public int slide2;

        public int memory;

        public int fslide2;

        public int memory2;

        public int target;
    }

    public partial struct _fine_vol_e__Struct
    {
        public int up_memory;

        public int down_memory;
    }

    public partial struct _gvol_e__Struct
    {
        public int slide;

        public int fslide;

        public int memory;
    }

    public partial struct _trackvol_e__Struct
    {
        public int slide;

        public int fslide;

        public int memory;
    }

    public partial struct _freq_e__Struct
    {
        public int slide;

        public double fslide;

        public int memory;
    }

    public partial struct _porta_e__Struct
    {
        public double target;

        public int dir;

        public int slide;

        public int memory;

        public int note_memory;
    }

    public partial struct _fine_porta_e__Struct
    {
        public int up_memory;

        public int down_memory;
    }

    public partial struct _pan_e__Struct
    {
        public int val;

        public int slide;

        public int fslide;

        public int memory;

        public int surround;
    }

    public partial struct _invloop_e__Struct
    {
        public int speed;

        public int count;

        public int pos;
    }

    public partial struct _tempo_e__Struct
    {
        public int slide;
    }

    public partial struct _filter_e__Struct
    {
        public int cutoff;

        public int resonance;

        public int envelope;

        public int can_disable;
    }

    public partial struct _macro_e__Struct
    {
        public float val;

        public float target;

        public float slide;

        public int active;

        public int finalvol;

        public int notepan;
    }

    public partial struct _noteslide_e__Struct
    {
        public int slide;

        public int fslide;

        public int speed;

        public int count;
    }
}
