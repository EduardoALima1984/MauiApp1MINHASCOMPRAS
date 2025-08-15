using MauiApp1MINHASCOMPRAS.Visuais;

namespace MauiApp1MINHASCOMPRAS;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(NovoProduto), typeof(NovoProduto));
        Routing.RegisterRoute(nameof(EditarProduto), typeof(EditarProduto));
    }
}