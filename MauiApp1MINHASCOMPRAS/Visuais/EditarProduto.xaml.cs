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

    public EditarProduto()
    {
        InitializeComponent();
        _db = App.Db;
    }


    public EditarProduto(SQLiteDatabaseHelper db, Produto? produto = null)
    {
        InitializeComponent();
        _db = db;

        if (produto != null)
        {
            _produto = produto;
            BindingContext = produto;
            PreencherCampos(produto);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


        if (_produto is null && int.TryParse(ProdutoId, out var id))
        {
            _produto = await _db.GetById(id);
        }


        if (_produto is null && BindingContext is Produto bc)
        {
            _produto = bc;
        }

        if (_produto != null)
        {
            BindingContext = _produto;
            PreencherCampos(_produto);
        }
    }

    private void PreencherCampos(Produto p)
    {
        txt_descricao.Text = p.Descricao;
        txt_quantidade.Text = p.Quantidade.ToString(CultureInfo.CurrentCulture);
        txt_preco.Text = p.Preco.ToString(CultureInfo.CurrentCulture);
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (_produto == null) return;

        _produto.Descricao = txt_descricao.Text?.Trim() ?? string.Empty;

        _ = double.TryParse(txt_quantidade.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var qtd);
        _produto.Quantidade = qtd;

        _ = double.TryParse(txt_preco.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var preco);
        _produto.Preco = preco;

        await _db.Update(_produto);

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}