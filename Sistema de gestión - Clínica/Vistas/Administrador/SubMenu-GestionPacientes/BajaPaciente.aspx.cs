using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace Vistas.Administrador.SubMenu_GestionPacientes
{
	public partial class BajaPaciente : System.Web.UI.Page
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

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            string legajo = txtLegajoBaja.Text.Trim();

            if (!string.IsNullOrEmpty(legajo))
            {
                NegocioPaciente negocioPaciente = new NegocioPaciente();
                int exito = negocioPaciente.BajaLogicaPacientePorLegajo(legajo);

                if (exito == 1)
                {
                    lblResultadoBaja.ForeColor = System.Drawing.Color.Green;
                    lblResultadoBaja.Text = "Paciente dado de baja exitosamente.";
                    txtLegajoBaja.Text = string.Empty;
                }
                else if (exito == 0)
                {
                    lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
                    lblResultadoBaja.Text = "No se encontró el paciente o ya estaba dado de baja.";
                }
                else
                {
                    lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
                    lblResultadoBaja.Text = "Ocurrió un error al intentar dar de baja al paciente. Por favor, intentá nuevamente más tarde.";
                }
            }
            else
            {
                lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
                lblResultadoBaja.Text = "Por favor, ingresá un legajo válido.";
            }
        }

    }
}