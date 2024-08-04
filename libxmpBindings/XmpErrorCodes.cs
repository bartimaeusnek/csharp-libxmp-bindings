namespace libxmpBindings;

public enum XmpErrorCodes
{
    End = -1, /*if module was stopped or the loop counter was reached*/
    Internal = -2, /* Internal error */
    Format = -3, /* Unsupported module format */
    Load = -4, /* Error loading file */
    Depack = -5, /* Error depacking file */
    System = -6, /* System error */
    Invalid = -7, /* Invalid parameter */
    State = -8, /* Invalid player state */
}