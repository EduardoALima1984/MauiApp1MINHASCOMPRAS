using MauiApp1MINHASCOMPRAS.Modelos;
using SQLite;

namespace MauiApp1MINHASCOMPRAS.ajudadores
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path) 
        { 
           _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait
        }

        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);

        }

        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descrição=?, Quantidade=?, Preço=? Where Id=?";
        return _conn.QueryAsync<Produto>(sql, p.Descrição, p.Quantidade, p.Preço, p.Id );
        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        
        }

        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * Produto  Where Descrição LIKE `%" +q + "%`";
            return _conn.QueryAsync<Produto>(sql);

        }

    }
}
