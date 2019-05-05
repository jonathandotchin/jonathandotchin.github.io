---
tags:
  - best practices
  - tips
---

To accurately debug or profile an external assembly or library (i.e. one you’re not directly compiling), you need the PDB files that accompany each of the DLLs. These files give your debugger or profiler access to information such as function names, line numbers, and other related metadata.

One thing that sucks in particular is debugging and profiling native Microsoft .NET assemblies without this kind of information. Fortunately, there’s a solution for this very issue. With a little-known feature in Visual Studio 2012 (and 2010 too!), you can connect to Microsoft’s Symbol Servers and obtain most of the debugging symbols for their assemblies and libraries.

Just go to Tools --> Options --> (expand) Debugging --> Symbols, and select the Microsoft Symbol Servers as your source for Symbols.

Getting the symbols from Microsoft every time you debug or profile is slow and painful. It’ll even give you a pop-up saying as much once you check the Microsoft Symbol Servers, so be sure to specify a directory under “Cache symbols in this directory”.

It will keep a local copy of the PDBs and check for updates every so often. As a result, you get your regular debugging/profiling AND you can see the function names of the Microsoft assemblies.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)