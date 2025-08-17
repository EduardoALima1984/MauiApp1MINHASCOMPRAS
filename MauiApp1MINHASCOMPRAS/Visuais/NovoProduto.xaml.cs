using System.Globalization;
using MauiApp1MINHASCOMPRAS.Modelos;
using MauiApp1MINHASCOMPRAS.ajudadores;

namespace MauiApp1MINHASCOMPRAS.Visuais;

public partial class NovoProduto : ContentPage
{
    private readonly SQLiteDatabaseHelper _db;

    public NovoProduto()
    {
        InitializeComponent();
        _db = App.Db;
    }

    public NovoProduto(SQLiteDatabaseHelper db)
    {
        InitializeComponent();
        _db = db;
    }


    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "Ok");
        }
    }
}