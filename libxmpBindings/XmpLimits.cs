namespace libxmpBindings;

public enum XmpLimits
{
    MaxKeys = 121, /* Number of valid keys */
    MaxEnvPoints = 32, /* Max number of envelope points */
    MaxModLength = 256, /* Max number of patterns in module */
    MaxChannels = 64, /* Max number of channels in module */
    MaxSrate = 49170, /* max sampling rate (Hz) */
    MinSrate = 4000, /* min sampling rate (Hz) */
    MinBpm = 20, /* min BPM */
    MaxFramesize = (5 * MaxSrate * 2 / MinBpm),
}