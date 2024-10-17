using System.ComponentModel.DataAnnotations;

namespace EmbalarPedidos.Api.Models
{
	public record Dimensao
	{
		[Required]
		public int Altura { get; set; }
		
		[Required]
		public int Largura { get; set; }
		
		[Required]
		public int Comprimento { get; set; }
	}
}
