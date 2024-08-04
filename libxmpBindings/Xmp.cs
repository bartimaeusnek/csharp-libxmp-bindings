namespace libxmpBindings;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NativeBindings;
using NAudio.Wave;

public sealed class Xmp : IDisposable
{
    public readonly unsafe sbyte* Context;
    private readonly IWavePlayer _player;
    private readonly int _rate;
    private readonly XmpFormat _format;
    private readonly float _refillRatio;
    private bool _hasBeenDisposed = false;
    public unsafe Xmp(IWavePlayer player, int rate = 44100, XmpFormat format = XmpFormat.None, float refillRatio = 0.75f)
    {
        _player = player;
        _rate = rate;
        _format = format;
        _refillRatio = refillRatio;
        Context = libxmp.xmp_create_context();
    }
#region Play
    public async Task PlayWithTimeout(IEnumerable<string> paths, TimeSpan timeout, bool loop = true, CancellationToken cancellationToken = default)
    {
        var pathsArray = paths.ToArray();
        do
        {
            foreach (string path in pathsArray)
            {
                await PlayAsync(path, false, cancellationToken: cancellationToken);
                try
                {
                    await Task.Delay(timeout, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        } while (loop && !cancellationToken.IsCancellationRequested);
        
    }
    public static async Task PlayWithOverlap(Xmp xmp1, Xmp xmp2, IEnumerable<string> paths, float overlapPercentage = 1/16f, int loudnessSmoothingSteps = 100, bool loop = true, bool highPrecision = false, CancellationToken cancellationToken = default)
    {
        loudnessSmoothingSteps = Math.Clamp(loudnessSmoothingSteps, 1, 100);
        var pathsArray = paths.ToArray();
        Task player1;
        do
        {
            var initialSong = pathsArray[0];
            var initialTime = highPrecision ? xmp1.GetTotalPlayTime(initialSong) : xmp1.GetEstimatedTotalPlayTime(initialSong);
            xmp1.LoadModule(initialSong);
            xmp1.StartPlayer();
            player1 = xmp1.PlayAsync(false, cancellationToken);
            Debug.WriteLine("play xmp1");
            var delayfix = DateTime.UtcNow;
            for (int i = 1; i < pathsArray.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                var song2 = pathsArray[i];
                var time = highPrecision ? xmp2.GetTotalPlayTime(initialSong) : xmp2.GetEstimatedTotalPlayTime(song2);
                xmp2.LoadModule(song2);
                xmp2.StartPlayer();
                Debug.WriteLine("set xmp2 2 0");
                xmp2.SetVolume(0);
                var delayed = time * overlapPercentage / loudnessSmoothingSteps;
                var delayfix2 = DateTime.UtcNow - delayfix;
                try
                {
                    await Task.Delay(Math.Max((int)(initialTime * (1f - overlapPercentage) - delayfix2).TotalMilliseconds, 0), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    return;
                }
                Debug.WriteLine("play xmp2");
                var player2 = xmp2.PlayAsync(false, cancellationToken);
                delayfix = DateTime.UtcNow;
                for (int j = loudnessSmoothingSteps - 1; j >= 0; j--)
                {
                    if (xmp1.GetPlayerState() == XmpPlayerStates.Playing)
                        xmp1.SetVolume((100 / loudnessSmoothingSteps) * j);
                    xmp2.SetVolume((100 / loudnessSmoothingSteps) * (loudnessSmoothingSteps - j));
                    try
                    {
                        await Task.Delay(delayed, cancellationToken);
                    }
                    catch (TaskCanceledException)
                    {
                        return;
                    }
                }
                Debug.WriteLine("set xmp2 2 100");
                xmp2.SetVolume(100);
                await player1;
                Debug.WriteLine("Swap");
                (xmp1, xmp2) = (xmp2, xmp1);
                initialTime = time;
                player1 = player2;
            }
        } while (loop && !cancellationToken.IsCancellationRequested);
        if (!player1.IsCompleted)
            await player1;
    }
    public async Task<bool> PlayAsync(string path, bool loop = true, CancellationToken cancellationToken = default)
    {
        if (!LoadModule(path))
            return false;
        
        await PlayInternal(loop, cancellationToken);
        return true;
    }
    public Task PlayAsync(bool loop = true, CancellationToken cancellationToken = default)
    {
        return PlayInternal(loop, cancellationToken);
    }
    public bool PlayBlocking(string path, bool loop = true, CancellationToken cancellationToken = default)
    {
        if (!LoadModule(path))
            return false;
        
        PlayBlocking(loop, cancellationToken);
        return true;
    }
    public void PlayBlocking(bool loop = true, CancellationToken cancellationToken = default)
    {
        PlayInternal(loop, cancellationToken).Wait(CancellationToken.None);
    }
    public async Task<bool> PlayAsync(string path, int buffersize, bool loop = true, CancellationToken cancellationToken = default)
    {
        if (!LoadModule(path))
            return false;
        
        await PlayAsync(buffersize, loop, cancellationToken);
        return true;
    }
    public bool PlayBlocking(string path, int buffersize, bool loop = true, CancellationToken cancellationToken = default)
    {
        if (!LoadModule(path))
            return false;
        
        PlayBlocking(buffersize, loop, cancellationToken);
        return true;
    }
    public void PlayBlocking(int buffersize, bool loop = true, CancellationToken cancellationToken = default)
    {
        PlayInternal(buffersize, loop, cancellationToken).Wait(CancellationToken.None);
    }
    private async Task PlayInternal(bool loop, CancellationToken cancellationToken)
    {
        if (GetPlayerState() == XmpPlayerStates.Loaded)
        {
            StartPlayer();
        }
        var waveProvider = new BufferedWaveProvider(new WaveFormat(_rate, _format.HasFlag(XmpFormat.Eightbit) ? 8 : 16, _format.HasFlag(XmpFormat.Mono) ? 1 : 2));
        _player.Init(waveProvider);
        byte[] buffer = new byte[(int)XmpLimits.MaxFramesize];
        _player.Play();
        var refillDuration = waveProvider.BufferDuration * _refillRatio;
        while (PlayFrame(buffer.AsSpan(), out int length, out int loopcounter) && (loop || loopcounter == 0) && !cancellationToken.IsCancellationRequested)
        {
            while (waveProvider.BufferedBytes >= waveProvider.BufferLength - length)
            {
                await Task.Delay(refillDuration, CancellationToken.None);
            }
            waveProvider.AddSamples(buffer, 0, length);
        }
        EndPlayer();
        ReleaseModule();
    }
    private async Task PlayInternal(int buffersize, bool loop, CancellationToken cancellationToken)
    {
        if (GetPlayerState() == XmpPlayerStates.Loaded)
        {
            StartPlayer();
        }
        var waveProvider = new BufferedWaveProvider(new WaveFormat(_rate, _format.HasFlag(XmpFormat.Eightbit) ? 8 : 16, _format.HasFlag(XmpFormat.Mono) ? 1 : 2));
        byte[] buffer = new byte[buffersize];
        _player.Init(waveProvider);
        _player.Play();
        var refillDuration = waveProvider.BufferDuration * _refillRatio;
        while (PlayBuffer(buffer, loop) && !cancellationToken.IsCancellationRequested)
        {
            while (waveProvider.BufferedBytes >= waveProvider.BufferLength - buffer.Length)
            {
                await Task.Delay(refillDuration, CancellationToken.None);
            }
            waveProvider.AddSamples(buffer, 0, buffer.Length);
        }
        EndPlayer();
        ReleaseModule();
    }
    public Task PlayAsync(int buffersize, bool loop = true, CancellationToken cancellationToken = default)
    {
        return PlayInternal(buffersize, loop, cancellationToken);
    }
    public unsafe bool PlayFrame(Span<byte> buffer, out int length, out int loopcounter)
    {
        var ret = PlayFrame(out var fi);
        fixed (byte* ptr = buffer)
            NativeMemory.Copy(fi.buffer, ptr, (nuint) fi.buffer_size);
        length = fi.buffer_size;
        loopcounter = fi.loop_count;
        return ret;
    }
    public unsafe bool PlayFrame(out void* buffer, out int bufferSize, out int loopcounter)
    {
        var fi = new xmp_frame_info();
        int error = libxmp.xmp_play_frame(Context);
        bool ret = !IsInInvalidState(error) && error != (int)XmpErrorCodes.End;
        libxmp.xmp_get_frame_info(Context, &fi);
        buffer = fi.buffer;
        bufferSize = fi.buffer_size;
        loopcounter = fi.loop_count;
        return ret;
    }
    private unsafe bool PlayFrame(out xmp_frame_info frameInfo)
    {
        var fi = new xmp_frame_info();
        int error = libxmp.xmp_play_frame(Context);
        bool ret = !IsInInvalidState(error) && error != (int)XmpErrorCodes.End;
        libxmp.xmp_get_frame_info(Context, &fi);
        frameInfo = fi;
        return ret;
    }
    public unsafe bool PlayBuffer(byte[] buffer, bool loop)
    {
        int error;
        fixed (byte* ptr = &buffer[0])
            error = libxmp.xmp_play_buffer(Context, ptr, buffer.Length, loop ? 0 : 1);
        return !IsInInvalidState(error) && error != (int)XmpErrorCodes.End;
    }
#endregion
    public XmpPlayerStates GetPlayerState()
    {
        return (XmpPlayerStates) GetParameter(XmpPlayerParameters.State);
    }
    public unsafe bool StartPlayer()
    {
        if (GetPlayerState() != XmpPlayerStates.Loaded)
        {
            throw new XmpIllegalStateException(XmpErrorCodes.State, "You MUST load a module before starting the player!");
        }
        int error = libxmp.xmp_start_player(Context, _rate, (int)_format);
        return !IsInInvalidState(error);
    }
    private unsafe void EndPlayer()
    {
        libxmp.xmp_end_player(Context);
    }
    private unsafe void ReleaseModule()
    {
        libxmp.xmp_release_module(Context);
    }
    public unsafe bool LoadModule(string path)
    {
        var error = libxmp.xmp_load_module(Context, path);
        return !IsInInvalidState(error);
    }
    public unsafe int GetParameter(XmpPlayerParameters param)
    {
        var error = libxmp.xmp_get_player(Context, (int) param);
        _ = IsInInvalidState(error);
        return error;
    }
#region SetVolume
    public int SetVolume(int volume)
    {
        return SetParameter(XmpPlayerParameters.Volume, Math.Clamp(volume, 0, 100));
    }
    public int SetVolume(uint volume)
    {
        return SetParameter(XmpPlayerParameters.Volume, (int)Math.Clamp(volume, 0u, 100u));
    }
    public int SetVolume(float volume)
    {
        return SetParameter(XmpPlayerParameters.Volume, Math.Clamp((int)MathF.Round(volume * 100f), 0, 100));
    }
    public int SetVolume(double volume)
    {
        return SetParameter(XmpPlayerParameters.Volume, Math.Clamp((int)Math.Round(volume * 100d), 0, 100));
    }
#endregion
    /**
     * WARNING, CHECK DOKUMENTATION BEFORE USE!
     */
    public unsafe int SetParameter(XmpPlayerParameters param, int value)
    {
        if (GetPlayerState() != XmpPlayerStates.Playing)
        {
            throw new XmpIllegalStateException(XmpErrorCodes.State, "Player must be STARTED before you can set parameters!");
        }
        var error = libxmp.xmp_set_player(Context, (int) param, value);
        _ = IsInInvalidState(error);
        return error;
    }
    public record TestInfo(string Name, string Format);
    public static unsafe bool TestModule(string path, [NotNullWhen(true)] out TestInfo? testInfo)
    {
        var nativetestInfo = new xmp_test_info();
        bool isModule = libxmp.xmp_test_module(path, &nativetestInfo) == 0;
        if (isModule is false)
        {
            testInfo = null;
            return false;
        }
        testInfo = new TestInfo(nativetestInfo.Name, nativetestInfo.Type);
        return isModule;
    }
    /**
     * Warning! This is costly to call, use GetEstimatedTotalPlayTime instead if applicable
     */
    public unsafe TimeSpan GetTotalPlayTime(string path)
    {
        LoadModule(path);
        StartPlayer();
        var fi = new xmp_frame_info();
        ulong totalTime = 0;
        while (fi.loop_count == 0)
        {
            int error = libxmp.xmp_play_frame(Context);
            if (IsInInvalidState(error))
                return TimeSpan.Zero;
        
            libxmp.xmp_get_frame_info(Context, &fi);
            totalTime += (ulong)fi.frame_time;
        }
        return TimeSpan.FromMicroseconds(totalTime);
    }
    public unsafe TimeSpan GetEstimatedTotalPlayTime(string path, uint samples = 1)
    {
        LoadModule(path);
        StartPlayer();
        var fi = new xmp_frame_info();
        ulong totalTime = 0;
        int error = libxmp.xmp_play_frame(Context);
        
        if (IsInInvalidState(error))
            return TimeSpan.Zero;
        
        libxmp.xmp_get_frame_info(Context, &fi);
        totalTime += (ulong)fi.total_time;
        int positions = (int) (fi.total_time / samples);
        for (int i = 1; i < samples; i++)
        {
            if (!SkipToPosition(positions * i))
            {
                return TimeSpan.Zero;
            }
            error = libxmp.xmp_play_frame(Context);
            if (IsInInvalidState(error))
                return TimeSpan.Zero;
            
            libxmp.xmp_get_frame_info(Context, &fi);
            totalTime += (ulong)fi.total_time;
        }
        EndPlayer();
        ReleaseModule();
        // ReSharper disable once PossibleLossOfFraction
        return TimeSpan.FromMilliseconds(totalTime / samples);
    }
    public static unsafe IEnumerable<string?> GetFormatList()
    {
        sbyte** nativeList = libxmp.xmp_get_format_list();
        if (nativeList == null)
            return Array.Empty<string>();

        var managedList = new List<string?>();
        int index = 0;

        while (true)
        {
            IntPtr currentPtr = Marshal.ReadIntPtr((IntPtr)nativeList, index * nint.Size);
            if (currentPtr == nint.Zero)
                break;

            string? format = Marshal.PtrToStringAnsi(currentPtr);
            managedList.Add(format);
            index++;
        }

        return managedList;
    }
    private bool IsInInvalidState(int error)
    {
        if (error >= (int)XmpErrorCodes.End)
            return false;
        EndPlayer();
        ReleaseModule();
        Dispose();
        throw new XmpIllegalStateException((XmpErrorCodes) error);
    }
    public unsafe bool SkipToPosition(int milliseconds)
    {
        int error = libxmp.xmp_seek_time(Context, milliseconds);
        return !IsInInvalidState(error);
    }
#region IDisposeable
    private unsafe void ReleaseUnmanagedResources()
    {
        libxmp.xmp_free_context(Context);
    }
    private void ReleaseManagedResources()
    {
        try
        {
            _player.Dispose();
        }
        catch (Exception)
        {
            //ignored
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Dispose()
    {
        if (_hasBeenDisposed)
            return;
        ReleaseManagedResources();
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
        _hasBeenDisposed = true;
    }
    ~Xmp()
    {
        ReleaseUnmanagedResources();
    }
#endregion
}