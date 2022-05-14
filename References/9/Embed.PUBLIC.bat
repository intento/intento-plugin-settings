echo on

set Сonfiguration=%2

cd %1
if '%Сonfiguration%'=='DebugPublic' del /S bin\AWSSDK*.pdb
if '%Сonfiguration%'=='DebugPublic' del /S bin\Serilog*.pdb
if '%Сonfiguration%'=='DebugPublic' del /S bin\Autofac*.pdb
if '%Сonfiguration%'=='ReleasePublic' del /S bin\*.pdb
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
echo ilmerge\ilmerge /targetplatform:v4/out:Merged\MemoQ.IntentoMT.dll bin\Intento.MemoQMTPlugin.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Intento.SDK.dll bin\Intento.SDK.DependencyInjection.Lite.dll
ilmerge\ilmerge /targetplatform:v4 /out:Merged\MemoQ.IntentoMT.dll bin\Intento.MemoQMTPlugin.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Intento.SDK.dll bin\Intento.SDK.DependencyInjection.Lite.dll

echo --- signing ---
copy Merged\MemoQ.IntentoMT.dll Signed
echo kgsign\MemoQ.AddinSigner.exe -s Signed\MemoQ.IntentoMT.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml
kgsign\MemoQ.AddinSigner.exe -s Signed\MemoQ.IntentoMT.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml

echo --- copying ---
copy Signed\MemoQ.IntentoMT.dll build
copy Signed\MemoQ.IntentoMT.kgsign build
copy Signed\MemoQ.IntentoMT.kgsign build\MemoQ.IntentoMT.skgsign

if not exist build\MemoQ.IntentoMT.dll goto error6
if not exist build\MemoQ.IntentoMT.kgsign goto error6
if not exist build\MemoQ.IntentoMT.skgsign goto error6

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
echo No C:\Program Files\7-Zip\7z.exe
exit 4

:error5
echo No %arg1%bin\Intento.MT.Plugin.PropertiesForm.dll
exit 5

:error6
echo Build error!!!
exit 6

