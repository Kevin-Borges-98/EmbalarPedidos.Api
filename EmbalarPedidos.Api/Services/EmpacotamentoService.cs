using EmbalarPedidos.Api.Interfaces;
using EmbalarPedidos.Api.Models;

namespace EmbalarPedidos.Api.Services
{
	public class EmpacotamentoService : IEmpacotamentoService
	{
		private List<Caixa> _caixasDisponiveis;

		public EmpacotamentoService()
		{
			_caixasDisponiveis = new List<Caixa>
			{
				new Caixa("Caixa 1", 30, 40, 80),
				new Caixa("Caixa 2", 80, 50, 40),
				new Caixa("Caixa 3", 50, 80, 60)
			};
		}

		public List<Response> EmpacotarPedidos(List<Pedido> pedidos) => pedidos.Select(EmpacotarPedido).ToList();

		private Response EmpacotarPedido(Pedido pedido)
		{
			var caixasUsadas = new List<CaixaResponse>();
			var produtosNaoEmpacotados = new List<Produto>();

			var caixasComVolumeDisponivel = _caixasDisponiveis.ToDictionary(caixa => caixa.Caixa_Id, caixa => caixa.Volume);

			foreach (var produto in pedido.Produtos)
			{
				var volumeProduto = CalcularVolume(produto.Dimensoes);

				var caixaEscolhida = caixasComVolumeDisponivel.Keys.FirstOrDefault(c =>
					caixasComVolumeDisponivel[c] >= volumeProduto &&
					_caixasDisponiveis.First(caixa => caixa.Caixa_Id == c).Altura >= produto.Dimensoes.Altura &&
					_caixasDisponiveis.First(caixa => caixa.Caixa_Id == c).Largura >= produto.Dimensoes.Largura &&
					_caixasDisponiveis.First(caixa => caixa.Caixa_Id == c).Comprimento >= produto.Dimensoes.Comprimento);

				if (caixaEscolhida != null)
				{
					caixasComVolumeDisponivel[caixaEscolhida] -= (int)volumeProduto; // Fazendo o cast explícito para int
					AdicionarProdutoNaCaixa(caixaEscolhida, produto, caixasUsadas);
				}
				else
					produtosNaoEmpacotados.Add(produto);

			}

			AdicionarProdutosNaoEmpacotados(produtosNaoEmpacotados, caixasUsadas);

			return new Response
			{
				Pedido_Id = pedido.Pedido_Id,
				Caixas = caixasUsadas
			};
		}

		private double CalcularVolume(Dimensao dimensoes)
		{
			return dimensoes.Altura * dimensoes.Largura * dimensoes.Comprimento;
		}

		private void AdicionarProdutoNaCaixa(string caixaId, Produto produto, List<CaixaResponse> caixasUsadas)
		{
			var caixaResposta = caixasUsadas.FirstOrDefault(c => c.Caixa_Id == caixaId) ??
								CriarNovaCaixaResponse(caixaId, caixasUsadas);

			caixaResposta.Produtos.Add(produto.Produto_Id);
		}

		private CaixaResponse CriarNovaCaixaResponse(string caixaId, List<CaixaResponse> caixasUsadas)
		{
			var novaCaixaResponse = new CaixaResponse(caixaId, new List<string>(), null);
			caixasUsadas.Add(novaCaixaResponse);
			return novaCaixaResponse;
		}

		private void AdicionarProdutosNaoEmpacotados(List<Produto> produtosNaoEmpacotados, List<CaixaResponse> caixasUsadas)
		{
			foreach (var produto in produtosNaoEmpacotados)
			{
				caixasUsadas.Add(new CaixaResponse(
					null,
					new List<string> { produto.Produto_Id },
					"Produto não cabe em nenhuma caixa disponível.")
				);
			}
		}
	}
}
