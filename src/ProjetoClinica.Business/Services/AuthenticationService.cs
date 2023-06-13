using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Data.Repository
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;       

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }        

        public bool Login(string email, string senha)
        {
            var usuario = _authenticationRepository.ObterPorEmail(email);

            if (usuario == null || usuario.Senha != senha)
            {
                return false;            
            }

            return true;
        }

        public void Dispose()
        {
            _authenticationRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
