ReadMe - Code Samples for Chapter 15, Asynchronous Programming

The AsyncSamples Solution contains several projects:
A library and a WPF application.
Two console applications that are available both as a .NET Framework console application as well as a .NET Core console application.

WPF Application
To run the WPF application successfully, you need to register with BING

Console Applications
The C# source code of the .NET Framework and the .NET Core console application makes use of the same source files that are available within the .NET Core console application. The .NET Framework console application uses links for the source files.

To build and run the .NET Core projects with Visual Studio, you need to install ASP.NET 5 tools in addition to Visual Studio 2015.
Install the tools from here:
https://go.microsoft.com/fwlink/?LinkId=627627

After opening the files from Visual Studio, you might need to start the command prompt and download the necessary NuGet packages using
>dnx restore

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