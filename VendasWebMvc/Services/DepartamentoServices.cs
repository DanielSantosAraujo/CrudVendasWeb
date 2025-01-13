using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Data;
using VendasWebMvc.Models;

namespace VendasWebMvc.Services
{
    public class DepartamentoServices
    {
        private readonly VendasWebMvcContext _context;

        public DepartamentoServices(VendasWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> EncontreTodosAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
