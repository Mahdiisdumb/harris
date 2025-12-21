\# Harris



Harris is a tiny .NET tool that scans all DLLs in a Unity project and automatically fixes broken `\[MovedFrom]` boolean attributes that make Unity’s ApiUpdater crash. 



The tool:



\- Recursively scans your Unity project folder for DLLs

\- Removes any `\[MovedFrom]` constructor arguments that are booleans

\- Logs which DLLs were patched



---



\### Why "Harris"?



It’s named after my classmate Harrison (aka Harris). He’s usually a complete imbecile, but when lunch detention is on the line… suddenly he’s Einstein. That duality seemed fitting for a tool that fixes broken stuff automatically.



---



\### Usage



```bash

dotnet run -- "C:\\Path\\To\\Your\\UnityProject"

