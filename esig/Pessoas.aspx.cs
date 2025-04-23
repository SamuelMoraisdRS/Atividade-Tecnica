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
    public partial class Pessoas : Page
    {
        private int TamanhoPagina
        {
            get
            {
                return (int)Session["tamanho_pagina_pessoas"];
            }
            set
            {
                Session["tamanho_pagina_pessoas"] = value;
            }
        }
        private string CriterioOrdenacao
        {
            get
            {
                return (string)Session["criterio_ordenacao_pessoas"];
            }
            set
            {
                Session["criterio_ordenacao_pessoas"] = value;
            }
        }
        private int PaginaAtual
        {
            get
            {
                return (int)Session["numero_pagina_pessoas"];
            }
            set
            {
                Session["numero_pagina_pessoas"] = value;
            }
        }

        private List<PessoaDTO> Buffer
        {
            get
            {
                return Session["buffer_pessoas"] as List<PessoaDTO>;
            }
            set
            {
                Session["buffer_pessoas"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PaginaAtual = 1;
                TamanhoPagina = 20;
                CriterioOrdenacao = "id";
                CarregarGrid();
            }
            
        }

        private void CarregarGrid()
        {
            Buffer = PessoaDAO.GetPessoas(CriterioOrdenacao, PaginaAtual);
            pessoasGridView.DataSource = Buffer;
            pessoasGridView.DataBind();
        }

        protected void pessoasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginaAtual = e.NewPageIndex + 1;
            CarregarGrid();
        }
        // Redireciona para a pagina de edicao
        protected void pessoasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                // Id da tabela obtida pela query string
                int id = Convert.ToInt32(pessoasGridView.DataKeys[index].Value);
                Response.Redirect($"EditarPessoa.aspx?id={id}");
            }
        }

        protected void NavigatePage(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button.CommandArgument == "Prev")
            {
                if (PaginaAtual > 1)
                {
                    PaginaAtual--;
                }
            }
            else if (button.CommandArgument == "Next")
            {
                PaginaAtual++;
            }
            CarregarGrid();
        }

    }
}