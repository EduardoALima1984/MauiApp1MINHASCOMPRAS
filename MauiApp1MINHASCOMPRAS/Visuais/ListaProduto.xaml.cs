using System.Collections.ObjectModel;
using MauiApp1MINHASCOMPRAS.Modelos;
using MauiApp1MINHASCOMPRAS.ajudadores;

namespace MauiApp1MINHASCOMPRAS.Visuais;

public partial class ListaProduto : ContentPage
{
    private readonly SQLiteDatabaseHelper _db;
    public ObservableCollection<Produto> Produtos { get; } = new();

    public ListaProduto(SQLiteDatabaseHelper db)
    {
        InitializeComponent();
        _db = db;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarAsync();
    }

    private async Task CarregarAsync(string? filtro = null)
    {
        Produtos.Clear();
        var dados = string.IsNullOrWhiteSpace(filtro) ? await _db.GetAll() : await _db.Search(filtro);
        foreach (var p in dados)
            Produtos.Add(p);
    }

    private async void OnNovoClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(NovoProduto));

    private async void OnEditarInvoked(object sender, EventArgs e)
    {
        var item = (sender as SwipeItem)?.CommandParameter as Produto;
        if (item is null) return;
        await Shell.Current.GoToAsync($"{nameof(EditarProduto)}?id={item.Id}");
    }

    private async void OnExcluirInvoked(object sender, EventArgs e)
    {
        var item = (sender as SwipeItem)?.CommandParameter as Produto;
        if (item is null) return;

        var ok = await DisplayAlert("Excluir", $"Excluir \"{item.Descricao}\"?", "Sim", "Não");
        if (!ok) return;

        await _db.Delete(item.Id);
        await CarregarAsync(sbBusca.Text);
    }

    private async void OnBuscaChanged(object sender, TextChangedEventArgs e) =>
        await CarregarAsync(e.NewTextValue);
}