@echo off

set Version=3.3.24
set Key=%NUGET_PUPLISH_KEY%
set Source=https://api.nuget.org/v3/index.json

dotnet nuget push build/Intento.MT.Plugin.PropertiesForm.%Version%.nupkg --api-key %Key% --source %Source%