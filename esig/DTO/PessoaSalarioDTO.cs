using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esig.DTO
{
    public class PessoaSalarioDTO
    {
        public int id { get; private set; }
        public string nome_cargo { get; private set; }
        public string nome_pessoa{ get; private set; }
        public decimal salario { get; private set; }
        public int id_pessoa { get; private set; }
        public PessoaSalarioDTO(int id, string nome_cargo, string nome_pessoa, decimal nu_salario, int id_pessoa)
        {
            this.id = id;
            this.nome_cargo = nome_cargo;
            this.nome_pessoa = nome_pessoa;
            this.salario = nu_salario;
            this.id_pessoa = id_pessoa;
        }
    }
}