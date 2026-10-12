using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoApi.ProdutoModels;
using ProjetoApi.ProdutosControllers;

namespace ProjetoApi.ProdutosRepository;

public class ProdutosRepository : IProdutosRepository
{
    private readonly AppDbContext _context;
    public ProdutosRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Produto>> ListarProdutosAsync()
    {
        return await _context.Produtos.ToListAsync();
    }
    public async Task<Produto?> BuscarProdutoPorIdAsync(int id)
    {   
        return await _context.Produtos.FindAsync(id);
    }
    public async Task<Produto> CriarProdutoAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }
    public async Task<Produto?> AtualizarProdutoAsync(Produto produtoAtualizado)
    {
        var produtoFiltrado = await _context.Produtos.FindAsync(produtoAtualizado.Id);
        if(produtoFiltrado == null)
        {
            return null;
        }

        produtoFiltrado.Nome = produtoAtualizado.Nome;
        produtoFiltrado.Preco = produtoAtualizado.Preco;

        await _context.SaveChangesAsync();
        return produtoFiltrado;
    }
    public async Task<bool> RemoverProdutoAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if(produto == null)
        {
            return false;
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }
}