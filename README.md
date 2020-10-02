To execute you should just have to execute `run.ps1` 

This will build 2 docker images 'is-bug:net31' and 'is-bug:net5'and will then execute them inside docker for linux.

To reproduce the issue and see corrupted images you will need to be running docker desktop on a windows machine, configured to use the hyper-v backend, and constrained to only have 1 cpu.

Just providing access to 2 cpus (on my dev machine) was/is enough to no longer suffer from the problem.


The process will output a hash of the de coded image data to the console... the 2 hashes should be identical if there is no bug.