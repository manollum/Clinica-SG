using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class BajaTurno : System.Web.UI.Page
    {
        private NegocioTurno negocioTurno;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
            }
        }

        protected void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            negocioTurno = new NegocioTurno();

            int codTurno = int.Parse(txtCodigoTurno.Text.Trim());

            if ((negocioTurno.BajaLogicaTurno(codTurno)) == 1)
            {
                lblResultadoBaja.Text = "La baja de turno se realizó correctamente.";
                lblResultadoBaja.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblResultadoBaja.Text = "No se pudo realizar la baja de turno. Verifique que el código ingresado exista.";
                lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
            }

            txtCodigoTurno.Text = string.Empty;
        }
    }
}