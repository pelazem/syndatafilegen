See http://github.com/pelazem/syndatafilegen/

Command line:
dotnet SynDataFileGen.dll [Path to runfile] [-i]
Where:
[Path to runfile] is the path to a local JSON run file. See the samples in the above github repo.
-i is optional. If specified, the app will run and emit elapsed time at end, and wait for user input to exit. If omitted, app will run silently and auto-exit (better for automated scenarios).

Example:
dotnet SynDataFileGen.dll runFile.json -i