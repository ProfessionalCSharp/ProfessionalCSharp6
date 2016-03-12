ReadMe - Code Samples for Chapter 20, Diagnostics and Application Insights

The sample code for this chapter contains several solutions:
- EventSourceSamples
- Net46EventSourceSamples
- Net46ReflectionSamples

The Net46EventSourceSamples directory contains the same source code (using linked files) as the EventSourceSamples directory. Use EventSourceSamples with .NET Core, Net46EventSourceSamples with the full .NET Framework.
You can open and build the Net46EventSourceSamples with a default installation of Visual Studio.

To open and build the EventSourceSamples solution, you need to install ASP.NET 5 tools in addition to Visual Studio 2015. 
Install the tools from here:
https://go.microsoft.com/fwlink/?LinkId=627627

After opening the files from Visual Studio, you might need to open the command prompt and download the necessary NuGet packages using
>dnu restore

If you have the .NET Core Command Line (CLI) Tools installed, you can use
>dotnet restore
instead.

Please download and install the .NET Core Command Line (CLI) Tools from https://github.com/dotnet/cli. For Windows, you will find an MSI package that you can install on your Windows system: https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe

The DynamicSamples directory contains both .NET Core as well as full .NET Framework projects. The .NET Core projects need to have the ASP.NET 5 tools installed as described earlier.
The CalculatorLib project is a .NET Core library that is loaded dynamically by the ClientApp application. To make this possible, you need to copy the CalculatorLib.dll to the c:/AddIns directory. Please create this directory, or change the directory where the library should be loaded in the file Program.cs (ClientApp Project). 

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