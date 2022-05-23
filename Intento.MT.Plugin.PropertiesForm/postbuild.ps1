$file=$args[0]
Write-Host $file
$res = signtool.exe verify /pa /q $file
if (!$?)
{
	Write-Host signing $file 
	signtool.exe sign /q /d "Intento.MT.Plugin.PropertiesForm Library" /du "https://inten.to" /fd SHA256 /tr http://ts.ssl.com /td sha256 /sha1 ceb3832e3ea926565f7dd05c82a711307b6679ee $file
}
else
{
	Write-Host Alerady signed $file
}



