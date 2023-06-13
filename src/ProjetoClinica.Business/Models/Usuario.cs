using ProjetoClinica.Business.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Models
{
    public class Usuario : Entity, IAggregateRoot
    {       
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Apagado { get; set; }

        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Apagado = false;
        }

        public Usuario()
        {
            Apagado = false;
        }
    }
}
