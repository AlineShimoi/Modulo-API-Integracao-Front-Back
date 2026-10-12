using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ProjetoApi.ProdutoModels;

namespace ProjetoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Produto> Produtos {get;set;}
    }
}