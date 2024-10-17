namespace EmbalarPedidos.Api.Models
{
	public record Caixa
	{
        public Caixa(string caixa_Id,
					 int altura, 
					 int largura,
					 int comprimento)
        {
			Caixa_Id = caixa_Id;
			Altura = altura;
			Largura = largura;
			Comprimento = comprimento;
        }

        public string Caixa_Id { get; set; }
		public int Altura { get; set; }
		public int Largura { get; set; }
		public int Comprimento { get; set; }
	}
}
