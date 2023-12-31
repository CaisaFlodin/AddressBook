using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Interfaces;
using Shared.Services;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<ContactService>();
                services.AddSingleton<MenuService>();
            }).Build();

            builder.Start();
            Console.Clear();

            var menuService = builder.Services.GetRequiredService<MenuService>();
            menuService.StartMenu();
        }
    }
}
