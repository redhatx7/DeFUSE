using DeFUSE.Core.Fuse.Configuration;

namespace DeFUSE.Core.Fuse;

public class FuseContext : IDisposable
{
    public int FileDescriptor { get; set; }
    public int MainFileDescriptor { get; set; }
    public IntPtr Session { get; set; }
    public string MountPoint { get; set; } = null!;

    public string[] MountedArguments { get; set; } = null!;
    
   
    

    public void Dispose()
    {
        // TODO release managed resources here
    }
}