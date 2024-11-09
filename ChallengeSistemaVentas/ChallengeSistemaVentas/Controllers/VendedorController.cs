using ChallengeSistemaVentas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChallengeSistemaVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VendedorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("GetAllVendedores")]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetAllVendedores()
        {
            string relativePath = _configuration.GetValue<string>("VendedoresPathJson");
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if (!System.IO.File.Exists(jsonPath))
                return NotFound("No se encontró el archivo de Vendedores");

            var jsonData = await System.IO.File.ReadAllTextAsync(jsonPath);

            //var articulos = JsonConvert.DeserializeObject<List<Articulo>>(jsonData);
            var vendedoresResponse = JsonConvert.DeserializeObject<VendedoresResponse>(jsonData);

            return Ok(vendedoresResponse.vendedores);
        }
        
    }
}
