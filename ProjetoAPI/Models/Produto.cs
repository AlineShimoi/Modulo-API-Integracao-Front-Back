using System.ComponentModel.DataAnnotations;
namespace ProjetoApi.ProdutoModels;

public class Produto
{
    public int Id {get; set;}
    
    [Required(ErrorMessage ="Campo obrigatório.")]
    public string? Nome {get; set;}

    [Range(0.01, 1000, ErrorMessage ="O preço deve estar entre 0 e 1000.")]
    public decimal Preco {get; set;}
}