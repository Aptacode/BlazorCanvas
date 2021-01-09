# BlazorCanvas
A high performance blazor wrapper around the HTML5 Canvas utilizing unmarshalled JS calls

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/249116ea839b4c689cada11bbc89ab0b)](https://www.codacy.com/gh/Aptacode/BlazorCanvas/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Aptacode/BlazorCanvas&amp;utm_campaign=Badge_Grade)
[![NuGet](https://img.shields.io/nuget/v/Aptacode.BlazorCanvas.svg?style=flat)](https://www.nuget.org/packages/Aptacode.BlazorCanvas/)
![last commit](https://img.shields.io/github/last-commit/Aptacode/BlazorCanvas?style=flat-square&cacheSeconds=86000)

## Usage

### Program.cs
#### Register BlazorCanvas
```csharp
  builder.Services.AddSingleton<BlazorCanvasInterop>();
```
### RazorComponent.razor
#### Setup your canvas element
```html
<canvas @ref="Canvas" width="100" height="100"/>
```
### RazorComponent.razor.cs
#### Register the canvas with BlazorCanvas
```csharp
  #region Properties
  [Inject] public BlazorCanvasInterop BlazorCanvasInterop { get; set; }

  public ElementReference Canvas { get; set; }
  #endregion

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
      if (firstRender)
      {
          await BlazorCanvasInterop.Register(Canvas);
      }

      await base.OnAfterRenderAsync(firstRender);
  }

  #endregion
 ```


    
