$Day = (Get-Date).Day
$Year = (Get-Date).Year
$Cookie = (Get-Content -Path .\.cookie).Trim()

New-Item -Path "Data" -Name "day$Day.txt" -ItemType "file"
New-Item -Path "DataExamples" -Name "day$Day.txt" -ItemType "file"
Add-Content -Path "Data\answers.txt" -Value "0 | 0"
Add-Content -Path "DataExamples\answers.txt" -Value "0 | 0"

$Path = "Answers\Solutions\Day$Day.cs"

if ((Test-Path $Path) -eq $False) {
	((Get-Content "Answers\Solutions\Day0.cs" -Raw) -replace [regex]::escape("[Answer(0)]"),"[Answer($Day)]" -replace "class Day0","class Day$Day") | Set-Content -NoNewline -Path $Path
}

$wc = New-Object System.Net.WebClient
$wc.Headers.Add([System.Net.HttpRequestHeader]::Cookie, "session=$Cookie")
$wc.DownloadFile("https://adventofcode.com/$Year/day/$Day/input", (Resolve-Path "Data\day$Day.txt"))

Start-Process "https://adventofcode.com/$Year/day/$Day"
