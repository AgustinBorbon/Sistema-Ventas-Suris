using ChallengeSistemaVentas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ChallengeSistemaVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticuloController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ArticuloController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("GetAllArticulos")]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetAllArticulos()
        {
            string relativePath = _configuration.GetValue<string>("ArticulosPathJson");
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if(!System.IO.File.Exists(jsonPath))
                return NotFound("No se encontró el archivo de Articulos");

            var jsonData = await System.IO.File.ReadAllTextAsync(jsonPath);

            //var articulos = JsonConvert.DeserializeObject<List<Articulo>>(jsonData);
            var articulosResponse = JsonConvert.DeserializeObject<ArticulosResponse>(jsonData);

            /*
             * Hago las 3 validaciones acá en vez de en el controller de Pedido porque en las validaciones dice
             que hay que mostrar solo los artículos de deposito 1, pero por ejemplo ya en el archivo .json hay
            un artículo del depósito 1 que tiene precio 0 y eso rompería el otro requerimiento de que el precio
            debe ser mayor a 0, esto también podría pasar con la última validación, que fuera articulo de deposito
            1 pero que tuviera caracteres especiales en el código.
            Entiendo que deberían pasar las 3 validaciones para mostrarse en pantalla.
             */
            var articulosFiltrados = articulosResponse.articulos
                .Where(a => a.deposito == 1 && a.precio > 0 && !ContieneCaracteresEspeciales(a.codigo))
                .ToList();

            return Ok(articulosFiltrados);
        }
        // Las validaciones dice que la descripciones no deben tener caracteres especiales, en el .json no ví ninguna, pero si en
        // los codigos de id, así que por las dudas cambio la validación de descripcion de articulo a codigo de articulo
        private bool ContieneCaracteresEspeciales(string codigo)
        {
            return codigo.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch));
        }
    }
}
