using EmbalarPedidos.Api.Interfaces;
using EmbalarPedidos.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmbalarPedidos.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class PedidosController : ControllerBase
	{
		[HttpPost]
		public IActionResult Empacotar(
			[FromServices] IEmpacotamentoService service,
			[FromBody] Request request)
		{
			if(!ModelState.IsValid) 
				return BadRequest(ModelState); 

			var response = service.EmpacotarPedidos(request.Pedidos);

			return Ok(response);
		}
	}
}