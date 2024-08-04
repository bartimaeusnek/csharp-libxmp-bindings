namespace libxmpBindings;

public class XmpIllegalStateException : Exception
{
    public XmpErrorCodes Error { get; set; }
    public XmpIllegalStateException(XmpErrorCodes error)
    {
        Error = error;
    }
    public XmpIllegalStateException(XmpErrorCodes error, string? message) : base(message)
    {
        Error = error;
    }
}