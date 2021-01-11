using System;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Aptacode.BlazorCanvas
{
    public class BlazorCanvasInterop
    {
        private readonly IJSUnmarshalledRuntime _jsUnmarshalledRuntime;
        private readonly IJSRuntime _jsRuntime;

        public BlazorCanvasInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _jsUnmarshalledRuntime = (IJSUnmarshalledRuntime)jsRuntime;
        }

        public async Task Register(ElementReference canvas)
        {
            await _jsRuntime.InvokeVoidAsync("registerCanvas", canvas);
        }

        public void Fill()
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<object>("fill");
        }

        public void Stroke()
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<object>("stroke");
        }

        public void BeginPath()
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<object>("beginPath");
        }

        public void Ellipse(float x, float y, float radiusX, float radiusY,
            float rotation, float startAngle, float endAngle)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("ellipse",
                new[] { x, y, radiusX, radiusY, rotation, startAngle, endAngle });
        }

        public void Polygon(Vector2[] vertices)
        {
            var floats = new float[vertices.Length * 2];
            var floatCount = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                var vertex = vertices[i];
                floats[floatCount++] = vertex.X;
                floats[floatCount++] = vertex.Y;
            }
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("polygon",
                floats);
        }

        public void PolyLine(Vector2[] vertices)
        {
            var floats = new float[vertices.Length * 2];
            var floatCount = 0;
            for(int i = 0; i < vertices.Length; i++)
            {
                var vertex = vertices[i];
                floats[floatCount++] = vertex.X;
                floats[floatCount++] = vertex.Y;
            }
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("polyline",
                floats);
        }

        public void FillRect(float x, float y, float width, float height)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("fillRect", new[] { x, y, width, height });
        }
        public void StrokeRect(float x, float y, float width, float height)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("strokeRect", new[] { x, y, width, height });
        }

        public void ClearRect(float x, float y, float width, float height)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("clearRect", new[] { x, y, width, height });
        }

        public void ClosePath()
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<object>("closePath");
        }

        public void MoveTo(float x, float y)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("moveTo", new[] { x, y });
        }

        public void LineTo(float x, float y)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("lineTo", new[] { x, y });
        }

        public void FillStyle(string defaultFillColor)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, object>("fillStyle", defaultFillColor);
        }

        public void StrokeStyle(string defaultBorderColor)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, object>("strokeStyle", defaultBorderColor);
        }

        public void LineWidth(float defaultBorderThickness)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<float[], object>("lineWidth", new[] { defaultBorderThickness });
        }

        #region Text

        public void TextAlign(string textAlign)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, object>("textAlign", textAlign);
        }

        public void FillText(string text, float x, float y)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, float[], object>("fillText", text, new[] { x, y });
        }

        public void Font(string font)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, object>("font", font);
        }

        public void WrapText(string text, float x, float y, float maxWidth, float maxHeight, float lineHeight)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, float[], object>("wrapText", text, new[] { x, y, maxWidth, maxHeight, lineHeight });
        }

        #endregion

        #region Images
        
        public async Task LoadImage(string src)
        {
            await _jsRuntime.InvokeVoidAsync("loadImage", src);
        }
        
        public void DrawImage(string src, float x, float y)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, float[], object>("drawImage", src, new[] { x, y });
        }
        
        public void DrawImage(string src, float x, float y, float width, float height)
        {
            _jsUnmarshalledRuntime.InvokeUnmarshalled<string, float[], object>("drawImage", src, new[] { x, y, width, height });
        }
        #endregion
    }
}
