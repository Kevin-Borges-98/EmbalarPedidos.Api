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
				new Caixa("Caixa 1", 30, 40, 80 ),
				new Caixa("Caixa 2", 80, 50, 40 ),
				new Caixa("Caixa 3", 50, 80, 60 )
			};
		}

		public List<Response> EmpacotarPedidos(List<Pedido> pedidos) => pedidos.Select(EmpacotarPedido).ToList();

		private Response EmpacotarPedido(Pedido pedido)
		{
			var caixasUsadas = new List<CaixaResponse>();

			var produtosNaoEmpacotados = new List<Produto>();

			foreach (var produto in pedido.Produtos)
			{
				var caixaEscolhida = EncontrarCaixaAdequada(produto);

				if (caixaEscolhida is not null)
					AdicionarProdutoNaCaixa(caixaEscolhida, produto, caixasUsadas);
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

		private Caixa EncontrarCaixaAdequada(Produto produto)
		{
			return _caixasDisponiveis.FirstOrDefault(c =>
				c.Altura >= produto.Dimensoes.Altura &&
				c.Largura >= produto.Dimensoes.Largura &&
				c.Comprimento >= produto.Dimensoes.Comprimento);
		}

		private void AdicionarProdutoNaCaixa(Caixa caixaEscolhida, Produto produto, List<CaixaResponse> caixasUsadas)
		{
			var caixaResposta = caixasUsadas.FirstOrDefault(c => c.Caixa_Id == caixaEscolhida.Caixa_Id) ??
								CriarNovaCaixaResponse(caixaEscolhida, caixasUsadas);

			caixaResposta.Produtos.Add(produto.Produto_Id);
		}

		private CaixaResponse CriarNovaCaixaResponse(Caixa caixaEscolhida, List<CaixaResponse> caixasUsadas)
		{
			var novaCaixaResponse = new CaixaResponse(caixaEscolhida.Caixa_Id, new List<string>(),null);
			
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
