using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Aptacode.BlazorCanvas.Demo.Pages;

public class IndexBase : ComponentBase
{
    protected BlazorCanvas Canvas { get; set; }
    protected int Width => 1000;
    protected int Height => 1000;

    private bool _imageLoaded = false;
    const int w = 400;
    const int h = 400;
    ArraySegment<int> data = new int[w * h];

    private readonly Mandelbrot _mandelbrot = new();

    protected override async Task OnInitializedAsync()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(15));

        while (Canvas is not { Ready : true })
        {
            await Task.Delay(10);
        }

        Canvas.SetImageBuffer(data);

        while (await timer.WaitForNextTickAsync())
        {
            await Draw();
        }
    }

    protected async Task Draw()
    {

        Canvas.ClearRect(0, 0, Width, Height);
        //Ellipse
        Canvas.LineWidth(2);
        Canvas.StrokeStyle("blue");
        Canvas.FillStyle("green");
        Canvas.Ellipse(40, 40, 30, 30, (float)Math.PI, 0, 2 * (float)Math.PI);
        Canvas.Stroke();
        Canvas.Fill();

        Canvas.TextAlign("center");
        Canvas.FillStyle("black");
        Canvas.FillText("Text", 150, 30);

        var posX = 200f;
        var posY = 50f;
        var width = 250f;
        var height = 150f;
        //Rectangle
        Canvas.FillStyle("white");
        Canvas.StrokeRect(posX, posY, width, height);

        //Text
        Canvas.TextAlign("center");
        Canvas.FillStyle("black");
        Canvas.Font("10pt Calibri");
        Canvas.WrapText(
            "Space is big. You just won't believe how vastly, hugely, mind-bogglingly big it is. I mean, you may think it's a long way down the road to the chemist's, but that's just peanuts to space.",
            posX, posY, width, height, 15);

        //Path
        Canvas.StrokeStyle("black");
        Canvas.LineWidth(4);
        Canvas.BeginPath();
        Canvas.MoveTo(100, 50);
        Canvas.LineTo(75, 80);
        Canvas.LineTo(90, 130);
        Canvas.Stroke();

        //Polyline Fast
        Canvas.FillStyle("gray");
        Canvas.PolyLine(new double[] { 150, 200, 130, 230, 240, 240 });
        Canvas.Stroke();

        //Polygon
        Canvas.FillStyle("gray");
        Canvas.BeginPath();
        Canvas.MoveTo(30, 80);
        Canvas.LineTo(30, 150);
        Canvas.LineTo(60, 150);
        Canvas.ClosePath();
        Canvas.Stroke();
        Canvas.Fill();

        //Polygon Fast
        Canvas.FillStyle("gray");
        Canvas.Polygon(new double[] { 100, 100, 120, 120, 100, 120 });
        Canvas.Stroke();
        Canvas.Fill();

        //Image
        const string imageSource =
            "https://raw.githubusercontent.com/Aptacode/BlazorCanvas/main/Resources/Images/logo.png";
        if (!_imageLoaded)
        {
            _imageLoaded = true;

            await Canvas.LoadImage(imageSource);
        }

        Canvas.DrawImage(imageSource, 100, 250, 128, 128);

        _mandelbrot.RenderImage(data, w, h, HighResolution);
        Canvas.DrawImageBuffer(0, 0, w, h);

        await InvokeAsync(StateHasChanged);
    }

    public bool HighResolution { get; set; }
    public void MouseDown()
    {
        HighResolution = true;
    }

    public void MouseUp()
    {
        HighResolution = false;
    }
}