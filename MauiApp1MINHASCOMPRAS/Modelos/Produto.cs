﻿using SQLite;

namespace MauiApp1MINHASCOMPRAS.Modelos
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total => Quantidade * Preco;
    }
}
