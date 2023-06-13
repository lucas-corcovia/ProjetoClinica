using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.App.ViewModels;
using ProjetoClinica.App.Extensions;
using System.Collections;

namespace ProjetoClinica.App.Controllers
{
    [ServiceFilter(typeof(AutenticacaoFilterAttribute))]
    public class PacientesController : Controller
    {
        private readonly IPacienteService _pacienteService;
        private readonly IFisioterapeutaService _fisioterapeutaService;
        private readonly IMapper _mapper;        

        public PacientesController(IPacienteService pacienteService, IMapper mapper, IFisioterapeutaService fisioterapeutaServicer)
        {
            _pacienteService = pacienteService;
            _mapper = mapper;
            _fisioterapeutaService = fisioterapeutaServicer;           
        }

        // GET: Pacientes        
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string Nome = "", int Id = 0)
        {
            var pacientes = _mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteService.ObterTodos());

            if (!string.IsNullOrEmpty(Nome))
            {
                var pacientePorNome = PesquisarPacientePorNome(Nome, pacientes);
                IEnumerable<PacienteViewModel> pacientesFiltrados = pacientePorNome.ToList();

                return View(ListagemPaginas(page, pageSize, pacientesFiltrados));
            }

            if (Id != 0)
            {
                var pacientePorId = PesquisarPacientePorCodigo(Id, pacientes);
                IEnumerable<PacienteViewModel> pacientesFiltrados = pacientePorId.ToList();

                return View(ListagemPaginas(page, pageSize, pacientesFiltrados));
            }

            return View(ListagemPaginas(page, pageSize, pacientes));
           
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var pacientes = _mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteService.ObterTodos());

            if (id == null || pacientes == null)
            {
                return NotFound();
            }

            var paciente =  _mapper.Map<PacienteViewModel>(await _pacienteService.ObterPacienteEFisioterapeutaPorId(id)); 

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public async Task<IActionResult> Create()
        {
            var fisioterapeutas = await _fisioterapeutaService.ObterTodos();
            var apenasDisponiveis = fisioterapeutas.Where(f => f.Disponivel != false).ToList();

            ViewData["FisioterapeutaId"] = new SelectList(apenasDisponiveis, "Id", "Nome");

            return View();
        }

        // POST: Pacientes/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PacienteViewModel pacienteViewModel, IFormFile? foto)
        {
            if (!ModelState.IsValid) return View(pacienteViewModel);

            ConverteFotoPaciente(pacienteViewModel, foto);           

            var fisioterapeutas = await _fisioterapeutaService.ObterTodos();
            var apenasDisponiveis = fisioterapeutas.Where(f => f.Disponivel != false).ToList();

            ViewData["FisioterapeutaId"] = new SelectList(apenasDisponiveis, "Id", "Nome");

            var paciente = _mapper.Map<Paciente>(pacienteViewModel);
            await _pacienteService.AdicionarPaciente(paciente);

            return RedirectToAction(nameof(Index));
        }

        //GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteService.ObterTodos()) == null)
            {
                return NotFound();
            }

            var paciente = _mapper.Map<PacienteViewModel>(await _pacienteService.ObterPorId(id));
            if (paciente == null)
            {
                return NotFound();
            }

            var fisioterapeutas = await _fisioterapeutaService.ObterTodos();
            var apenasDisponiveis = fisioterapeutas.Where(f => f.Disponivel != false).ToList();
            ViewData["FisioterapeutaId"] = new SelectList(apenasDisponiveis, "Id", "Nome");

            return View(paciente);
        }

        // POST: Pacientes/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] PacienteViewModel pacienteViewModel, IFormFile foto)
        {
            if (id != pacienteViewModel.Id) return NotFound();

            ConverteFotoPaciente(pacienteViewModel, foto);

            var paciente = _mapper.Map<Paciente>(pacienteViewModel);
            await _pacienteService.AtualizarPaciente(paciente);

            var fisioterapeutas = await _fisioterapeutaService.ObterTodos();
            var apenasDisponiveis = fisioterapeutas.Where(f => f.Disponivel != false).ToList();
            ViewData["FisioterapeutaId"] = new SelectList(apenasDisponiveis, "Id", "Nome");

            return RedirectToAction("Index");
        }

        //GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _pacienteService.ObterTodos == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteService.ObterPorId(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_pacienteService.ObterTodos() == null)
            {
                return Problem("Entity set 'ClinicaContext.Pacientes'  is null.");
            }
            var paciente = _pacienteService.ObterPorId(id);
            if (paciente != null)
            {
                await _pacienteService.RemoverPaciente(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private void ConverteFotoPaciente(PacienteViewModel pacienteViewModel, IFormFile foto)
        {
            if (foto != null && foto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    foto.CopyToAsync(memoryStream);
                    pacienteViewModel.Foto = memoryStream.ToArray();
                }
            }
        }

        private IQueryable<PacienteViewModel> PesquisarPacientePorNome(string Nome, IEnumerable<PacienteViewModel> pacientes)
        {
            var query = pacientes.ToList().AsQueryable();

            if (!string.IsNullOrEmpty(Nome))
            {
                query = query.Where(f => f.Nome.Contains(Nome));
            }

            query = query.OrderBy(f => f.Nome);

            return query;
        }

        private IQueryable<PacienteViewModel> PesquisarPacientePorCodigo(int id, IEnumerable<PacienteViewModel> pacientes)
        {
            var query = pacientes.ToList().AsQueryable();

            if (id != null)
            {
                query = query.Where(f => f.Id == id);
            }

            query = query.OrderBy(f => f.Id);

            return query;
        }

        private IEnumerable ListagemPaginas(int page, int pageSize, IEnumerable<PacienteViewModel> pacientes)
        {
            var pacientesList = pacientes.ToList();
            var pacientesPaginados = pacientesList
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = (int)Math.Ceiling(pacientesList.Count / (double)pageSize);

            return pacientesPaginados;
        }
    }
}
