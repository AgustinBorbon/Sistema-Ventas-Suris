namespace ChallengeSistemaVentas.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string descripcion { get; set; }
    }

    //Igual que en artículos, cree una clase contenedora por la estructura .json de vendedores con una raíz vendedores
    public class VendedoresResponse
    {
        public List<Vendedor> vendedores { get; set; }
    }
}
