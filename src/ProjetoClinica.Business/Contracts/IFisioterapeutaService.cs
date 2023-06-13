using ProjetoClinica.Business.Models;

namespace ProjetoClinica.Business.Contracts
{
    public interface IFisioterapeutaService : IDisposable
    {
        Task AdicionarFisioterapeuta(Fisioterapeuta fisioterapeuta);
        Task AtualizarFisioterapeuta(Fisioterapeuta fisioterapeuta);
        Task RemoverFisioterapeuta(int id);
        Task<Fisioterapeuta> ObterPorId(int? id);
        Task<IEnumerable<Fisioterapeuta>> ObterTodos();
    }
}
