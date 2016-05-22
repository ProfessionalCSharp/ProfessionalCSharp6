This document contains changes with .NET Core that have been changed after the book **Professional C# 6 and .NET Core 1.0** was published, as well as typos.

# Chapter 1 - .NET Application Architectures

Page 15 - .NET Native
.NET Native didn't make it to the first release of .NET Core. Building native applications on Linux and Windows will be possible with a later release. Currently it's just possible to compile UWP applications to native code.

Page 19 - Note
The option --type will not be available using *dotnet new*. Instead, a much more flexible option will be available with preview 2 of the tools. 
Preview 2 offers the --template option to select any template. Installed templates will be shown wiht dotnet new --list. See [Reimagine dotnet-new](https://github.com/dotnet/cli/issues/2052)

Page 20 - The *compilationOptions* from project.json changed to *buildOptions*

# Chapter 38 - Entity Framework Core

Page 1169, tools section:
The Entity Framework Core tools need to be referenced using "Microsoft.EntityFrameworkCore.Tools" instead of "dotnet-ef":

```
"tools": {
  "Microsoft.EntityFrameworkCore.Tools": {
    "version": "1.0.0-*",
    "imports": "portable-net452+win81"
  }
}
```

