namespace libxmpBindings.NativeBindings;

public unsafe partial struct xmp_callbacks
{
    [NativeTypeName("unsigned long (*)(void *, unsigned long, unsigned long, void *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, void*, uint> read_func;

    [NativeTypeName("int (*)(void *, long, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, int> seek_func;

    [NativeTypeName("long (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> tell_func;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> close_func;
}
