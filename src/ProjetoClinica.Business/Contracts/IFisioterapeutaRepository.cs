using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Contracts
{
    public interface IFisioterapeutaRepository : IRepository<Fisioterapeuta>
    {
        Task<IEnumerable<Fisioterapeuta>> ObterTodos();
        Task<Fisioterapeuta> ObterPorId(int? id);
        void Adicionar(Fisioterapeuta fisioterapeuta);
        void Atualizar(Fisioterapeuta fisioterapeuta);
        void Remover(int id);
    }
}
