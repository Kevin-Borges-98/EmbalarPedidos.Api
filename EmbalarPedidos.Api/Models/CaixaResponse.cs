using System.Text.Json.Serialization;

namespace EmbalarPedidos.Api.Models
{
	public record CaixaResponse
	{
        public CaixaResponse(
			string caixa_Id,
			List<string> produtos,
			string observacao)
        {
			Caixa_Id = caixa_Id;
			Produtos = produtos;
			Observacao = observacao;
        }

        public string Caixa_Id { get; set; }
		public List<string> Produtos { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string Observacao { get; set; }
	}
}
