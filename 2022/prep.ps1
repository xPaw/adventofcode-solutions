$Day = (Get-Date).Day

New-Item -Path "Data" -Name "day$Day.txt" -ItemType "file"
New-Item -Path "DataExamples" -Name "day$Day.txt" -ItemType "file"

$Path = "Answers\Solutions\Day$Day.cs"

if ((Test-Path $Path) -eq $False) {
	((Get-Content "Answers\Solutions\Day1.cs" -Raw) -replace [regex]::escape("[Answer(1)]"),"[Answer($Day)]" -replace "class Day1","class Day$Day") | Set-Content -Path $Path
}
