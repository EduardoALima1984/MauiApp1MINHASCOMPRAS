using System.Globalization;
using MauiApp1MINHASCOMPRAS.Modelos;
using MauiApp1MINHASCOMPRAS.ajudadores;

namespace MauiApp1MINHASCOMPRAS.Visuais;

[QueryProperty(nameof(ProdutoId), "id")]
public partial class EditarProduto : ContentPage
{
    private readonly SQLiteDatabaseHelper _db;
    private Produto? _produto;
    public string ProdutoId { get; set; } = string.Empty;

    public EditarProduto(SQLiteDatabaseHelper db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (int.TryParse(ProdutoId, out var id))
        {
            _produto = await _db.GetById(id);
            if (_produto != null)
            {
                txtDesc.Text = _produto.Descricao;
                txtQtd.Text = _produto.Quantidade.ToString(CultureInfo.CurrentCulture);
                txtPreco.Text = _produto.Preco.ToString(CultureInfo.CurrentCulture);
            }
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (_produto == null) return;

        _produto.Descricao = txtDesc.Text?.Trim() ?? string.Empty;
        _ = double.TryParse(txtQtd.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var qtd);
        _produto.Quantidade = qtd;

        _ = double.TryParse(txtPreco.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var preco);
        _produto.Preco = preco;

        await _db.Update(_produto);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelarClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("..");
}