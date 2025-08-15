using System.Globalization;
using MauiApp1MINHASCOMPRAS.Modelos;
using MauiApp1MINHASCOMPRAS.ajudadores;

namespace MauiApp1MINHASCOMPRAS.Visuais;

public partial class NovoProduto : ContentPage
{
    private readonly SQLiteDatabaseHelper _db;
    public NovoProduto(SQLiteDatabaseHelper db)
    {
        InitializeComponent();
        _db = db;
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDesc.Text))
        {
            await DisplayAlert("Atenção", "Informe a descrição.", "OK");
            return;
        }

        _ = double.TryParse(txtQtd.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var qtd);
        _ = double.TryParse(txtPreco.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var preco);

        var p = new Produto
        {
            Descricao = txtDesc.Text!.Trim(),
            Quantidade = qtd,
            Preco = preco
        };

        await _db.Insert(p);
        await Shell.Current.GoToAsync(".."); // volta para a lista
    }

    private async void OnCancelarClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("..");
}