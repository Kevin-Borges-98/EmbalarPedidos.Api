namespace EmbalarPedidos.Api.Models
{
	public record Response
	{
		public int Pedido_Id { get; set; }
		public List<CaixaResponse> Caixas { get; set; }
	}
}
