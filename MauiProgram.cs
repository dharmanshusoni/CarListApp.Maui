using CarListApp.Maui.Services;
using CarListApp.Maui.ViewModels;
using CarListApp.Maui.Views;
using Microsoft.Extensions.Logging;

namespace CarListApp.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbpath = Path.Combine(FileSystem.AppDataDirectory, "Cars.db3");

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //Services
            builder.Services.AddSingleton( s => ActivatorUtilities.CreateInstance<CarService>(s, dbpath) );

            //ViewModels
            builder.Services.AddSingleton<CarListViewModel>();
            builder.Services.AddSingleton<CarDetailsViewModel>();

            //Pages
            builder.Services.AddSingleton<CarDetailsPage>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
