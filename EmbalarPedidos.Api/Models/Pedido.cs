using System.ComponentModel.DataAnnotations;

namespace EmbalarPedidos.Api.Models
{
	public record Pedido
	{
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "O ID do pedido deve ser maior que zero.")]
		public int Pedido_Id { get; set; }

		[Required(ErrorMessage = "A lista de produtos é obrigatória.")]
		public List<Produto> Produtos { get; set; }
	}
}
