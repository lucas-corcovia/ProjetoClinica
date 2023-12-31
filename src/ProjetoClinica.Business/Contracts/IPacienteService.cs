﻿using ProjetoClinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoClinica.Business.Contracts
{
    public interface IPacienteService : IDisposable
    {
        Task AdicionarPaciente(Paciente paciente, Endereco endereco);
        Task AtualizarPaciente(Paciente paciente);
        Task AtualizarEndereco(Endereco endereco);
        Task RemoverPaciente(int id);
        Task<Paciente> ObterPorId(int? id);
        Task<Paciente> ObterPacienteEFisioterapeutaPorId(int? id);
        Task<IEnumerable<Paciente>> ObterTodos();

    }
}
