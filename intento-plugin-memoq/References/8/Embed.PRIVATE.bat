
echo on
set arg1=%1
set arg2=%2
echo %arg1%
echo %arg2%

if not exist %arg1%..\bin\IntentoSDK.dll goto error2
if not exist %arg1%..\ilmerge\ilmerge.exe goto error3
if not exist %arg1%..\bin\Intento.MT.Plugin.PropertiesForm.dll goto error5

rem echo --- target directory
mkdir %arg1%..\MemoQ.IntentoMT

rem echo --- ilmerge ---
echo %arg1%..\ilmerge\ilmerge /targetplatform:v4 /out:%arg1%..\MemoQ.IntentoMT\MemoQ.IntentoMT.dll %arg1%%arg2%Intento.MemoQMTPlugin.dll %arg1%%arg2%IntentoSDK.dll %arg1%%arg2%Intento.MT.Plugin.PropertiesForm.dll  %arg1%%arg2%Newtonsoft.Json.dll
%arg1%..\ilmerge\ilmerge /targetplatform:v4 /out:%arg1%..\MemoQ.IntentoMT\Intento.MemoQMTPlugin.dll %arg1%%arg2%Intento.MemoQMTPlugin.dll %arg1%%arg2%IntentoSDK.dll %arg1%%arg2%Intento.MT.Plugin.PropertiesForm.dll  %arg1%%arg2%Newtonsoft.Json.dll


rem exit 0

rem copy %arg1%%arg2%Intento.MemoQMTPlugin.dll %arg1%..\Intento.MemoQMTPlugin.Splitted
rem %arg1%%arg2%Intento.MemoQMTPlugin.pdb %arg1%..\Intento.MemoQMTPlugin.Splitted
rem %arg1%%arg2%Intento.MT.Plugin.PropertiesForm.dll %arg1%..\Intento.MemoQMTPlugin.Splitted
rem %arg1%%arg2%Intento.MT.Plugin.PropertiesForm.pdb %arg1%..\Intento.MemoQMTPlugin.Splitted
rem %arg1%%arg2%IntentoSDK.dll %arg1%..\Intento.MemoQMTPlugin.Splitted
rem %arg1%%arg2%IntentoSDK.pdb %arg1%..\Intento.MemoQMTPlugin.Splitted
rem %arg1%%arg2%Newtonsoft.Json.dll %arg1%..\Intento.MemoQMTPlugin.Splitted

echo --- signing ---
rem echo %arg1%..\kgsign\MemoQ.AddinSigner.exe -s %arg1%..\MemoQ.IntentoMT\Intento.MemoQMTPlugin.dll %arg1%..\kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml
%arg1%..\kgsign\MemoQ.AddinSigner.exe -s %arg1%..\MemoQ.IntentoMT\Intento.MemoQMTPlugin.dll %arg1%..\kgsign\Intento.MemoQMTPluginPrivatePublicKey.xml

rem echo --- zipping ---
rem if not exist "C:\Program Files\7-Zip\7z.exe" goto noZip
rem del ..\Intento.MemoQMTPlugin.7z
rem echo "C:\Program Files\7-Zip\7z.exe" a ..\Intento.MemoQMTPlugin.7z ..\IntentoMTPlugin\Intento.MemoQMTPlugin.dll ..\kgsign\Intento.MemoQMTPlugin.kgsign
rem "C:\Program Files\7-Zip\7z.exe" a ..\Intento.MemoQMTPlugin.7z ..\IntentoMTPlugin\Intento.MemoQMTPlugin.dll ..\IntentoMTPlugin\Intento.MemoQMTPlugin.kgsign

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
