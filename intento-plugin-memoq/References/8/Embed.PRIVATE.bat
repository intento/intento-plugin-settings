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
echo ilmerge\ilmerge /targetplatform:v4 /out:Merged\Intento.MemoQMTPlugin.dll bin\Intento.MemoQMTPlugin.dll bin\IntentoSDK.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Newtonsoft.Json.dll
ilmerge\ilmerge /targetplatform:v4 /out:Merged\Intento.MemoQMTPlugin.dll bin\Intento.MemoQMTPlugin.dll bin\IntentoSDK.dll bin\Intento.MT.Plugin.PropertiesForm.dll bin\Newtonsoft.Json.dll

echo --- signing ---
copy Merged\Intento.MemoQMTPlugin.dll Signed
echo kgsign\MemoQ.AddinSigner.exe -s Signed\Intento.MemoQMTPlugin.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml
kgsign\MemoQ.AddinSigner.exe -s Signed\Intento.MemoQMTPlugin.dll kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml

echo --- zipping ---
copy Signed\Intento.MemoQMTPlugin.dll MemoQ.IntentoMT
copy Signed\Intento.MemoQMTPlugin.kgsign MemoQ.IntentoMT
copy Signed\Intento.MemoQMTPlugin.kgsign MemoQ.IntentoMT\Intento.MemoQMTPlugin.skgsign
echo 7z a MemoQ.IntentoMT\Intento.MemoQMTPlugin.7z MemoQ.IntentoMT\Intento.MemoQMTPlugin.dll MemoQ.IntentoMT\Intento.MemoQMTPlugin.kgsign  MemoQ.IntentoMT\Intento.MemoQMTPlugin.skgsign
7z a MemoQ.IntentoMT\Intento.MemoQMTPlugin.7z MemoQ.IntentoMT\Intento.MemoQMTPlugin.dll MemoQ.IntentoMT\Intento.MemoQMTPlugin.kgsign  MemoQ.IntentoMT\Intento.MemoQMTPlugin.skgsign

if not exist MemoQ.IntentoMT\Intento.MemoQMTPlugin.7z goto error6
if not exist MemoQ.IntentoMT\Intento.MemoQMTPlugin.dll goto error6
if not exist MemoQ.IntentoMT\Intento.MemoQMTPlugin.kgsign goto error6
if not exist MemoQ.IntentoMT\Intento.MemoQMTPlugin.skgsign goto error6

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

