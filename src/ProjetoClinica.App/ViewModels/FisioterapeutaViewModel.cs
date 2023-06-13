using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.App.ViewModels
{
    public class FisioterapeutaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Celular { get; set; }
        public string Especialidade { get; set; }
        public bool Disponivel { get; set; }

        public FisioterapeutaViewModel(string nome, string cpf, string rg, string celular, string especialidade, bool disponivel)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Celular = celular;
            Especialidade = especialidade;
            Disponivel = disponivel;
        }

        public FisioterapeutaViewModel() { }
    }
}
