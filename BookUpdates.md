This document contains changes with .NET Core that have been changed after the book **Professional C# 6 and .NET Core 1.0** was published, as well as typos.

## Chapter 1 - .NET Application Architectures

Page 15 - .NET Native
.NET Native didn't make it to the first release of .NET Core. Building native applications on Linux and Windows will be possible with a later release. Currently it's just possible to compile UWP applications to native code.

Page 19 - Note - check for future implementations for dotnet --template at [Reimagine dotnet-new](https://github.com/dotnet/cli/issues/2052). 
Preview 2 of the dotnet tools offers the --type option as described in the book. You can use the values Console, Web, Lib, and xunittest.

Page 20 - The *compilationOptions* from project.json changed to *buildOptions*

Page 20 - The framework *netstandardapp1.5* has been changed to *netcoreapp1.0*

Page 20: - using preview 2 of the dotnet tools, *dotnet new* produces this *project.json*:

```js
{
  "version": "1.0.0-*",
  "buildOptions": {
    "debugType": "portable",
    "emitEntryPoint": true
  },
  "dependencies": { },
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        }
      },
      "imports": "dnxcore50"
    }
  }
}
```

Page 20 - Version 1.4 of NetStandard.Library now includes support for the Universal Windows Platform (UAP). See the [.NET Platform Standard](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md ".NET Platform Standard").
New is version 1.6 which includes support for .NET 4.6.3 and .NET Core 1.0.

Page 21 - on top: netstandardapp1.5 should be netcoreapp1.0:
```javascript
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        }
      },
      "imports": "dnxcore50"
    },
    "net46": {}
  }
```

Page 22 - Note, inside the note *dotnet compile* is mentioned. This should be *dotnet build

Page 23 - the table lists *7.0* for Entity Framework. It should be *Core 1.0* instead

## Chapter 2 - Core C<span>#</span>

Page 33 - Console Application (Package) changed to Console Application (.NET Core). You can find this template within Installed -> Templates -> Visual C# -> .NET Core

Page 34 - *project.json* change:
*netstandardapp1.5* should be *netcoreapp1.0*

*tags*, *projectUrl*, *licenseUrl* should not be within the root of project.json, but instead within *packOptions*. Form the Visual Studio template, these options are no longer added to project.json. I removed them from the RC2 sample files.

## Chapter 3 - Objects and Types

Page 76 - typo: *_* missing with _firstName variable within get accessor

Page 84 - typo: new MySingleton(42) should by new Singleton(42)

## Chapter 6 - Generics

Page 173 - Typo in the first note
Within the method GetRectangles an underscore is missing accessing the variable _coll

## Chapter 9 - Delegates, Lambdas, and Events

Page 264 - Wrong code in NewCarIsHere, the code should be like this:

```csharp
public void NewCarIsHere(object sender, CarInfoEventArgs e)
{
  WriteLine($"{_name}: car {_e.Car} is new");
}
```

## Chapter 13 - Language Integrated Query

Page 370, the text before the summary:
`Expression<Func<TSource, bool>gt;` should be `Expression<Func<TSource, bool>>`

## Chapter 16 - Reflection, Metadata, and Dynamic Programming

Page 437 - CompilationOptions has been changed to BuildOptions

Page 437/438 - Loading an assembly dynamically has become easier, the DirectoryLoader is no longer needed, use AssemblyLoadContext instead of PlatformServices

```csharp
private static object GetCalculator()
{
  Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(CalculatorLibPath);
  Type type = assembly.GetType(CalculatorTypeName);
  return Activator.CreateInstance(type);
}
```

## Chapter 24 - Security

Page 697, the `InitProtection` method changed because of API changes with data protection. Configuration with the `AddDataProtection` method instead of the `ConfigureDataProtection` method:

```csharp
public static MySafe InitProtection()
{
  var serviceCollection = new ServiceCollection();   
  serviceCollection.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("."))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(20))
    .ProtectKeysWithDpapi();
          
  IServiceProvider services = serviceCollection.BuildServiceProvider();

  return ActivatorUtilities.CreateInstance<MySafe>(services);
}
```

## Chapter 38 - Entity Framework Core

Page 1166, the EF context with depdendency injection now needs a constructor with `DbContextOptions<TContext>`:

```csharp
public class BooksContext : DbContext
{
  public BooksContext(DbContextOptions<BooksContext> options)
    : base(options)
  {          
  }
  public DbSet<Book> Books { get; set; }
}
```

Page 1169, tools section:
The Entity Framework Core tools need to be referenced using *Microsoft.EntityFrameworkCore.Tools* instead of *dotnet-ef*:

```js
"tools": {
  "Microsoft.EntityFrameworkCore.Tools": {
    "version": "1.0.0-*",
    "imports": "portable-net452+win81"
  }
}
```

Page 1175, scaffold a model

dotnet instead of dnx: *dotnet ef dbcontext scaffold* instead of *dnx ef dbcontext scaffold*

## Chapter 40 - ASP.NET Core

Page 1227, the Main method changed slightly with UseKestrel (the new Web host), UseIISIntegration (integration when used with IIS), and UseContentRoot (to define the static content for the Web site):

```csharp
public static void Main(string[] args)
{
  var host = new WebHostBuilder()
    .UseKestrel()
    .UseIISIntegration()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseStartup<Startup>()
    .Build();

  host.Run();
}
```
Page 1229, after *Adding Static Content*, the text should be: ASP.NET Core 1.0 reduces the overhead as much as possible.

Page 1250, the directory for the configuration is now configured with `SetBasePath`

```csharp
public Startup(IHostingEnvironment env)
{
  var builder = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

  //...
}
```

## Chapter 41 - ASP.NET MVC

Page 1272, `ToString` instead of `ToShortDateString`:

```html
<td>@item.Day.ToString("d")</td>
```

Page 1284 (bottom of the page), the correct namespace for the tag helper is *Microsoft.AspNetCore.Mvc.TagHelpers*

Page 1289, the attribute `TargetElement` is now `HtmlTargetElement`

Page 1291, the quotes need to be removed from `@addTagHelper` (source file TagHelpers/CustomHelper.cshtml):

```
@addTagHelper *, MVCSampleApp
```

Page 1299, 1300
Action result names have been changed, the Http prefix removed: HttpBadRequest, HttpNotFound... renamed to BadRequest, NotFound.
[Action result naming changes](https://github.com/aspnet/Announcements/issues/153)

## Chapter 42 - ASP.NET Web API

Page 1332, the Swagger part of the implementation of ConfigureServices changed:

```csharp
services.AddSwaggerGen();           

services.ConfigureSwaggerGen(options =>
{
  options.SingleApiVersion(new Info
  {
    Version = "v1",
    Title = "Book Chapters",
    Description = "A sample for Professional C# 6"
  });
  options.IgnoreObsoleteActions();
  options.IgnoreObsoleteProperties();
  options.DescribeAllEnumsAsStrings();
});
```

Page 1332, the Swagger part of the implementation of Configure changed:

```csharp
app.UseSwagger();
app.UseSwaggerUi();
```

