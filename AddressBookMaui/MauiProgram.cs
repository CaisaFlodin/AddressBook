using AddressBookMaui.Pages;
using AddressBookMaui.ViewModels;
using Shared.Interfaces;
using Shared.Services;

namespace AddressBookMaui
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

         
            builder.Services.AddSingleton<ContactService>();
            builder.Services.AddSingleton<IFileService, FileService>();

            builder.Services.AddSingleton<ContactListViewModel>();
            builder.Services.AddSingleton<ContactListPage>();

            builder.Services.AddSingleton<ContactAddViewModel>();
            builder.Services.AddSingleton<ContactAddPage>();

            builder.Services.AddSingleton<ContactEditViewModel>();
            builder.Services.AddSingleton<ContactEditPage>();

            return builder.Build();
        }
    }
}
