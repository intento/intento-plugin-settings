$file=$args[0]
Write-Host $file
$res = signtool.exe verify /pa /q $file
if (!$?)
{
	Write-Host signing $file 
	signtool.exe sign /q /d "Intento.MT.Plugin.PropertiesForm Library" /du "https://inten.to" /fd SHA256 /tr http://timestamp.digicert.com /td sha256 /sha1 0d1f66efbfc3f97c281800cbc3a91ab883fb1663 $file
}
else
{
	Write-Host Alerady signed $file
}



