echo on

set Ñonfiguration=%2

cd %1
if '%Ñonfiguration%'=='Debug' del /S bin\AWSSDK*.pdb
if '%Ñonfiguration%'=='Debug' del /S bin\Serilog*.pdb
if '%Ñonfiguration%'=='Debug' del /S bin\Autofac*.pdb
if '%Ñonfiguration%'=='DebugLocal' del /S bin\AWSSDK*.pdb
if '%Ñonfiguration%'=='DebugLocal' del /S bin\Serilog*.pdb
if '%Ñonfiguration%'=='DebugLocal' del /S bin\Autofac*.pdb
if '%Ñonfiguration%'=='DebugStage' del /S bin\AWSSDK*.pdb
if '%Ñonfiguration%'=='DebugStage' del /S bin\Serilog*.pdb
if '%Ñonfiguration%'=='DebugStage' del /S bin\Autofac*.pdb
if '%Ñonfiguration%'=='Release' del /S bin\*.pdb 
if '%Ñonfiguration%'=='AmadeusDebug' del /S bin\AWSSDK*.pdb
if '%Ñonfiguration%'=='AmadeusRelease' del /S bin\*.pdb

del /S bin\*.xml

if not exist bin\Intento.SDK.dll goto error2
if not exist ilmerge\ilmerge.exe goto error3
if not exist bin\Intento.MT.Plugin.PropertiesForm.dll goto error5

echo --- mkdirs ---
if not exist Merged mkdir Merged
del Merged\*.* /Q
if not exist Signed mkdir Signed
del Signed\*.* /Q
if not exist build mkdir build
del build\*.* /Q

echo --- ilmerge ---
echo ilmerge\ilmerge /targetplatform:v4 /out:Merged\Intento.MemoQMTPlugin.dll bin\Intento.MemoQMTPlugin.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Intento.SDK.dll bin\Intento.SDK.Autofac.dll bin\Autofac.dll bin\AWSSDK.S3.dll bin\AWSSDK.Core.dll bin\UrlCombineLib.dll
ilmerge\ilmerge /targetplatform:v4 /out:Merged\Intento.MemoQMTPlugin.dll bin\Intento.MemoQMTPlugin.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Intento.SDK.dll bin\Intento.SDK.Autofac.dll bin\Autofac.dll bin\AWSSDK.S3.dll bin\AWSSDK.Core.dll bin\UrlCombineLib.dll 


echo --- signing ---
copy Merged\Intento.MemoQMTPlugin.dll Signed
if exist Merged\Intento.MemoQMTPlugin.pdb copy Merged\Intento.MemoQMTPlugin.pdb Signed
echo kgsign\MemoQ.AddinSigner.exe -s Signed\Intento.MemoQMTPlugin.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml
kgsign\MemoQ.AddinSigner.exe -s Signed\Intento.MemoQMTPlugin.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml

echo --- zipping ---
copy Signed\Intento.MemoQMTPlugin.dll build
if exist Signed\Intento.MemoQMTPlugin.pdb copy Signed\Intento.MemoQMTPlugin.pdb build
copy Signed\Intento.MemoQMTPlugin.kgsign build
copy Signed\Intento.MemoQMTPlugin.kgsign build\Intento.MemoQMTPlugin.skgsign
echo utils\7z.exe a build\Intento.MemoQMTPlugin.7z build\Intento.MemoQMTPlugin.dll build\Intento.MemoQMTPlugin.kgsign build\Intento.MemoQMTPlugin.skgsign
utils\7z.exe a build\Intento.MemoQMTPlugin.7z build\Intento.MemoQMTPlugin.dll build\Intento.MemoQMTPlugin.kgsign build\Intento.MemoQMTPlugin.skgsign

if not exist build\Intento.MemoQMTPlugin.7z goto error6
if not exist build\Intento.MemoQMTPlugin.dll goto error6
if not exist build\Intento.MemoQMTPlugin.kgsign goto error6
if not exist build\Intento.MemoQMTPlugin.skgsign goto error6

echo OK!
exit 0

:noZip
echo No zip!
exit 1

:error2
echo No %arg1%..\bin\Intento.SDK.dll
exit 2

:error3
echo No %arg1%..\ilmerge\ilmerge.exe
exit 3

:error4
echo No %arg1%bin\Intento.MT.Plugin.PropertiesForm.dll
exit 5

:error6
echo Build error!!!
exit 6

