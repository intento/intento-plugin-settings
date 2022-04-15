@echo off

set memoQ=%ProgramFiles%\memoQ\memoQ-9
set addinsPath=%memoQ%\Addins
set runMemoq=1

tasklist | find /i "MemoQ.exe" && taskkill /im MemoQ.exe /F || echo process "MemoQ.exe" not running.

xcopy "build\*.dll" "%addinsPath%" /sy
xcopy "build\*.pdb" "%addinsPath%" /sy
xcopy "build\*.*sign" "%addinsPath%" /sy

if %runMemoq%==1 start "" "%memoQ%\MemoQ.exe"

