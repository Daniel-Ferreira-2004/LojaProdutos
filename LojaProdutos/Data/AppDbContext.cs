using LojaProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutosModel> Produtos { get; set; }
    }
}
