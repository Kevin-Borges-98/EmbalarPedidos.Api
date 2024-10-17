using EmbalarPedidos.Api.Models;
using EmbalarPedidos.Api.Services;
using Xunit;

namespace EmbalarPedidos.Tests
{
	public class EmpacotamentoServiceTests
	{
		private readonly EmpacotamentoService _service;

		public EmpacotamentoServiceTests()
		{
			_service = new EmpacotamentoService();
		}

		[Fact]
		public void EmpacotarPedidos_DeveRetornarVazio_QuandoNenhumPedidoForFornecido()
		{
			// Arrange
			var pedidos = new List<Pedido>();

			// Act
			var resultado = _service.EmpacotarPedidos(pedidos);

			// Assert
			Assert.Empty(resultado); // Verifica se o resultado está vazio
		}

		[Fact]
		public void EmpacotarPedidos_DeveRetornarCaixas_QuandoPedidosValidosForemFornecidos()
		{
			// Arrange
			var pedidos = new List<Pedido>
			{
				new Pedido
				{
					Pedido_Id = 1,
					Produtos = new List<Produto>
					{
						new Produto { Produto_Id = "PS5", Dimensoes = new Dimensao { Altura = 40, Largura = 10, Comprimento = 25 } },
						new Produto { Produto_Id = "Volante", Dimensoes = new Dimensao { Altura = 40, Largura = 30, Comprimento = 30 } }
					}
				}
			};

			// Act
			var resultado = _service.EmpacotarPedidos(pedidos);

			// Assert
			Assert.NotNull(resultado); // Verifica se há um pedido no resultado
			Assert.Equal(1, resultado[0].Pedido_Id); // Verifica se o ID do pedido é correto
			Assert.NotEmpty(resultado[0].Caixas); // Verifica se há caixas no resultado
			Assert.Contains(resultado[0].Caixas, c => c.Caixa_Id == "Caixa 2");
		}

		[Fact]
		public void EmpacotarPedidos_DeveLidarComProdutosQueNaoCabemEmNenhumaCaixa()
		{
			// Arrange
			var pedidos = new List<Pedido>
			{
				new Pedido
				{
					Pedido_Id = 2,
					Produtos = new List<Produto>
					{
						new Produto { Produto_Id = "Cadeira Gamer", Dimensoes = new Dimensao { Altura = 120, Largura = 60, Comprimento = 70 } }
					}
				}
			};

			// Act
			var resultado = _service.EmpacotarPedidos(pedidos);

			// Assert
			Assert.NotNull(resultado); // Verifica se há um pedido no resultado
			Assert.Equal(2, resultado[0].Pedido_Id); // Verifica se o ID do pedido é correto
			Assert.NotNull(resultado[0].Caixas); // Verifica se há uma caixa no resultado
			Assert.Null(resultado[0].Caixas[0].Caixa_Id); // Caixa_Id deve ser nulo
			Assert.Equal("Produto não cabe em nenhuma caixa disponível.", resultado[0].Caixas[0].Observacao); // Verifica a observação
		}

		[Fact]
		public void EmpacotarPedidos_DeveRetornarMultipleCaixas_QuandoProdutosValidosForemFornecidos()
		{
			// Arrange
			var pedidos = new List<Pedido>
			{
				new Pedido
				{
					Pedido_Id = 3,
					Produtos = new List<Produto>
					{
						new Produto { Produto_Id = "Monitor", Dimensoes = new Dimensao { Altura = 50, Largura = 60, Comprimento = 20 } },
						new Produto { Produto_Id = "Notebook", Dimensoes = new Dimensao { Altura = 2, Largura = 35, Comprimento = 25 } }
					}
				}
			};

			// Act
			var resultado = _service.EmpacotarPedidos(pedidos);

			// Assert
			Assert.NotNull(resultado); // Verifica se há um pedido no resultado
			Assert.Equal(3, resultado[0].Pedido_Id); // Verifica se o ID do pedido é correto
			Assert.Equal(2, resultado[0].Caixas.Count); // Verifica se duas caixas foram utilizadas
		}

		[Fact]
		public void EmpacotarPedidos_NaoDeveAdicionarObservacao_QuandoNula()
		{
			// Arrange
			var pedidos = new List<Pedido>
			{
				new Pedido
				{
					Pedido_Id = 4,
					Produtos = new List<Produto>
					{
						new Produto { Produto_Id = "Mine Cadeira Gamer", Dimensoes = new Dimensao { Altura = 40, Largura = 40, Comprimento = 40 } }
					}
				}
			};

			// Act
			var resultado = _service.EmpacotarPedidos(pedidos);

			// Assert
			Assert.NotNull(resultado); // Verifica se há um pedido no resultado
			Assert.Null(resultado[0].Caixas[0].Observacao); // A observação não deve ser exibida
		}
	}
}