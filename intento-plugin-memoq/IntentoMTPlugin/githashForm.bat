@echo off
FOR /F "usebackq" %%a IN (`git rev-parse HEAD`) DO (
 set result=%%a
)
echo namespace IntentoMTPlugin { 
echo    static internal class GitHash    {    
echo        public const string hash =           
echo "%result%"                                   
echo            ;}}                               
