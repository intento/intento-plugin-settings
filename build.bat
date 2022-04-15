@echo off

set Configuration=ReleasePublic
set DoSign=0

"%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" build.memoQ.proj -maxcpucount:1  /fileLogger