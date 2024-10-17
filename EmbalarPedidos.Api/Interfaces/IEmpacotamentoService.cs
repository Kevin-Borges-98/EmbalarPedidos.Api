using EmbalarPedidos.Api.Models;

namespace EmbalarPedidos.Api.Interfaces
{
	public interface IEmpacotamentoService
	{
		List<Response> EmpacotarPedidos(List<Pedido> pedidos);
	}
}
