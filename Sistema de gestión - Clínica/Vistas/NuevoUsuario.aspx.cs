using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        private readonly NegocioUsuarioMedico negocioUsuarioMedico = new NegocioUsuarioMedico();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                lblContraseña.Text = string.Empty;
            }
        }

        protected void btnAgregarUsuarioMedico_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string nombreUsuario = txtNuevoUsuario.Text.Trim();
                string contrasena = txtNuevaContraseña.Text.Trim();

                //  Validar legajo ingresado
                if (!int.TryParse(txtLegajoMedico.Text.Trim(), out int legajoMedico))
                {
                    lblContraseña.ForeColor = System.Drawing.Color.Red;
                    lblContraseña.Text = "Por favor, ingresá un legajo válido.";
                    limpiarCampos();
                    return;
                }

                // Verificar que el médico exista
                if (!negocioUsuarioMedico.ExisteMedicoPorLegajo(legajoMedico))
                {
                    lblContraseña.ForeColor = System.Drawing.Color.Red;
                    lblContraseña.Text = "No existe un médico con el legajo ingresado.";
                    limpiarCampos();
                    return;
                }

                // Verificar que el médico no tenga ya un usuario asignado
                if (negocioUsuarioMedico.ExisteUsuarioMedicoPorLegajo(legajoMedico))
                {
                    lblContraseña.ForeColor = System.Drawing.Color.Red;
                    lblContraseña.Text = "Este médico ya tiene un usuario asignado.";
                    limpiarCampos();
                    return;
                }

                // Insertar usuario médico
                bool exito = negocioUsuarioMedico.InsertarUsuarioMedico(legajoMedico, nombreUsuario, contrasena);

                if (exito)
                {
                    lblContraseña.ForeColor = System.Drawing.Color.Green;
                    lblContraseña.Text = "Usuario creado correctamente."; 
                }
                else
                {
                    lblContraseña.ForeColor = System.Drawing.Color.Red;
                    lblContraseña.Text = "Ocurrió un error al crear el usuario. Intentá nuevamente.";
                }

                limpiarCampos();
            }
        }

        private void limpiarCampos()
        {
            txtLegajoMedico.Text = "";
            txtNuevoUsuario.Text = "";
            txtNuevaContraseña.Text = "";
            txtNuevaContraseña2.Text = "";
        }
    }
}
