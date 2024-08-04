# C# Bindings for libxmp
libxmp is a software written in C to playback oldschool tracker (.it, .mod, etc.) files.  
Sadly there is no (to my knowledge) software thats similar and has a permissive license written in C#.  
So i made bindings.  

## Usage
Install any NAudio backend via nuget, use DirectSound (Windows only) or write your own ``IWavePlayer`` backend. Then initialise the Xmp class with any ``IWavePlayer`` backend. I.E:  
```csharp
    using var xmp = new Xmp(new DirectSoundOut());
```

To play the file just call (for async):
```csharp
    var playtask = xmp.PlayAsync(path-to-file);
    <some stuff>
    await playtask;
```
or as blocking call:
```csharp
    xmp.PlayBlocking(path-to-file);
```

For name of the song and file-format call:
```csharp
    xmp.TestModule(path-to-file, out var testInfo);
```
And for duration call:
```csharp
    xmp.GetEstimatedTotalPlayTime(path-to-file);
```

## Special Thanks to the [libxmp](https://github.com/libxmp/libxmp) project, aswell as the [ClangSharpPInvokeGenerator](https://github.com/dotnet/clangsharp/)!