using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Aptacode.BlazorCanvas.Demo.Pages
{
    public class IndexBase : ComponentBase
    {
        #region Properties
        public ElementReference Canvas { get; set; }

        [Inject] public BlazorCanvasInterop ctx { get; set; }


        #endregion

        #region Lifecycle

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await ctx.Register(Canvas);
            }

            //Ellipse
            ctx.LineWidth(2);
            ctx.StrokeStyle("green");
            ctx.FillStyle("white");
            ctx.Ellipse(40, 40, 30, 30, (float)Math.PI, 0, 2 * (float)Math.PI);
            ctx.Stroke();
            ctx.Fill();

            //Text
            ctx.TextAlign("center");
            ctx.FillStyle("black");
            ctx.FillText("Text", 150, 30);

            //Path
            ctx.StrokeStyle("black");
            ctx.LineWidth(4);
            ctx.BeginPath();
            ctx.MoveTo(100,50);
            ctx.LineTo(75, 80);
            ctx.LineTo(90, 130);
            ctx.Stroke();

            //Polygon
            ctx.FillStyle("gray");
            ctx.BeginPath();
            ctx.MoveTo(30, 80);
            ctx.LineTo(30, 150);
            ctx.LineTo(60, 150);
            ctx.ClosePath();
            ctx.Stroke();
            ctx.Fill();


            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion

    }
}
