using System.Runtime.InteropServices;

namespace DeFUSE.Interop;

public static partial class LibCInterop
{
    private const string LibC = "libc";
    #if LINUX
  
    #elif BSD
    private const string LibC = "libc";
    #endif
    
    [LibraryImport(LibC, EntryPoint = "read",  SetLastError = true)]
    public static  partial int Read(int fd, ReadOnlySpan<byte> buffer, int count);
    
    [LibraryImport(LibC, EntryPoint = "write", SetLastError = true)]
    public static  partial int Write(int fd, ReadOnlySpan<byte> buffer, int count);

    [LibraryImport(LibC, EntryPoint = "fcntl", SetLastError = true)]
    public static partial int DuplicateFileDescriptor(int fd, int op, int args = 0);
    
}