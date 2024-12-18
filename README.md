# DeFUSE ⚡

**DeFUSE** is a minimalistic C# toolkit designed for direct interaction with the Linux kernel's filesystem operations. By simplifying traditional FUSE mechanics, DeFUSE provides a clean, efficient approach to kernel-level filesystem development. 
\
For fun, I was studying about Distributed FileSystems and since C# is my favorite language, I've decided to create a simple FUSE binding for C#.
This binding is only working on linux yet. No BSD or OsX support
## Features 

- **Simple Design:** A simple design without over-engineered OOP
- **Direct Kernel Access:** Interfaces directly with the kernel, reducing unnecessary overhead. Only uses libfuse to `mount` and `unmount`
- **Efficiency:** Employs `unsafe` code where necessary to achieve optimal performance.

## 🛠 Roadmap

- Real-world examples to guide implementation.
- Tests to ensure reliability.
- Expanded feature set to cater to diverse use cases.

