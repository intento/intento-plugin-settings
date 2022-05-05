# Build public plugin with Visual Studio

Open solution <code>Intento.MemoQMTPlugin.sln</code>
Set build configuration to <code>ReleasePublic</code>
Build solution. Plugin will be in folder <code>..\build</code>

# Build plugin

To build plugin you should edit and after that run file <code>**buid.bat**</code>.

```
@echo off

set Configuration=Release
set DoSign=0

"%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" build.memoQ.proj
```

First of all, you should set <code>**Configuration**</code>.

-[x] For public plugin: <code>**DebugPublic**</code> and <code>**ReleasePublic**</code>
-[x] For private plugin: <code>**Other configurations**</code>

After that, you should check path to <code>MSBuild</code>