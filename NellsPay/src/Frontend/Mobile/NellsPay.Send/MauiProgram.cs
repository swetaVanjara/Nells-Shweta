
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;
using ZXing.Net.Maui.Controls;
namespace NellsPay.Send;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .UseSkiaSharp()
          .ConfigureMauiHandlers(handlers =>
          {
              handlers.AddHandler<CameraView, ZXing.Net.Maui.CameraViewHandler>();
          })
            .ConfigureSyncfusionCore()
            
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddFont("SFPRODISPLAYBLACKITALIC.OTF", "SFBlackItalic");
                fonts.AddFont("SFPRODISPLAYBOLD.OTF", "SFBlackBold");
                fonts.AddFont("SFPRODISPLAYMEDIUM.OTF", "SFBlackMedium");
                fonts.AddFont("SFPRODISPLAYREGULAR.OTF", "SFRegular");

                fonts.AddFont("Figtree-Light.ttf", "FLight");
                fonts.AddFont("Figtree-Regular.ttf", "FRegular");
                fonts.AddFont("Figtree-Medium.ttf", "FMedium");
                fonts.AddFont("Figtree-SemiBold.ttf", "FSemBold");
                fonts.AddFont("Figtree-Bold.ttf", "FBold");
                fonts.AddFont("Figtree-ExtraBold.ttf", "FExBold");

                fonts.AddFont("fa-brands.ttf", "FontAwsomeBrand");
				fonts.AddFont("fa-regular.ttf", "FontAwsomeRegular");
				fonts.AddFont("fa-solid.ttf", "FontAwsomeSolid");
                fonts.AddFont("fontello2.ttf", "fontelloicon");

                fonts.AddFont("fontellolast.ttf", "Ficons");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSendServices();
        builder.UseBarcodeReader();

        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
      {
#if ANDROID
    handler.PlatformView.Background = null; // removes underline
#endif
      });

#if IOS
builder.ConfigureMauiHandlers(handlers =>
{
    handlers.AddHandler(typeof(NellsPay.Send.Helpers.CustomWebView), typeof(NellsPay.Send.Platforms.iOS.Handlers.CustomWebViewHandler));
});
#endif
        return builder.Build();
	}
}
