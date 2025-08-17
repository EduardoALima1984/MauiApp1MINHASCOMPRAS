using System.Collections.ObjectModel;
using MauiApp1MINHASCOMPRAS.Modelos;
using MauiApp1MINHASCOMPRAS.ajudadores;

namespace MauiApp1MINHASCOMPRAS.Visuais;
public partial class ListaProduto : ContentPage
{
    public ListaProduto()
    { InitializeComponent();
    }
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new NovoProduto());
        }catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}