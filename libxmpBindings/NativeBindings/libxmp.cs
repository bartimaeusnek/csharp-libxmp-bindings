namespace libxmpBindings.NativeBindings;

using System.Runtime.InteropServices;

/**
 * For help see https://xmp.sourceforge.net/libxmp.html
 */
public static unsafe partial class libxmp
{
    [LibraryImport("libxmp")]
    public static partial int xmp_syserrno();

    [LibraryImport("libxmp")]
    [return: NativeTypeName("xmp_context")]
    public static partial sbyte* xmp_create_context();

    [LibraryImport("libxmp")]
    public static partial void xmp_free_context([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp", StringMarshalling = StringMarshalling.Utf8)]
    public static partial int xmp_load_module([NativeTypeName("xmp_context")] sbyte* context, [NativeTypeName("const char *")] string path);

    [LibraryImport("libxmp")]
    public static partial int xmp_load_module_from_memory([NativeTypeName("xmp_context")] sbyte* context, [NativeTypeName("const void *")] void* param1, [NativeTypeName("long")] int param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_load_module_from_file([NativeTypeName("xmp_context")] sbyte* context, void* param1, [NativeTypeName("long")] int param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_load_module_from_callbacks([NativeTypeName("xmp_context")] sbyte* context, void* param1, [NativeTypeName("struct xmp_callbacks")] xmp_callbacks param2);

    [LibraryImport("libxmp", StringMarshalling = StringMarshalling.Utf8)]
    public static partial int xmp_test_module([NativeTypeName("const char *")] string path, [NativeTypeName("struct xmp_test_info *")] xmp_test_info* param1);

    [LibraryImport("libxmp")]
    public static partial int xmp_test_module_from_memory([NativeTypeName("const void *")] void* param0, [NativeTypeName("long")] int param1, [NativeTypeName("struct xmp_test_info *")] xmp_test_info* param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_test_module_from_file(void* param0, [NativeTypeName("struct xmp_test_info *")] xmp_test_info* param1);

    [LibraryImport("libxmp")]
    public static partial int xmp_test_module_from_callbacks(void* param0, [NativeTypeName("struct xmp_callbacks")] xmp_callbacks param1, [NativeTypeName("struct xmp_test_info *")] xmp_test_info* param2);

    [LibraryImport("libxmp")]
    public static partial void xmp_scan_module([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial void xmp_release_module([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial int xmp_start_player([NativeTypeName("xmp_context")] sbyte* context, int param1, int param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_play_frame([NativeTypeName("xmp_context")] sbyte* context);
    
    [LibraryImport("libxmp")]
    public static partial int xmp_play_buffer([NativeTypeName("xmp_context")] sbyte* context, void* buffer, int size, int loop);

    [LibraryImport("libxmp")]
    public static partial void xmp_get_frame_info([NativeTypeName("xmp_context")] sbyte* context, [NativeTypeName("struct xmp_frame_info *")] xmp_frame_info* param1);

    [LibraryImport("libxmp")]
    public static partial void xmp_end_player([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial void xmp_inject_event([NativeTypeName("xmp_context")] sbyte* context, int param1, [NativeTypeName("struct xmp_event *")] xmp_event* param2);

    [LibraryImport("libxmp")]
    public static partial void xmp_get_module_info([NativeTypeName("xmp_context")] sbyte* context, [NativeTypeName("struct xmp_module_info *")] xmp_module_info* param1);

    [LibraryImport("libxmp")]
    [return: NativeTypeName("const char *const *")]
    public static partial sbyte** xmp_get_format_list();

    [LibraryImport("libxmp")]
    public static partial int xmp_next_position([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial int xmp_prev_position([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial int xmp_set_position([NativeTypeName("xmp_context")] sbyte* context, int param1);

    [LibraryImport("libxmp")]
    public static partial int xmp_set_row([NativeTypeName("xmp_context")] sbyte* context, int param1);

    [LibraryImport("libxmp")]
    public static partial int xmp_set_tempo_factor([NativeTypeName("xmp_context")] sbyte* context, double param1);

    [LibraryImport("libxmp")]
    public static partial void xmp_stop_module([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial void xmp_restart_module([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial int xmp_seek_time([NativeTypeName("xmp_context")] sbyte* context, int param1);

    [LibraryImport("libxmp")]
    public static partial int xmp_channel_mute([NativeTypeName("xmp_context")] sbyte* context, int param1, int param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_channel_vol([NativeTypeName("xmp_context")] sbyte* context, int chn, int vol);
    
    [LibraryImport("libxmp")]
    public static partial int xmp_get_player([NativeTypeName("xmp_context")] sbyte* context, int param);

    [LibraryImport("libxmp")]
    public static partial int xmp_set_player([NativeTypeName("xmp_context")] sbyte* context, int param, int value);

    [LibraryImport("libxmp")]
    public static partial int xmp_set_instrument_path([NativeTypeName("xmp_context")] sbyte* context, [NativeTypeName("const char *")] sbyte* param1);

    [LibraryImport("libxmp")]
    public static partial int xmp_start_smix([NativeTypeName("xmp_context")] sbyte* context, int param1, int param2);

    [LibraryImport("libxmp")]
    public static partial void xmp_end_smix([NativeTypeName("xmp_context")] sbyte* context);

    [LibraryImport("libxmp")]
    public static partial int xmp_smix_play_instrument([NativeTypeName("xmp_context")] sbyte* context, int param1, int param2, int param3, int param4);

    [LibraryImport("libxmp")]
    public static partial int xmp_smix_play_sample([NativeTypeName("xmp_context")] sbyte* context, int param1, int param2, int param3, int param4);

    [LibraryImport("libxmp")]
    public static partial int xmp_smix_channel_pan([NativeTypeName("xmp_context")] sbyte* context, int param1, int param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_smix_load_sample([NativeTypeName("xmp_context")] sbyte* context, int param1, [NativeTypeName("const char *")] sbyte* param2);

    [LibraryImport("libxmp")]
    public static partial int xmp_smix_release_sample([NativeTypeName("xmp_context")] sbyte* context, int param1);
}
