Var /GLOBAL version
# define the name of the installer
Outfile "MemoQMTPlugin-$version".exe"
 
# define the directory to install to, the desktop in this case as specified  
# by the predefined $DESKTOP variable
InstallDir $DESKTOP
 
# default section
Section
 
# define the output path for this file
SetOutPath $INSTDIR
 
# define what to install and place it in the output path
File ../build/Intento.MemoQMTPlugin.dll
File ../build/Intento.MemoQMTPlugin.kgsign
File ../build/Intento.MemoQMTPlugin.skgsign
 
SectionEnd