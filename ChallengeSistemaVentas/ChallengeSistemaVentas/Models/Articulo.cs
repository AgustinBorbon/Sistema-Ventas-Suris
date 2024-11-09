namespace ChallengeSistemaVentas.Models
{
    public class Articulo
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int deposito { get; set; }

    }
    //Cree un contenedor porque en la estructura del Json hay una raíz que tiene la propiedad articulos y al
    //deserializarlo tiraba error en el swagger.
    public class ArticulosResponse
    {
        public List<Articulo> articulos { get; set; }
    }
}
