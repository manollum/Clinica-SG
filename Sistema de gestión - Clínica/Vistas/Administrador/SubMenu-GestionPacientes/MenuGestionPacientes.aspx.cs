using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class WebForm2 : System.Web.UI.Page
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

        protected void btnAltaPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaPaciente.aspx");
        }

        protected void btnBajaPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("BajaPaciente.aspx");
        }

        protected void btnModificacionPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModificacionPaciente.aspx");
        }

        protected void btnListarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarPacientes.aspx");
        }
    }
}