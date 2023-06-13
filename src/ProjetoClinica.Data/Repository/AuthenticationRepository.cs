using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.Data.Context;

namespace ProjetoClinica.Data.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ClinicaContext _context;

        public AuthenticationRepository(ClinicaContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        

        public Usuario ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Apagado != true);          
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
