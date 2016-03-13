ReadMe - Code Samples for Chapter 27, XML and JSON

The sample code for this chapter contains these solutions:
- XMLandJSON
- Net46XMLandJSON

The .NET Core projects from XMLandJSON is duplicated in Net46XMLandJSON with full .NET Framework projects. The source code files are linked from the XMLandJSON project.

To open and build the .NET Core projects of XMLandJSON you need to install ASP.NET 5 tools in addition to Visual Studio 2015. 
Install the tools from here:
https://go.microsoft.com/fwlink/?LinkId=627627

After opening the files from Visual Studio, you might need to open the command prompt and download the necessary NuGet packages using
>dnu restore

If you have the .NET Core Command Line (CLI) Tools installed, you can use
>dotnet restore
instead.

Please download and install the .NET Core Command Line (CLI) Tools from https://github.com/dotnet/cli. For Windows, you will find an MSI package that you can install on your Windows system: https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe

When .NET Core RC2 is available, the files from this Chapter will be changed. With all projects, the project.json files will be changed. A few C# source files might change as well.

Please re-check the Wrox code downloads for updates.

Currently,
8-Apr is planned for RC2
30-Jun is planned for RTM of .NET Core.
Please check this link for Microsoft's actual schedule:
https://github.com/dotnet/corefx/milestones

For code comments and issues please check https://github.com/ProfessionalCSharp/ProfessionalCSharp6

Thank you!
Christian