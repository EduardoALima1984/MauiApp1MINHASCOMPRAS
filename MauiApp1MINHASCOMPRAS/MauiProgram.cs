using Microsoft.Maui.Storage;
using MauiApp1MINHASCOMPRAS.ajudadores;
using MauiApp1MINHASCOMPRAS.Visuais;

namespace MauiApp1MINHASCOMPRAS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Helper do SQLite como singleton
        builder.Services.AddSingleton(sp =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "minhascompras.db3");
            return new SQLiteDatabaseHelper(dbPath);
        });

        // Páginas
        builder.Services.AddTransient<ListaProduto>();
        builder.Services.AddTransient<NovoProduto>();
        builder.Services.AddTransient<EditarProduto>();

        return builder.Build();
    }
}