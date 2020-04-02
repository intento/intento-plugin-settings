echo on
cd ..

if not exist bin\IntentoSDK.dll goto error2
if not exist ilmerge\ilmerge.exe goto error3
if not exist bin\Intento.MT.Plugin.PropertiesForm.dll goto error5

echo --- mkdirs ---
if not exist Merged mkdir Merged
del Merged\*.* /Q
if not exist Signed mkdir Signed
del Signed\*.* /Q
if not exist MemoQ.IntentoMT mkdir MemoQ.IntentoMT
del MemoQ.IntentoMT\*.* /Q

echo --- ilmerge ---
echo ilmerge\ilmerge /targetplatform:v4 /out:Merged\MemoQ.IntentoMT.dll bin\Intento.MemoQMTPlugin.dll bin\IntentoSDK.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Newtonsoft.Json.dll
ilmerge\ilmerge /targetplatform:v4 /out:Merged\MemoQ.IntentoMT.dll bin\Intento.MemoQMTPlugin.dll bin\IntentoSDK.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Newtonsoft.Json.dll

echo --- signing ---
copy Merged\MemoQ.IntentoMT.dll Signed
echo kgsign\MemoQ.AddinSigner.exe -s Signed\MemoQ.IntentoMT.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml
kgsign\MemoQ.AddinSigner.exe -s Signed\MemoQ.IntentoMT.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml

echo --- copying ---
copy Signed\MemoQ.IntentoMT.dll MemoQ.IntentoMT
copy Signed\MemoQ.IntentoMT.kgsign MemoQ.IntentoMT
copy Signed\MemoQ.IntentoMT.kgsign MemoQ.IntentoMT\MemoQ.IntentoMT.skgsign

if not exist MemoQ.IntentoMT\MemoQ.IntentoMT.dll goto error6
if not exist MemoQ.IntentoMT\MemoQ.IntentoMT.kgsign goto error6
if not exist MemoQ.IntentoMT\MemoQ.IntentoMT.skgsign goto error6

echo OK!
exit 0

:noZip
echo No zip!
exit 1

:error2
echo No %arg1%..\bin\IntentoSDK.dll
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

