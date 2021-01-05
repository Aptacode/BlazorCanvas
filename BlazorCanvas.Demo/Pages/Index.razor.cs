using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Aptacode.BlazorCanvas.Demo.Pages
{
    public class IndexBase : ComponentBase
    {
        #region Properties
        public ElementReference Canvas { get; set; }

        [Inject] public BlazorCanvasInterop BlazorCanvasInterop { get; set; }


        #endregion

        #region Lifecycle

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await BlazorCanvasInterop.Register(Canvas);
            }

            BlazorCanvasInterop.StrokeStyle("green");
            BlazorCanvasInterop.Ellipse(30, 30, 30, 30, (float)Math.PI, 0, 2 * (float)Math.PI);
            BlazorCanvasInterop.Stroke();
            
            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion

    }
}
