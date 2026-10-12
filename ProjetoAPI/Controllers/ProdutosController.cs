using Microsoft.AspNetCore.Mvc;
using ProjetoApi.ProdutoModels;
using ProjetoApi.ProdutosServices;

namespace ProjetoApi.ProdutosControllers;

[ApiController]
[Route("api/[controller]")]

public class ProdutosController : ControllerBase
{
   private readonly IProdutosService _service;
    public ProdutosController(IProdutosService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> ListarProdutos()
    {
        try
        {
            var produtos = await _service.ListarProdutosAsync();
            return Ok(produtos);
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarProdutoPorId(int id)
    {
        try
        {
            var produto = await _service.BuscarProdutoPorIdAsync(id);
            if(produto == null)
            {
                return NotFound($"Produto {id} não encontrado.");
            }
            return Ok(produto);
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto([FromBody] Produto produto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var produtoCriado = await _service.CriarProdutoAsync(produto);
            return CreatedAtAction(nameof(BuscarProdutoPorId), new {id = produto.Id}, produtoCriado);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarProduto([FromBody] Produto produtoAtualizado)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try 
        {
            var produto = await _service.AtualizarProdutoAsync(produtoAtualizado);
            if(produto == null)
            {
                return NotFound($"Produto {produtoAtualizado.Nome} não encontrado.");
            }
            return Ok($"Informações do produto {produtoAtualizado.Nome} atualizadas.");
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverProduto(int id)
    {
        try
        {
            var removido = await _service.RemoverProdutoAsync(id);
            if (!removido)
            {
                return NotFound(new { mensagem = $"Produto {id} não encontrado." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}