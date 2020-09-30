To execute you should just have to execute `run.ps1` 

this will build 2 docker images 'is-bug:net31' and 'is-bug:net5' the attempt to run them with various memory constraints.


Once complete you should find the outputs in the `results` folder 
output folder follow the convention 

* **prefix** = dotnet version (including sdk and target framework)
* **postfix** = memory constants 'lm' = low(200m), 'hm' = high(10g)