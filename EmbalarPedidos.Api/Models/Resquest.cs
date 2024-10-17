namespace EmbalarPedidos.Api.Models
{
	public record Request
	{ 
		public List<Pedido> Pedidos { get; set; }
	}
}
