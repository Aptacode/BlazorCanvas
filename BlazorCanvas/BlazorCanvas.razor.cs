using System;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Numerics;

namespace Aptacode.BlazorCanvas
{
    public partial class BlazorCanvas : IAsyncDisposable
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected ElementReference canvasReference { get; set; }
        public bool Ready { get; private set; }

        private IJSObjectReference? module = null;

        protected override async Task OnInitializedAsync()
        {
            await JSHost.ImportAsync("BlazorCanvas",
                "../_content/Aptacode.BlazorCanvas/BlazorCanvas.razor.js");
            module = await JsRuntime.InvokeAsync<IJSObjectReference>("import",
                "../_content/Aptacode.BlazorCanvas/BlazorCanvas.razor.js");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (module != null)
            {
                await module.InvokeVoidAsync("canvas_register", canvasReference);
                Ready = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (module != null)
            {
                await module.DisposeAsync();
            }
        }

        [JSImport("canvas_save", "BlazorCanvas")]
        internal static partial void CanvasSave();

        public void Save()
        {
            CanvasSave();
        }

        [JSImport("canvas_restore", "BlazorCanvas")]
        internal static partial void CanvasRestore();

        public void Restore()
        {
            CanvasRestore();
        }

        [JSImport("canvas_fill", "BlazorCanvas")]
        internal static partial void CanvasFill();
        public void Fill()
        {
            CanvasFill();
        }


        [JSImport("canvas_stroke", "BlazorCanvas")]
        internal static partial void CanvasStroke();
        public void Stroke()
        {
            CanvasStroke();
        }


        [JSImport("canvas_beginPath", "BlazorCanvas")]
        internal static partial void CanvasBeginPath();
        public void BeginPath()
        {
            CanvasBeginPath();
        }

        [JSImport("canvas_ellipse", "BlazorCanvas")]
        internal static partial void CanvasEllipse(float x, float y, float radiusX, float radiusY,
            float rotation, float startAngle, float endAngle);
        public void Ellipse(float x, float y, float radiusX, float radiusY, float rotation, float startAngle, float endAngle)
        {
            CanvasEllipse(x, y, radiusX, radiusY, rotation, startAngle, endAngle);
        }

        [JSImport("canvas_polygon", "BlazorCanvas")]
        internal static partial void CanvasPolygon(double[] verticies);
        public void Polygon(double[] vertices)
        {
            CanvasPolygon(vertices);
        }

        [JSImport("canvas_fillRect", "BlazorCanvas")]
        internal static partial void CanvasFillRect(float x, float y, float width, float height); 
        public void FillRect(float x, float y, float width, float height)
        {
            CanvasFillRect(x, y, width, height);
        }
        [JSImport("canvas_strokeRect", "BlazorCanvas")]
        internal static partial void CanvasStrokeRect(float x, float y, float width, float height);
        public void StrokeRect(float x, float y, float width, float height)
        {
            CanvasStrokeRect(x, y, width, height);
        }

        [JSImport("canvas_clearRect", "BlazorCanvas")]
        internal static partial void CanvasClearRect(float x, float y, float width, float height);
        public void ClearRect(float x, float y, float width, float height)
        {
            CanvasClearRect(x, y, width, height);
        }

        [JSImport("canvas_closePath", "BlazorCanvas")]
        internal static partial void CanvasClosePath();
        public void ClosePath()
        {
            CanvasClosePath();
        }

        [JSImport("canvas_moveTo", "BlazorCanvas")]
        internal static partial void CanvasMoveTo(float x, float y);
        public void MoveTo(float x, float y)
        {
            CanvasMoveTo(x, y);
        }

        [JSImport("canvas_lineTo", "BlazorCanvas")]
        internal static partial void CanvasLineTo(float x, float y);
        public void LineTo(float x, float y)
        {
            CanvasLineTo(x, y);
        }

        [JSImport("canvas_fillStyle", "BlazorCanvas")]
        internal static partial void CanvasFillStyle(string defaultFillColor);
        public void FillStyle(string defaultFillColor)
        {
            CanvasFillStyle(defaultFillColor);
        }

        [JSImport("canvas_strokeStyle", "BlazorCanvas")]
        internal static partial void CanvasStrokeStyle(string defaultBorderColor);
        public void StrokeStyle(string defaultBorderColor)
        {
            CanvasStrokeStyle(defaultBorderColor);
        }

        [JSImport("canvas_lineWidth", "BlazorCanvas")]
        internal static partial void CanvasLineWidth(float defaultBorderThickness);
        public void LineWidth(float defaultBorderThickness)
        {
            CanvasLineWidth(defaultBorderThickness);
        }

        [JSImport("canvas_transform", "BlazorCanvas")]
        internal static partial void CanvasTransform(float a, float b, float c, float d, float e, float f);
        public void Transform(float a, float b, float c, float d, float e, float f)
        {
            CanvasTransform(a, b, c, d, e, f);
        }

        [JSImport("canvas_polyline", "BlazorCanvas")]
        internal static partial void CanvasPolyLine(double[] vertices);
        public void PolyLine(double[] vertices)
        {
            CanvasPolyLine(vertices);
        }

        public void GlobalCompositeOperation(CompositeOperation operation)
        {
            var operationName = string.Empty;
            switch (operation)
            {
                case CompositeOperation.SourceOver:
                    operationName = "source-over";
                    break;
                case CompositeOperation.SourceAtop:
                    operationName = "source-atop";
                    break;
                case CompositeOperation.SourceIn:
                    operationName = "source-in";
                    break;
                case CompositeOperation.SourceOut:
                    operationName = "source-out";
                    break;
                case CompositeOperation.DestinationOver:
                    operationName = "destination-over";
                    break;
                case CompositeOperation.DestinationAtop:
                    operationName = "destination-atop";
                    break;
                case CompositeOperation.DestinationIn:
                    operationName = "destination-in";
                    break;
                case CompositeOperation.DestinationOut:
                    operationName = "destination-out";
                    break;
                case CompositeOperation.Lighter:
                    operationName = "lighter";
                    break;
                case CompositeOperation.Copy:
                    operationName = "copy";
                    break;
                case CompositeOperation.Xor:
                    operationName = "xor";
                    break;
            }

            CanvasGlobalCompositeOperation(operationName);
        }
        [JSImport("canvas_globalCompositeOperation", "BlazorCanvas")]
        internal static partial void CanvasGlobalCompositeOperation(string operationName);

        #region Text

        [JSImport("canvas_textAlign", "BlazorCanvas")]

        internal static partial void CanvasTextAlign(string textAlign);
        public void TextAlign(string textAlign)
        {
            CanvasTextAlign(textAlign);
        }

        [JSImport("canvas_fillText", "BlazorCanvas")]

        internal static partial void CanvasFillText(string text, float x, float y);
        public void FillText(string text, float x, float y)
        {
            CanvasFillText(text, x, y);
        }


        [JSImport("canvas_font", "BlazorCanvas")]
        internal static partial void CanvasFont(string font);
        public void Font(string font)
        {
            CanvasFont(font);
        }


        [JSImport("canvas_wrapText", "BlazorCanvas")]
        internal static partial void CanvasWrapText(string text, float x, float y, float maxWidth, float maxHeight, float lineHeight);
        public void WrapText(string text, float x, float y, float maxWidth, float maxHeight, float lineHeight)
        {
            CanvasWrapText(text, x, y, maxWidth, maxHeight, lineHeight);
        }


        #endregion

        #region Images
        public async Task LoadImage(string src)
        {
            if (module == null)
            {
                return;
            }

            await module.InvokeVoidAsync("canvas_loadImage", src);
        }


        [JSImport("canvas_drawImageData", "BlazorCanvas")]
        internal static partial void CanvasDrawImageData(int x, int y, int w, int h, [JSMarshalAs<JSType.MemoryView>] ArraySegment<byte> data);
        public void DrawImageData(int x, int y, int w, int h, ArraySegment<byte> data)
        {
            CanvasDrawImageData(x, y, w, h, data);
        }

        [JSImport("canvas_setImageBuffer", "BlazorCanvas")]
        internal static partial void CanvasSetImagebuffer([JSMarshalAs<JSType.MemoryView>] ArraySegment<Int32> data);
        public void SetImageBuffer(ArraySegment<Int32> data)
        {
            CanvasSetImagebuffer(data);
        }

        [JSImport("canvas_drawImageBuffer", "BlazorCanvas")]
        internal static partial void CanvasDrawImageBuffer(int x, int y, int w, int h);
        public void DrawImageBuffer(int x, int y, int w, int h)
        {
            CanvasDrawImageBuffer(x, y, w, h);
        }

        [JSImport("canvas_drawImage", "BlazorCanvas")]
        internal static partial void CanvasDrawImage(string src, double x, double y, double w, double h);
        public void DrawImage(string src, float x, float y, float width, float height)
        {
            CanvasDrawImage(src, x, y, width, height);
        }

        [JSImport("canvas_drawImagePortion", "BlazorCanvas")]
        internal static partial void CanvasDrawImagePortion(string src, double sx, double sy, double sw, double sh, double dx, double dy, double dw, double dh);
        public void DrawImage(string src, float sx, float sy, float sw, float sh, float dx, float dy, float dw, float dh)
        {
            CanvasDrawImagePortion(src, sx, sy, sw, sh, dx, dy, dw, dh);
        }

        #endregion
    }
}