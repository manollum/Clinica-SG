using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;

namespace Vistas.Administrador.SubMenu_GestionMedicos
{
	public partial class BajaMedico : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (Session["usuario"] == null)
            {
                
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
            }
        }

        protected void btnBajaMedico_Click(object sender, EventArgs e)
        {
            string legajo = txtLegajoBajaMedico.Text?.Trim();

            if (!string.IsNullOrEmpty(legajo))
            {
                NegocioMedico negocioMedico = new NegocioMedico();
                int  exito = negocioMedico.BajaLogicaMedicoPorLegajo(legajo);

                if (exito == 1)
                {
                    lblResultadoBajaMedico.ForeColor = System.Drawing.Color.Green;
                    lblResultadoBajaMedico.Text = "Médico dado de baja exitosamente.";
                    txtLegajoBajaMedico.Text = string.Empty;
                }
                else if (exito == 0)
                {
                    lblResultadoBajaMedico.ForeColor = System.Drawing.Color.Red;
                    lblResultadoBajaMedico.Text = "No se encontró el médico o el este ya estaba dado de baja.";
                }
                else if (exito == -1)
                {
                    lblResultadoBajaMedico.ForeColor = System.Drawing.Color.Red;
                    lblResultadoBajaMedico.Text = "Error al intentar dar de baja al médico. Por favor, intente nuevamente.";
                }
            }
            else
            {
                lblResultadoBajaMedico.ForeColor = System.Drawing.Color.Red;
                lblResultadoBajaMedico.Text = "Por favor, ingresá un legajo válido.";
            }
        }
    }
}