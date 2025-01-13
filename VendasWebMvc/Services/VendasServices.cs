using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Data;
using VendasWebMvc.Models;

namespace VendasWebMvc.Services
{
    public class VendasServices
    {
        private readonly VendasWebMvcContext _context;

        public VendasServices(VendasWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Vendas>> EncontrePelaDataAsync(DateTime? dataMinima, DateTime? dataMaxima)
        {
            var resultado = from obj in _context.Venda select obj;
            if (dataMinima.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataMinima.Value);
            }
            if (dataMaxima.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataMaxima.Value);
            }
            return await resultado
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departamento, Vendas>>> EncontrePelaDataAgrupadaAsync(DateTime? dataMinima, DateTime? dataMaxima)
        {
            var resultado = from obj in _context.Venda select obj;
            if (dataMinima.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataMinima.Value);
            }
            if (dataMaxima.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataMaxima.Value);
            }
            return await resultado
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Vendedor.Departamento)
                .ToListAsync();
        }

    }
}
