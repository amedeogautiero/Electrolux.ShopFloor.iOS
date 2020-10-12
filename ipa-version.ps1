param([string]$p1,[string]$p2,[string]$p3)

#$p1 = $p1.Replace("""", "")
#$p2 = $p2.Replace("""", "")

#$ret = "'$($p1)' '$($p2)'" 
$a = gci env:
$p1 | Out-File -FilePath c:\temp\prova.txt 
$p2 | Out-File -FilePath c:\temp\prova.txt -Append
$p3 | Out-File -FilePath c:\temp\prova.txt -Append

[xml]$plist = Get-Content "C:\Users\amedeo.gautiero\source\repos\xShopFloor_dev\Electrolux.ShopFloor.iOS\Info-UAT.plist"
$ver =  $plist.SelectSingleNode("//*[text()='CFBundleVersion']").NextSibling.Value
$source = Join-Path -Path:$p1 -ChildPath:$p2
$dest = $source.Replace(".ipa", "$($ver).ipa");

if ($p3 -eq "UAT")
{
    $ver | Out-File -FilePath c:\temp\prova.txt -Append
    $dest | Out-File -FilePath c:\temp\prova.txt -Append
    Copy-Item -Path:$source -Destination:$dest -Force
}