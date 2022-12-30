using System;

namespace Aptacode.BlazorCanvas.Demo.Pages;

public class Mandelbrot
{
    private readonly double yMin = -1.5;
    private readonly double yMax = 1.5;
    private readonly double xMin = -2.0;
    private readonly double xMax = 2.0;
    private readonly int[] colours;
    private readonly int black = (255 << 24) |    // alpha
                            (0 << 16)|    // blue
                            (0 << 8) |    // green
                            (0);          // red

    private double Zoom = 0.01;
    private int maxIterations = 200;

    //Tante Renate
    private readonly double px = -0.7746806106269039;
    private readonly double py = -0.1374168856037867;

    public Mandelbrot()
    {
        colours = new int[1000];
        for (int i = 0; i < 1000; i++)
        {
            var colourIndex = (double)i / colours.Length;
            var hue = Math.Pow(colourIndex, 0.25);
            colours[i] = ColorFromHSLA(hue, 0.9, 0.6);
        }
    }
    public void RenderImage(ArraySegment<int> data, int width, int height, bool highResolution = false)
    {
        Zoom *= 0.9;
        maxIterations += 10;

        var RX1 = px - Zoom / 2;
        var RX2 = px + Zoom / 2;
        var RY1 = py - Zoom / 2;
        var RY2 = py + Zoom / 2;
        var dRx = RX2 - RX1;
        var dRy = RY2 - RY1;

        var xyPixelStep = highResolution ? 1 : 4;
        var xStep = (xMax - xMin) / (width) * xyPixelStep;
        var yStep = (yMax - yMin) / (height) * xyPixelStep;

        var yPix = 0;

        for (var y = yMin; y < yMax; y += yStep)
        {
            var y0 = y * dRy + RY1;
            var xPix = 0;
            for (var x = xMin; x < xMax; x += xStep)
            {
                var x0 = x * dRx + RX1;
                var x1 = x0;
                var y1 = y0;

                int k = 0;
                while (++k < maxIterations)
                {
                    double x2 = x1 * x1;
                    double y2 = y1 * y1;
                    y1 = 2 * x1 * y1 + y0;
                    x1 = x2 - y2 + x0;

                    if (x2 + y2 >= 4)
                    {
                        break;
                    }
                }

                var color = k < maxIterations ? colours[k % 1000] : black;

                for (int pX = 0; pX < xyPixelStep; pX++)
                {
                    for (int pY = 0; pY < xyPixelStep; pY++)
                    {
                        data[(yPix + pY) * width + (xPix + pX)] = color;
                    }
                }

                xPix += xyPixelStep;
            }
            yPix += xyPixelStep;
        }
    }

    private static int ColorFromHSLA(double H, double S, double L)
    {
        double v;
        double r, g, b;

        r = L;
        g = L;
        b = L;

        v = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);

        if (v > 0)
        {
            double m;
            double sv;
            int sextant;
            double fract, vsf, mid1, mid2;

            m = L + L - v;
            sv = (v - m) / v;
            H *= 6.0;
            sextant = (int)H;
            fract = H - sextant;
            vsf = v * sv * fract;
            mid1 = m + vsf;
            mid2 = v - vsf;

            switch (sextant)
            {
                case 0:
                    r = v;
                    g = mid1;
                    b = m;
                    break;

                case 1:
                    r = mid2;
                    g = v;
                    b = m;
                    break;

                case 2:
                    r = m;
                    g = v;
                    b = mid1;
                    break;

                case 3:
                    r = m;
                    g = mid2;
                    b = v;
                    break;

                case 4:
                    r = mid1;
                    g = m;
                    b = v;
                    break;

                case 5:
                    r = v;
                    g = m;
                    b = mid2;
                    break;
            }
        }

        return (255 << 24) |                // alpha
                ((int)(b * 255) << 16) |    // blue
                ((int)(g * 255) << 8)  |    // green
                (int)(r * 255);             // red
    }
}
