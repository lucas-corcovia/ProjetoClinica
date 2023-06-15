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
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ClinicaContext _context;

        public EnderecoRepository(ClinicaContext clinicaContext)
        {
            _context = clinicaContext;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
        }

        public void Atualizar(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
        }        

        public async Task<Endereco> ObterPorId(int? id)
        {
            return await _context.Enderecos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Endereco>> ObterTodos()
        {
            return await _context.Enderecos.AsNoTracking().ToListAsync();
        }

        public void Remover(int id)
        {
            //var fisio = _context.Enderecos.FirstOrDefault(x => x.Id == id);
            //if (fisio != null)
            //{
            //    fisio.Apagado = true;
            //    _context.Fisioterapeutas.Update(fisio);
            //}
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
