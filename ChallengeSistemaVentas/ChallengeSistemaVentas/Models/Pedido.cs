namespace ChallengeSistemaVentas.Models
{
    public class Pedido
    {
        public int id {  get; set; }
        public Vendedor vendedor { get; set; }
        public List<Articulo> articulos { get; set; }

    }
    
        public class PedidosResponse
        {
            public List<Pedido> pedidos { get; set; }
        }
    

}
