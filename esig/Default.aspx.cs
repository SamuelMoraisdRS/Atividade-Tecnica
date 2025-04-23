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
    public partial class Default : Page
    {
        private int TamanhoPagina
        {
            get
            {
                return (int)Session["tamanho_pagina_pessoa_salario"];
            }
            set
            {
                Session["tamanho_pagina_pessoa_salario"] = value;
            }
        }
        private string CriterioOrdenacao
        {
            get
            {
                return (string)Session["criterio_ordenacao"];
            }
            set
            {
                Session["criterio_ordenacao"] = value;
            }
        }
        private int PaginaAtual
        {
            get
            {
                return (int)Session["numero_pagina_pessoa_salario"];
            }
            set
            {
                Session["numero_pagina_pessoa_salario"] = value;
            }
        }

        private List<PessoaSalarioDTO> Buffer
        {
            get
            {
                return Session["buffer"] as List<PessoaSalarioDTO>;
            }
            set
            {
                Session["buffer"] = value;
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
            Buffer = PessoaSalarioDAO.GetPessoasSalario(CriterioOrdenacao, PaginaAtual);
            empregadosGridView.DataSource = Buffer;
            empregadosGridView.DataBind();
        }

        protected void empregadosGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            empregadosGridView.EditIndex = e.NewEditIndex;
            CarregarGrid();
        }

        protected void empregadosGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            empregadosGridView.EditIndex = -1;
            CarregarGrid();
        }

        protected void empregadosGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(empregadosGridView.DataKeys[e.RowIndex].Value.ToString());
            Decimal novoSalario = Convert.ToDecimal(e.NewValues["salario"]);

            PessoaSalarioDAO.UpdateSalario(id, novoSalario);
            empregadosGridView.EditIndex = -1;

            CarregarGrid();
        }

        protected void empregadosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginaAtual = e.NewPageIndex + 1;
            CarregarGrid();
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
