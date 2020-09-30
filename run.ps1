docker build $PSScriptRoot -f "$PSScriptRoot/NET3.1.Dockerfile" -t is-bug:net31
docker build $PSScriptRoot -f "$PSScriptRoot/NET5.Dockerfile" -t is-bug:net5

New-Item "$PSScriptRoot/results/net5-lm"  -ItemType "directory" -ErrorAction Ignore
New-Item "$PSScriptRoot/results/net5-hm"  -ItemType "directory" -ErrorAction Ignore
New-Item "$PSScriptRoot/results/net31-lm"  -ItemType "directory" -ErrorAction Ignore
New-Item "$PSScriptRoot/results/net31-hm"  -ItemType "directory" -ErrorAction Ignore

docker run --rm --memory="200m" --volume $PSScriptRoot/results/net5-lm/:/results/ is-bug:net5  # low memory
docker run --rm --memory="10g" --volume $PSScriptRoot/results/net5-hm/:/results/ is-bug:net5 # high memory

docker run --rm --memory="200m" --volume $PSScriptRoot/results/net31-lm/:/results/ is-bug:net31 # low memory
docker run --rm --memory="10g" --volume $PSScriptRoot/results/net31-hm/:/results/ is-bug:net31 # high memory