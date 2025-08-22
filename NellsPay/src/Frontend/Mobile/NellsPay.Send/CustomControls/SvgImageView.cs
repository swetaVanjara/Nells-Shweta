using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Svg.Skia;
using Microsoft.Maui.Controls;

namespace NellsPay.Send.CustomControls
{
    public class SvgImageView : SKCanvasView
    {
        public static readonly BindableProperty SvgUrlProperty =
            BindableProperty.Create(
                nameof(SvgUrl),
                typeof(string),
                typeof(SvgImageView),
                default(string),
                propertyChanged: OnSvgUrlChanged);

        public static readonly BindableProperty AspectProperty =
            BindableProperty.Create(
                nameof(Aspect),
                typeof(Aspect),
                typeof(SvgImageView),
                Aspect.AspectFit,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var view = (SvgImageView)bindable;
                    view.InvalidateSurface();
                });

        public string SvgUrl
        {
            get => (string)GetValue(SvgUrlProperty);
            set => SetValue(SvgUrlProperty, value);
        }

        public Aspect Aspect
        {
            get => (Aspect)GetValue(AspectProperty);
            set => SetValue(AspectProperty, value);
        }

        private SKSvg _svg;

        public SvgImageView()
        {
            PaintSurface += OnPaintSurface;
        }

        private static async void OnSvgUrlChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SvgImageView view && newValue is string url)
            {
                await view.LoadSvgAsync(url);
                view.InvalidateSurface();
            }
        }

        private async Task LoadSvgAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                var svgData = await httpClient.GetByteArrayAsync(url);
                using var stream = new MemoryStream(svgData);
                _svg = new SKSvg();
                _svg.Load(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading SVG: {ex.Message}");
                _svg = null;
            }
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            if (_svg?.Picture == null)
                return;

            var canvasWidth = e.Info.Width;
            var canvasHeight = e.Info.Height;
            var svgBounds = _svg.Picture.CullRect;

            float svgWidth = svgBounds.Width;
            float svgHeight = svgBounds.Height;

            float scaleX = canvasWidth / svgWidth;
            float scaleY = canvasHeight / svgHeight;

            float scale;
            float x = 0;
            float y = 0;

            switch (Aspect)
            {
                case Aspect.Fill:
                    canvas.Scale(scaleX, scaleY);
                    break;

                case Aspect.AspectFit:
                    scale = Math.Min(scaleX, scaleY);
                    x = (canvasWidth - svgWidth * scale) / 2f;
                    y = (canvasHeight - svgHeight * scale) / 2f;
                    canvas.Translate(x, y);
                    canvas.Scale(scale);
                    break;

                case Aspect.AspectFill:
                    scale = Math.Max(scaleX, scaleY);
                    x = (canvasWidth - svgWidth * scale) / 2f;
                    y = (canvasHeight - svgHeight * scale) / 2f;
                    canvas.Translate(x, y);
                    canvas.Scale(scale);
                    break;
            }

            canvas.DrawPicture(_svg.Picture);
        }
    }
}
