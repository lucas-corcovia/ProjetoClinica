using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Services
{
    public class FisioterapeutaService : IFisioterapeutaService
    {
        private readonly IFisioterapeutaRepository _fisioRepository;

        public FisioterapeutaService(IFisioterapeutaRepository fisioterapeutaRepository)
        {
            _fisioRepository = fisioterapeutaRepository;
        }

        public async Task AdicionarFisioterapeuta(Fisioterapeuta fisioterapeuta)
        {
            _fisioRepository.Adicionar(fisioterapeuta);
            await _fisioRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarFisioterapeuta(Fisioterapeuta fisioterapeuta)
        {
            _fisioRepository.Atualizar(fisioterapeuta);
            await _fisioRepository.UnitOfWork.Commit();
        }

        public async Task<Fisioterapeuta> ObterPorId(int? id)
        {
            return await _fisioRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Fisioterapeuta>> ObterTodos()
        {
            return await _fisioRepository.ObterTodos();
        }

        public async Task RemoverFisioterapeuta(int id)
        {
            _fisioRepository.Remover(id);
            await _fisioRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _fisioRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
