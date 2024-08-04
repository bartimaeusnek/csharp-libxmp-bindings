namespace libxmpBindings;

public enum XmpPlayerParameters
{
    Amp = 00, /* Amplification factor */
    Mix = 01, /* Stereo mixing */
    Interp = 02, /* Interpolation type */
    Dsp = 03, /* DSP effect flags */
    Flags = 04, /* Player flags */
    Cflags = 05, /* Player flags for current module */
    Smpctl = 06, /* Sample control flags */
    Volume = 07, /* Player module volume */
    State = 08, /* Internal player state (read only) */
    SmixVolume = 09, /* SMIX volume */
    Defpan = 10, /* Default pan setting */
    Mode = 11, /* Player personality */
    MixerType = 12, /* Current mixer (read only) */
    Voices = 13, /* Maximum number of mixer voices */
}