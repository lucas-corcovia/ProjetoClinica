using ProjetoClinica.Business.DomainObjects;

namespace ProjetoClinica.Business.Models
{
    public class Fisioterapeuta : Entity, IAggregateRoot
    {

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Celular { get; set; }
        public string Especialidade { get; set; }
        public bool Disponivel { get; set; }
        public bool Apagado { get; set; }
        public ICollection<Paciente> Pacientes { get; set; }

        //public Paciente Paciente { get; set; }

        public Fisioterapeuta(string nome, string cpf, string rg, string celular, string especialidade, bool disponivel)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Celular = celular;
            Especialidade = especialidade;
            Disponivel = disponivel;
            Apagado = false;
        }

        public Fisioterapeuta()
        {
            Apagado = false;
        }
    }
}