using ProjetoApi.ProdutosControllers;
using ProjetoApi.ProdutoModels;
using ProjetoApi.ProdutosRepository;

namespace ProjetoApi.ProdutosServices;

public class ProdutosService : IProdutosService
{
    private readonly IProdutosRepository _repository;
    public ProdutosService(IProdutosRepository repository)
    {
        _repository = repository;
    }

     public async Task<List<Produto>> ListarProdutosAsync()
    {
        return await _repository.ListarProdutosAsync();
    }
    public async Task<Produto?> BuscarProdutoPorIdAsync(int id)
    {
        return await _repository.BuscarProdutoPorIdAsync(id);
    }
    public async Task<Produto> CriarProdutoAsync(Produto produtos)
    {
        return await _repository.CriarProdutoAsync(produtos);
    }
    public async Task<Produto?> AtualizarProdutoAsync(Produto produtoAtualizado)
    {
        return await _repository.AtualizarProdutoAsync(produtoAtualizado);
    }
    public async Task<bool> RemoverProdutoAsync(int id)
    {
        return await _repository.RemoverProdutoAsync(id);
    }   
}