using ProjetoApi.ProdutoModels;
namespace ProjetoApi.ProdutosServices;

public interface IProdutosService
{
    Task<List<Produto>> ListarProdutosAsync();
    Task<Produto?> BuscarProdutoPorIdAsync(int id);
    Task<Produto> CriarProdutoAsync(Produto produto);
    Task<Produto?> AtualizarProdutoAsync(Produto produtoAtualizado);
    Task<bool> RemoverProdutoAsync(int id);
}