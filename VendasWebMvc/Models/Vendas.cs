using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Models
{
    public class Vendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Quantia { get; set; }
        public VendasStatus Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public Vendas()
        {
            
        }

        public Vendas(int id, DateTime data, double quantia, VendasStatus status, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Quantia = quantia;
            Status = status;
            Vendedor = vendedor;
        }
    }
}