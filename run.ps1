docker build $PSScriptRoot -f "$PSScriptRoot/NET3.1.Dockerfile" -t is-bug:net31
docker build $PSScriptRoot -f "$PSScriptRoot/NET5.Dockerfile" -t is-bug:net5

New-Item "$PSScriptRoot/results/net5"  -ItemType "directory" -ErrorAction Ignore
New-Item "$PSScriptRoot/results/net31"  -ItemType "directory" -ErrorAction Ignore

#docker run --rm --volume $PSScriptRoot/results/net5/:/results/ is-bug:net5  # low memory

#docker run --rm --volume $PSScriptRoot/results/net31/:/results/ is-bug:net31 # high memory
Write-Host ".NET 5"
docker run --rm is-bug:net5  

Write-Host ".NET Core 3.1"
docker run --rm  is-bug:net31