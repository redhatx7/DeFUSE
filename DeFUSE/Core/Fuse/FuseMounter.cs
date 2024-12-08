using System.Diagnostics;
using System.Runtime.InteropServices;
using DeFUSE.Core.Fuse.Configuration;
using DeFUSE.Interop;

namespace DeFUSE.Core.Fuse;

public class FuseMounter
{
    private const int F_DUPFD_CLOEXEC = 1030;
    
    public static FuseContext Mount(string mountPoint, IFileSystem fileSystem, MountArguments arguments)
    {
        var args = arguments.ToArgumentArray();
        return Mount(mountPoint, fileSystem, args);
    }


    private static dynamic FuseSessionPtrToStruct()
    {
        throw new NotImplementedException();
    }
    public static FuseContext Mount(string mountPoint, IFileSystem fileSystem, string[] arguments)
    {
        if(string.IsNullOrEmpty(mountPoint))
            throw new ArgumentException($"{nameof(mountPoint)} must not be null or empty.");

        var fuseArgs = new FuseInterop.FuseArgs(arguments);
        var session = FuseInterop.NewFuseSession(fuseArgs, IntPtr.Zero, 0,IntPtr.Zero);
        _ = session == IntPtr.Zero ? throw new ExternalException($"Unable to mount fuse session. Return Code : {session.ToInt32()}") : 0;
        
        if (!Directory.Exists(mountPoint))
        {
            throw new DirectoryNotFoundException("Mount point does not exist.");
        }
        var mountCode = FuseInterop.Mount(session, mountPoint);
        _ = mountCode != 0 ? throw new ExternalException($"Unable to mount fuse session. Return Code : {mountCode}") : 0;
        var fileDescriptor = FuseInterop.FileDescriptor(session);
        if (fileDescriptor <= 0)
        {
            throw new ExternalException($"Unable to get file descriptor. Return Code : {fileDescriptor}");
        }
        
        var mergedFd = LibCInterop.DuplicateFileDescriptor(fileDescriptor, F_DUPFD_CLOEXEC, 0);

        return new FuseContext()
        {
            FileDescriptor = fileDescriptor,
            MainFileDescriptor = mergedFd > 0 ? mergedFd : fileDescriptor,
            MountPoint = mountPoint,
            Session = session,
            MountedArguments = arguments
        };

    }
}


   


