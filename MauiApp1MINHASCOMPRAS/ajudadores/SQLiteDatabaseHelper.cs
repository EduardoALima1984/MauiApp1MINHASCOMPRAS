using MauiApp1MINHASCOMPRAS.Modelos;
using SQLite;

namespace MauiApp1MINHASCOMPRAS.ajudadores
{
    public class SQLiteDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) =>
            _conn.InsertAsync(p);

        public Task<int> Update(Produto p)
        {
            const string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";
            return _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<int> Delete(int id)
        {
            const string sql = "DELETE FROM Produto WHERE Id = ?";
            return _conn.ExecuteAsync(sql, id);
        }

        public Task<List<Produto>> GetAll() =>
            _conn.Table<Produto>().ToListAsync();

        public async Task<Produto?> GetById(int id)
        {
            var item = await _conn.FindAsync<Produto>(id);
            return item;
        }
        public Task<List<Produto>> Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return GetAll();

            const string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";
            return _conn.QueryAsync<Produto>(sql, $"%{q}%");
        }
        public async Task<int> DeleteAllProdutos()
        {
            return await _conn.ExecuteAsync("DELETE FROM Produto");
        }
    }
}