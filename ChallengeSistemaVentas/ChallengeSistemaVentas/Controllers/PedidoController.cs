using ChallengeSistemaVentas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ChallengeSistemaVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PedidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("PostPedido")]
        public async Task<ActionResult> PostPedido([FromBody] Pedido newPedido)
        {
            string relativePath = _configuration.GetValue<string>("PedidosPathJson");
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if (!System.IO.File.Exists(jsonPath))
                return NotFound("No se encontró el archivo de Pedidos");

            var jsonData = await System.IO.File.ReadAllTextAsync(jsonPath);
            var pedidosResponse = JsonConvert.DeserializeObject<PedidosResponse>(jsonData) ?? new PedidosResponse { pedidos = new List<Pedido>() };

            // En esta parte sí use chatgpt, me estaba haciendo un lío intentando traer el int para el
            // pedido, y como hacerlo autoincremental.
            int newId = pedidosResponse.pedidos.Any() ? pedidosResponse.pedidos.Max(p => p.id) + 1 : 1;
            newPedido.id = newId;

            pedidosResponse.pedidos.Add(newPedido);

            var updatedJsonData = JsonConvert.SerializeObject(pedidosResponse, Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(jsonPath, updatedJsonData);

            return Ok(new { message = "Pedido guardado exitosamente", pedidoId = newId });
        }
    }
}
