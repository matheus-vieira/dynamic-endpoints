# dynamic-endpoints

Add dynamic endpoints to a web project

# Motivation

Imagine a system that you could abstract a service call to a single method.

This method receives only a string, but it does not need a big validations.

To limit the number of string in a classical webapi with .net there are some options

## First option - Many Controllers

Pros: Fixed endpoints, with fixed call, less validations
Cons: Too much work to create new endpoints

```csharp
public class InfoAController
{
    private readonly IMyService _service;
    //...
    [HttpGet]
    public IActionResult GetInfo(string info)
    {
        return Ok(_service.Execute("InfoA"));
    }
}
```

```csharp
public class InfoBController
{
    private readonly IMyService _service;
    //...
    [HttpGet]
    public IActionResult GetInfo(string info)
    {
        return Ok(_service.Execute("InfoB"));
    }
}
```

## Second option - Single Controller

Pros: Less code to maintain
Cons: More validations to create

```csharp
private readonly IMyService _service;
//...
private readonly string[] _availableList = new[] { "InfoA", "InfoB" };
[HttpGet("/{info}")]
public IActionResult GetInfo(string info)
{
    if(string.IsNullOrEmpty(info) || _availableList.Contains(info))
        return BadRequest("Error");

    return Ok(_service.Execute(info));
}
```

## Minimal API to rescue.

With the evolution of dotnet technologies a new implementation with both previous options can be create

Create a minimal
```bash
dotnet new web -o MyApi
```

Change the `program.cs` following the code above:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var endpoints = new[] { "Foo", "Xpto", "bar" };

foreach (var endpoint in endpoints)
    app.MapGet($"/{endpoint}", () => $"Call {endpoint}");

app.Run();
```

Execute the aplication and navigate to the new endpoints

* `/foo`
* `/Xpto`
* `bar`