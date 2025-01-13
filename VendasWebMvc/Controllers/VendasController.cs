using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class VendasController : Controller
    {
        private readonly VendasServices _vendasServices;

        public VendasController(VendasServices vendasServices)
        {
            _vendasServices = vendasServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>  PesquisaSimples(DateTime? dataMinima, DateTime? dataMaxima)
        {
            if (!dataMinima.HasValue)
            {
                dataMinima = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataMaxima.HasValue)
            {
                dataMaxima = DateTime.Now;
            }
            var resultado = await _vendasServices.EncontrePelaDataAsync(dataMinima, dataMaxima);
            return View(resultado);
            
        }

        public async Task<IActionResult> PesquisaAgrupada(DateTime? dataMinima, DateTime? dataMaxima)
        {
            if (!dataMinima.HasValue)
            {
                dataMinima = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataMaxima.HasValue)
            {
                dataMaxima = DateTime.Now;
            }
            var resultado = await _vendasServices.EncontrePelaDataAgrupadaAsync(dataMinima, dataMaxima);
            return View(resultado);
        }
    }
}
