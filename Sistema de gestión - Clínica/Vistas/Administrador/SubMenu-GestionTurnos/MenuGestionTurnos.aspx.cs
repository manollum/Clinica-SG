using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador.SubMenu_GestionTurnos
{
	public partial class MenuGestionTurnos : System.Web.UI.Page
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

        protected void btnAltaTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaTurno.aspx");
        }

        protected void btnBajaTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("BajaTurno.aspx");
        }

        protected void btnModificacionTurno_Click(object sender, EventArgs e)
        {
			Response.Redirect("ModificacionTurno.aspx");
        }

        protected void btnListadoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarTurnos.aspx");
        }
    }
}