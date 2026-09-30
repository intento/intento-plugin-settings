@echo off
setlocal

if not defined Configuration set Configuration=Release
if not defined DoSign set DoSign=1
if not defined Version set Version=3.5.0

rem Always run from the directory that contains this script
pushd "%~dp0"

rem ---- Locate MSBuild.exe: any VS 2017+ edition (Community/Professional/Enterprise/BuildTools), newest first
set "MSBUILD="
set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist "%VSWHERE%" (
    for /f "usebackq delims=" %%i in (`"%VSWHERE%" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do set "MSBUILD=%%i"
)
rem Fallback: MSBuild.exe already on PATH (e.g. Developer Command Prompt)
if not defined MSBUILD (
    for %%i in (MSBuild.exe) do if not "%%~$PATH:i"=="" set "MSBUILD=%%~$PATH:i"
)
if not defined MSBUILD (
    echo ERROR: MSBuild.exe not found. Install Visual Studio 2017+ or Build Tools with the MSBuild component,
    echo        or run this script from a Developer Command Prompt.
    goto :fail
)
echo Using MSBuild: %MSBUILD%

rem ---- Locate nuget.exe (needed by "nuget pack" in SDK.build.proj); download a local copy if absent
set "NUGET="
for %%i in (nuget.exe) do if not "%%~$PATH:i"=="" set "NUGET=%%~$PATH:i"
if not defined NUGET if exist "%~dp0.tools\nuget.exe" set "NUGET=%~dp0.tools\nuget.exe"
if not defined NUGET (
    echo nuget.exe not found on PATH, downloading to .tools\nuget.exe ...
    if not exist "%~dp0.tools" mkdir "%~dp0.tools"
    curl -sSL -o "%~dp0.tools\nuget.exe" https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
    if not exist "%~dp0.tools\nuget.exe" (
        echo ERROR: could not download nuget.exe. Install it or put it on PATH.
        goto :fail
    )
    set "NUGET=%~dp0.tools\nuget.exe"
)
for %%i in ("%NUGET%") do set "PATH=%%~dpi;%PATH%"
echo Using nuget:   %NUGET%

rem ---- Restore PackageReferences, then build/pack/sign via SDK.build.proj
"%MSBUILD%" Intento.MT.Plugin.PropertiesForm.sln -t:Restore -p:Configuration=%Configuration% -nologo -v:minimal
if errorlevel 1 goto :fail

rem dotnet SDK version decides which certificate fingerprint form "dotnet nuget sign" accepts (see SDK.build.proj)
set "DOTNET_SDK="
for /f "delims=" %%v in ('dotnet --version 2^>nul') do set "DOTNET_SDK=%%v"
if "%DoSign%"=="1" if not defined DOTNET_SDK (
    echo ERROR: dotnet SDK not found; it is required for "dotnet nuget sign". Install it or build with DoSign=0.
    goto :fail
)

"%MSBUILD%" SDK.build.proj -maxcpucount:1 /fileLogger -p:DotnetSdkVersion=%DOTNET_SDK%
if errorlevel 1 goto :fail

popd
echo.
echo Build succeeded.
pause
exit /b 0

:fail
popd
echo.
echo Build FAILED.
pause
exit /b 1
