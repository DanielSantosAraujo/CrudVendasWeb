using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VendasWebMvc.Models;
using VendasWebMvc.Models.ViewModels;
using VendasWebMvc.Services;
using VendasWebMvc.Services.Exceptions;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoServices _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoServices departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _vendedorService.EncontreTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.EncontreTodosAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            
             await _vendedorService.InserirAsync(vendedor);
             return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não fornecido" });
            }

            var obj = await _vendedorService.EncontrePeloIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           
                await _vendedorService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            
            
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não fornecido" });
            }

            var obj = await _vendedorService.EncontrePeloIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não fornecido" });
            }

            var obj = await _vendedorService.EncontrePeloIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não encontrado" });
            }

            List<Departamento> departamentos = await _departamentoService.EncontreTodosAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id mismatch" });
            }

            try
            {
                await _vendedorService.AtualizaAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { messagem = e.Message });
            }
        }

        public IActionResult Error(string messagem)
        {
            var viewModel = new ErrorViewModel
            {
                Messagem = messagem,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
