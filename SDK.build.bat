@echo off

set Configuration=Release
set DoSign=1
set Version=3.2.3

"%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" SDK.build.proj -maxcpucount:1 /fileLogger

pause