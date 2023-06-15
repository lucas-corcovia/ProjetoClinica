using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Contracts
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<IEnumerable<Endereco>> ObterTodos();
        Task<Endereco> ObterPorId(int? id);
        void Adicionar(Endereco endereco);
        void Atualizar(Endereco endereco);
        void Remover(int id);    
    }
}
