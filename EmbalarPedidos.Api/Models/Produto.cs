using System.ComponentModel.DataAnnotations;

namespace EmbalarPedidos.Api.Models
{
	public record Produto
	{
		[Required(ErrorMessage = "O ID do produto é obrigatório.")]
		public string Produto_Id { get; set; }

		[Required(ErrorMessage = "As dimensões do produto são obrigatórias.")]
		public Dimensao Dimensoes { get; set; }
	}
}
