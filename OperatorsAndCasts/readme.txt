ReadMe - Code Samples for Chapter 8, Operators and Casts

The samples are available in two variants:
a) using the full .NET Framework
b) using .NET Core

The sample code for the full framework is available in the directory Net46OperatorsAndCasts. The source code is not directly available in the Net46OperatorsAndCAsts directory, but is linked instead. The C# source code files are the same as used with the .NET Core samples. 
You can open the solution file Net46OperatorsAndCasts.sln with Visual Studio and work with it.

The sample code for .NET Core is available in the directory OperatorsAndCasts. To use these source files with Visual Studio, you need to install ASP.NET 5 tools in addition to Visual Studio 2015.
Install the tools from here:
https://go.microsoft.com/fwlink/?LinkId=627627

After opening the files from Visual Studio, you might need to start the command prompt and download the necessary NuGet packages using
>dnu restore

If you have the .NET Core Command Line (CLI) Tools installed, you can use
>dotnet restore
instead.

Please download and install the .NET Core Command Line (CLI) Tools from https://github.com/dotnet/cli. For Windows, you will find an MSI package that you can install on your Windows system: https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe 

When .NET Core RC2 is available, the files from this Chapter will be changed. I'm expecting that this will not influence the C# source code of this Chapter, but the project.json files will be changed. 

Please re-check the Wrox code downloads for updates.

Currently,
8-Apr is planned for RC2
30-Jun is planned for RTM of .NET Core.
Please check this link for Microsoft's actual schedule:
https://github.com/dotnet/corefx/milestones

For code comments and issues please check https://github.com/ProfessionalCSharp/ProfessionalCSharp6

Thank you!
Christian