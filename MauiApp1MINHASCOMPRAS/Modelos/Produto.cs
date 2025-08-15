using SQLite;

namespace MauiApp1MINHASCOMPRAS.Modelos
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descrição { get; set; }
        public double Quantidade { get; set; }
        public double Preço { get; set; }
    }
}
