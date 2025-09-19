using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
	public partial class MenuGestionMedicos : System.Web.UI.Page
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

        protected void btnAltaMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaMedico.aspx");

        }

        protected void btnListadoMedicos_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarMedicos.aspx");
        }

        protected void btnBajaMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("BajaMedico.aspx");
        }

        protected void btnModificacionMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModificacionMedico.aspx");
        }

        protected void btnGestionarTurnosMedicos_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubMenu-GestionDisponibilidad/MenuDisponibilidad.aspx");
        }
    }
}