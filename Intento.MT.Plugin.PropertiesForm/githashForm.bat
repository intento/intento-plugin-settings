@echo off
FOR /F "usebackq" %%a IN (`git rev-parse HEAD`) DO (
 set result=%%a
)
echo namespace Intento.MT.Plugin.PropertiesForm { 
echo    static internal class GitHash    {    
echo        public const string hash =           
echo "%result%"                                   
echo            ;}}                               
