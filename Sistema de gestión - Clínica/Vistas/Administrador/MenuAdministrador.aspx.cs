using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class MenuAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
            }
        }

        protected void btnGestionMedicos_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubMenu-GestionMedicos/MenuGestionMedicos.aspx");
        }

        protected void btnGestionTurnos_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubMenu-GestionTurnos/MenuGestionTurnos.aspx");
        }

        protected void btnGestionPacientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubMenu-GestionPacientes/MenuGestionPacientes.aspx");
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubCarpeta-Reportes-Informes/MenuReportes-Informes.aspx");
        }
    }
}