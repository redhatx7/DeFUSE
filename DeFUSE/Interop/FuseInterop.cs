using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DeFUSE.Interop;

public static partial class FuseInterop
{
    
    private const string LibName = "libfuse3.so";
    
    [StructLayout(LayoutKind.Sequential)]
    public ref struct FuseArgs
    {
        public int Argc;
        public unsafe char** Argv;
        public int Allocated;

        public FuseArgs()
        {
            Allocated = 0;
        }

        public FuseArgs(string[] args)
        {
            Allocated = 0;
            unsafe
            {
                Argv = (char**)Marshal.AllocHGlobal(sizeof(char*) * args.Length);
                for (var i = 0; i < args.Length; i++)
                {
                    Argv[i] = (char*)Marshal.StringToHGlobalAnsi(args[i]);
                }
            }
           
        }
    }
    
     
    [LibraryImport(LibName, EntryPoint = "fuse_session_new", SetLastError = true)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr NewFuseSession(FuseArgs args, IntPtr op, uint opSize, IntPtr userData);


    [LibraryImport(LibName, EntryPoint = "fuse_session_mount", SetLastError = true)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mount(IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string mountPoint);

    [LibraryImport(LibName, EntryPoint = "fuse_session_fd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FileDescriptor(IntPtr session);


    [LibraryImport(LibName, EntryPoint = "fuse_session_unmount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Unmount(IntPtr session);
    
    [LibraryImport(LibName, EntryPoint = "fuse_session_destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySession(IntPtr session);
}