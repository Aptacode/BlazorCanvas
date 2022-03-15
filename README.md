<p align="center">
   <div style="width:640;height:320">
       <img style="width: inherit" src="https://raw.githubusercontent.com/Aptacode/BlazorCanvas/main/Resources/Images/Banner.jpg">
</div>
</p>

A high performance blazor wrapper around the HTML5 Canvas utilizing unmarshalled JS calls

[![demo](https://github.com/Aptacode/BlazorCanvas/actions/workflows/demo.yml/badge.svg)](https://aptacode.github.io/BlazorCanvas/)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/249116ea839b4c689cada11bbc89ab0b)](https://www.codacy.com/gh/Aptacode/BlazorCanvas/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Aptacode/BlazorCanvas&amp;utm_campaign=Badge_Grade)
[![code metrics](https://github.com/Aptacode/BlazorCanvas/actions/workflows/metrics.yml/badge.svg)](https://github.com/Aptacode/BlazorCanvas/blob/main/CODE_METRICS.md)
[![nuget](https://img.shields.io/nuget/v/Aptacode.BlazorCanvas.svg?style=flat&color=brightgreen)](https://www.nuget.org/packages/Aptacode.BlazorCanvas/)
![last commit](https://img.shields.io/github/last-commit/Aptacode/BlazorCanvas?style=flat&cacheSeconds=86000&color=brightgreen)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://opensource.org/licenses/MIT)


[![NuGet](https://img.shields.io/nuget/v/Aptacode.BlazorCanvas.svg?style=flat)](https://www.nuget.org/packages/Aptacode.BlazorCanvas/)
![last commit](https://img.shields.io/github/last-commit/Aptacode/BlazorCanvas?style=flat-square&cacheSeconds=86000)

## Usage
### index.html
#### Add the Js script
```html
<script src="_content/Aptacode.BlazorCanvas/BlazorCanvasInterop.js"></script>
```

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


    
