public class Caixa
{
	public string Caixa_Id { get; set; }
	public int Altura { get; set; }
	public int Largura { get; set; }
	public int Comprimento { get; set; }
	public int Volume => Altura * Largura * Comprimento; 
	public int VolumeDisponivel { get; set; } 

	public Caixa(string caixaId, int altura, int largura, int comprimento)
	{
		Caixa_Id = caixaId;
		Altura = altura;
		Largura = largura;
		Comprimento = comprimento;
		VolumeDisponivel = Volume; 
	}
}
