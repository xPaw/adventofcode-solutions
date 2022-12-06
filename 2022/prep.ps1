$Day = (Get-Date).Day

New-Item -Path "Data" -Name "day$Day.txt" -ItemType "file"
New-Item -Path "DataExamples" -Name "day$Day.txt" -ItemType "file"

$Path = "Answers\Solutions\Day$Day.cs"

if ((Test-Path $Path) -eq $False) {
	((Get-Content "Answers\Solutions\Day0.cs" -Raw) -replace [regex]::escape("[Answer(0)]"),"[Answer($Day)]" -replace "class Day0","class Day$Day") | Set-Content -NoNewline -Path $Path
}
