using DeFUSE.Core.Fuse.Enums;

namespace DeFUSE.Core.Fuse.Configuration;

/// <summary>
/// Represents the final mount arguments configuration
/// </summary>
public class MountArguments
{
    // Internal dictionary of mount options
    private readonly Dictionary<MountOption, string> _options;

    internal MountArguments(Dictionary<MountOption, string> options)
    {
        _options = options;
    }
    
    public static MountArgumentBuilder CreateMountOptions()
    {
        return new MountArgumentBuilder();
    }

    /// <summary>
    /// Convert mount options to string array for native FUSE interop
    /// </summary>
    public string[] ToArgumentArray()
    {
        return _options.Select(option =>
            string.IsNullOrEmpty(option.Value)
                ? new[] { "-o", $"{ConvertOptionToString(option.Key)}" }
                : new[] { "-o", $"{ConvertOptionToString(option.Key)}={option.Value}" }
        ).SelectMany(x => x).ToArray();
    }

    /// <summary>
    /// Check if a specific option is set
    /// </summary>
    public bool HasOption(MountOption option)
    {
        return _options.ContainsKey(option);
    }

    /// <summary>
    /// Get the value of a parameterized option
    /// </summary>
    public string GetOptionValue(MountOption option)
    {
        return _options.TryGetValue(option, out var value) ? value : string.Empty;
    }

    /// <summary>
    /// Convert enum to kebab-case string for FUSE options
    /// </summary>
    private static string ConvertOptionToString(MountOption option)
    {
        return option switch
        {
            MountOption.FsName => "fsname",
            MountOption.SubType => "subtype",
            MountOption.AllowOther => "allow_other",
            MountOption.AllowRoot => "allow_root",
            MountOption.AutoUnmount => "auto_unmount",
            MountOption.DefaultPermissions => "default_permissions",
            MountOption.Dev => "dev",
            MountOption.NoDev => "nodev",
            MountOption.Suid => "suid",
            MountOption.NoSuid => "nosuid",
            MountOption.Ro => "ro",
            MountOption.Rw => "rw",
            MountOption.Exec => "exec",
            MountOption.NoExec => "noexec",
            MountOption.Atime => "atime",
            MountOption.NoAtime => "noatime",
            MountOption.DirSync => "dirsync",
            MountOption.Sync => "sync",
            MountOption.Async => "async",
            _ => throw new ArgumentException($"Unsupported mount option: {option}")
        };
    }
}

