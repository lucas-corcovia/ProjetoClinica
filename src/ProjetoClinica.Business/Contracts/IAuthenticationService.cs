using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Contracts
{
    public interface IAuthenticationService :  IDisposable
    {
       bool Login(string email, string senha);
    }
}
