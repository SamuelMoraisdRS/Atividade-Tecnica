using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using esig.DAL;
using esig.DTO;

namespace esig
{
    public partial class EditarPessoa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int id = Convert.ToInt32(Request.QueryString["id"]);
                int id = int.Parse(Request.QueryString["id"]);
                PessoaDTO pessoa = PessoaDAO.GetPessoaPorId(id);
                if (pessoa != null)
                {
                    string nomeCargo = PessoaDAO.GetNomeCargoPorPessoa(pessoa.id_cargo);
                    lblID.Text = pessoa.id.ToString();
                    editNome.Text = pessoa.nome;
                    editEmail.Text = pessoa.email;
                    editCEP.Text = pessoa.cep;
                    editEndereco.Text = pessoa.endereco;
                    editUsuario.Text = pessoa.usuario;
                    editCidade.Text = pessoa.cidade;
                    editDataNascimento.Text = pessoa.data_nascimento.ToString("MM/dd/yy");
                    editPais.Text = pessoa.pais;
                    editCargo.Text = nomeCargo;
                    editTelefone.Text = pessoa.telefone;
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
         
            int novoIdCargo = PessoaDAO.GetIdCargoPorNome(editCargo.Text);
            PessoaDTO pessoa = new PessoaDTO(
                id: Convert.ToInt32(lblID.Text),
                nome: editNome.Text,
                email: editEmail.Text,
                telefone: editTelefone.Text,
                endereco: editEndereco.Text,
                cidade: editCidade.Text,
                pais: editPais.Text,
                cep: editCEP.Text,
                usuario: editUsuario.Text,
                data_nascimento: Convert.ToDateTime(editDataNascimento.Text),
                id_cargo: Convert.ToInt32(novoIdCargo)

            );
            PessoaDAO.UpdatePessoa(pessoa);
            Response.Redirect("Pessoas.aspx");
        }
    }
}