using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esig.DTO
{
    public class PessoaDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; } 
        public string cidade { get; set; }
        public string estado { get; set; }  
        public string cep { get; set; }
        public int id_cargo { get; set; }
        public DateTime data_nascimento { get; set; }
        public string pais { get; set; }
        public PessoaDTO(int id, string nome, string email, string telefone, DateTime data_nascimento,
                        string endereco, string cidade, string pais, string cep, string usuario, int id_cargo)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.endereco = endereco;
            this.cidade = cidade;
            this.estado = estado;
            this.cep = cep;
            this.pais = pais;
            this.usuario = usuario;
            this.data_nascimento = data_nascimento;
            this.id_cargo = id_cargo;
        }
    }
}