using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Negocios; 

namespace Vistas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();

            NegocioLogin negocioLogin = new NegocioLogin();

            // Validación Administrador
            if (negocioLogin.ValidarUsuarioAdministrador(usuario, contrasena))
            {
                Session["usuario"] = "Administrador";
                Response.Redirect("~/Administrador/MenuAdministrador.aspx");
            }
            // Validación Médico
            else if (negocioLogin.ValidarUsuarioMedico(usuario, contrasena))
            {
                string nombreCompleto = negocioLogin.ObtenerNombreCompletoMedico(usuario, contrasena);
                Session["usuario"] = nombreCompleto;
                Session["legajo"] = negocioLogin.ObtenerLegajo(usuario, contrasena);
                Response.Redirect("~/Medico/MenuMedico.aspx");
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }

            txtUsuario.Text = string.Empty;
            txtContraseña.Text = string.Empty;
        }
    }
}