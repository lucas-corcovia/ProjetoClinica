using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.App.ViewModels;
using ProjetoClinica.App.Extensions;

namespace ProjetoClinica.App.Controllers
{
    [ServiceFilter(typeof(AutenticacaoFilterAttribute))]
    public class FisioterapeutasController : Controller
    {
        private readonly IFisioterapeutaService _fisioterapeutaService;
        private readonly IMapper _mapper;

        public FisioterapeutasController(IFisioterapeutaService fisioterapeutaService, IMapper mapper)
        {
            _fisioterapeutaService = fisioterapeutaService;
            _mapper = mapper;
        }

        //GET: Fisioterapeutas
        //[Route("fisioterapeutas-lista")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string Nome = "", int Id = 0)
        {
            var fisioterapeutas = _mapper.Map<IEnumerable<FisioterapeutaViewModel>>(await _fisioterapeutaService.ObterTodos());

            if (!string.IsNullOrEmpty(Nome))
            {
                var fisioterapeutaPorNome = PesquisarFisioPorNome(Nome, fisioterapeutas);
                IEnumerable<FisioterapeutaViewModel> fisioterapeutasFiltrados = fisioterapeutaPorNome.ToList();

                return View(ListagemPaginas(page, pageSize, fisioterapeutasFiltrados));
            }

            if (Id != 0)
            {
                var fisioterapeutaPorId = PesquisarFisioPorCodigo(Id, fisioterapeutas);
                IEnumerable<FisioterapeutaViewModel> fisioterapeutasFiltrados = fisioterapeutaPorId.ToList();

                return View(ListagemPaginas(page, pageSize, fisioterapeutasFiltrados));
            }

            return View(ListagemPaginas(page, pageSize, fisioterapeutas));
        }

        // GET: Fisioterapeutas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FisioterapeutaViewModel fisioterapeutaViewModel)
        {
            if (!ModelState.IsValid) return View(fisioterapeutaViewModel);

            var fisioterapeuta = _mapper.Map<Fisioterapeuta>(fisioterapeutaViewModel);
            await _fisioterapeutaService.AdicionarFisioterapeuta(fisioterapeuta);

            return RedirectToAction(nameof(Index));
        }

        // GET: Fisioterapeutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _fisioterapeutaService.ObterTodos == null)
            {
                return NotFound();
            }

            var fisioterapeuta = await _fisioterapeutaService.ObterPorId(id);

            if (fisioterapeuta == null)
            {
                return NotFound();
            }

            return View(fisioterapeuta);
        }

        // GET: Fisioterapeutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _mapper.Map<IEnumerable<FisioterapeutaViewModel>>(await _fisioterapeutaService.ObterTodos()) == null)
            {
                return NotFound();
            }

            var fisioterapeuta = _mapper.Map<FisioterapeutaViewModel>(await _fisioterapeutaService.ObterPorId(id));
            if (fisioterapeuta == null)
            {
                return NotFound();
            }

            return View(fisioterapeuta);
        }

        // POST: Fisioterapeutas/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] FisioterapeutaViewModel fisioterapeutaViewModel)
        {
            if (id != fisioterapeutaViewModel.Id) return NotFound();

            var fisioterapeuta = _mapper.Map<Fisioterapeuta>(fisioterapeutaViewModel);
            await _fisioterapeutaService.AtualizarFisioterapeuta(fisioterapeuta);

            return RedirectToAction("Index");
        }

        // GET: Fisioterapeutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _fisioterapeutaService.ObterTodos == null)
            {
                return NotFound();
            }

            var fisioterapeuta = await _fisioterapeutaService.ObterPorId(id);
            if (fisioterapeuta == null)
            {
                return NotFound();
            }

            return View(fisioterapeuta);
        }

        // POST: Fisioterapeutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_fisioterapeutaService.ObterTodos() == null)
            {
                return Problem("Entity set 'ClinicaContext.Fisioterapeutas'  is null.");
            }
            var fisioterapeuta = _fisioterapeutaService.ObterPorId(id);
            if (fisioterapeuta != null)
            {
                await _fisioterapeutaService.RemoverFisioterapeuta(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FisioterapeutaExists(int id)
        {
            var teste = _fisioterapeutaService.ObterPorId(id);
            if (teste == null) return false;

            return true;
            // return (_context.Fisioterapeutas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IQueryable<FisioterapeutaViewModel> PesquisarFisioPorNome(string Pesquisa, IEnumerable<FisioterapeutaViewModel> fisioterapeutas)
        {
            var query = fisioterapeutas.ToList().AsQueryable();

            if (!string.IsNullOrEmpty(Pesquisa))
            {
                query = query.Where(f => f.Nome.Contains(Pesquisa));
            }

            query = query.OrderBy(f => f.Nome);

            return query;
        }

        private IQueryable<FisioterapeutaViewModel> PesquisarFisioPorCodigo(int id, IEnumerable<FisioterapeutaViewModel> fisioterapeutas)
        {
            var query = fisioterapeutas.ToList().AsQueryable();

            if (id != null)
            {
                query = query.Where(f => f.Id == id);
            }

            query = query.OrderBy(f => f.Id);

            return query;
        }

        private IEnumerable ListagemPaginas(int page, int pageSize, IEnumerable<FisioterapeutaViewModel> fisioterapeutas)
        {
            var fisioterapeutasList = fisioterapeutas.ToList();
            var fisioterapeutasPaginados = fisioterapeutasList
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = (int)Math.Ceiling(fisioterapeutasList.Count / (double)pageSize);

            return fisioterapeutasPaginados;
        }
    }
}
