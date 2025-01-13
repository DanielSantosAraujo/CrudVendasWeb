using System.ComponentModel.DataAnnotations;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} requerido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} tamanha deve está entre {2} and {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} requerido")]
        [EmailAddress(ErrorMessage = "Coloque um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} requerido")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Salario { get; set; }
        public Departamento Departamento { get; set; }
        [Required]
        public int DepartamentoId { get; set; }
        public ICollection<Vendas> Vendas { get; set; } = new List<Vendas>();

        public Vendedor()
        {

        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salario, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Salario = salario;
            Departamento = departamento;
        }

        public void AdicionaVenda(Vendas venda)
        {
            Vendas.Add(venda);
        }

        public void RemoveVenda(Vendas venda)
        {
            Vendas.Remove(venda);
        }

        public double TotalVendas(DateTime inicial, DateTime final)
        {
            return Vendas.Where(venda => venda.Data >= inicial && venda.Data <= final).Sum(venda => venda.Quantia);
        }
    }
}
