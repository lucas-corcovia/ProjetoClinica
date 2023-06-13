using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Data.Repository
{
    public class FisioterapeutaRepository : IFisioterapeutaRepository
    {
        private readonly ClinicaContext _context;

        public FisioterapeutaRepository(ClinicaContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Fisioterapeuta fisio)
        {
            _context.Fisioterapeutas.Add(fisio);
        }

        public void Atualizar(Fisioterapeuta fisio)
        {
            _context.Fisioterapeutas.Update(fisio);
        }

        public async Task<Fisioterapeuta> ObterPorId(int? id)
        {
            return await _context.Fisioterapeutas
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && !p.Apagado);
        }

        public async Task<IEnumerable<Fisioterapeuta>> ObterTodos()
        {
            return await _context.Fisioterapeutas.Where(f => f.Apagado != true).AsNoTracking().ToListAsync();
        }

        public void Remover(int id)
        {
            var fisio = _context.Fisioterapeutas.FirstOrDefault(x => x.Id == id && x.Apagado != true);
            if (fisio != null)
            {
                fisio.Apagado = true;
                _context.Fisioterapeutas.Update(fisio);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
