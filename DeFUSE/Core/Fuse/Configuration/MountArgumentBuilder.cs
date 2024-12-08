using DeFUSE.Core.Fuse.Enums;

namespace DeFUSE.Core.Fuse.Configuration;

public class MountArgumentBuilder
{
     // Internal collection to store mount options
        private readonly Dictionary<MountOption, string> _options = new Dictionary<MountOption, string>();
        
        // Flag to track if parameterless options have been added
        private readonly HashSet<MountOption> _parameterlessOptions = new HashSet<MountOption>
        {
            MountOption.AllowOther,
            MountOption.AllowRoot,
            MountOption.AutoUnmount,
            MountOption.DefaultPermissions,
            MountOption.Dev,
            MountOption.NoDev,
            MountOption.Suid,
            MountOption.NoSuid,
            MountOption.Ro,
            MountOption.Rw,
            MountOption.Exec,
            MountOption.NoExec,
            MountOption.Atime,
            MountOption.NoAtime,
            MountOption.DirSync,
            MountOption.Sync,
            MountOption.Async
        };

        /// <summary>
        /// Set the filesystem name in mtab
        /// </summary>
        public MountArgumentBuilder WithFsName(string name)
        {
            _options[MountOption.FsName] = name;
            return this;
        }

        /// <summary>
        /// Set the filesystem subtype in mtab
        /// </summary>
        public MountArgumentBuilder WithSubType(string subType)
        {
            _options[MountOption.SubType] = subType;
            return this;
        }

        /// <summary>
        /// Add a parameterless mount option
        /// </summary>
        public MountArgumentBuilder AddOption(MountOption option)
        {
            if (!_parameterlessOptions.Contains(option))
            {
                throw new ArgumentException($"Option {option} requires a parameter or is not a valid parameterless option.");
            }

            _options[option] = null;
            return this;
        }

        /// <summary>
        /// Generate the final mount arguments
        /// </summary>
        public MountArguments Build()
        {
            return new MountArguments(_options);
        }

        /// <summary>
        /// Convenience method to create a read-only filesystem
        /// </summary>
        public MountArgumentBuilder AsReadOnly()
        {
            _options[MountOption.Ro] = null;
            return this;
        }

        /// <summary>
        /// Convenience method to allow other users to access the filesystem
        /// </summary>
        public MountArgumentBuilder AllowAllUsers()
        {
            _options[MountOption.AllowOther] = null;
            return this;
        }
}