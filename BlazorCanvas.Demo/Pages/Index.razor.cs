﻿using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Aptacode.BlazorCanvas.Demo.Pages;

public class IndexBase : ComponentBase
{
    protected BlazorCanvas Canvas { get; set; }
    protected int Width => 600;
    protected int Height => 600;

    private bool _imageLoaded = false;
    const int w = 100;
    const int h = 100;
    ArraySegment<int> data = new int[w * h];

    protected override async Task OnInitializedAsync()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(15));

        while (Canvas is not { Ready : true })
        {
            await Task.Delay(10);
        }

        for (var x = 0; x < w; x++)
        {
            for (var y = 0; y < h; y++)
            {
                var r = (byte)(x % 255);
                var g = (byte)(y % 255);
                var b = (byte)(x * y % 255);
                data[y * w + x] =
                        (255 << 24) |
                        (b << 16) |
                        (g << 8) | 
                         r;
            }
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

        var rand = new Random();

        for (var x = 0; x < w; x++)
        {
            for (var y = 0; y < h; y++)
            {
                var r = (byte)(rand.Next(255));
                var g = (byte)(y % 255);
                var b = (byte)(x * y % 255);
                var a = (byte)255;

                data[y * w + x] =
                        (255 << 24) |    // alpha
                        (b << 16) |    // blue
                        (g << 8) |    // green
                         r;            // red
            }
        }

        Canvas.DrawImageBuffer(300, 300, w, h);
        await InvokeAsync(StateHasChanged);
    }
}