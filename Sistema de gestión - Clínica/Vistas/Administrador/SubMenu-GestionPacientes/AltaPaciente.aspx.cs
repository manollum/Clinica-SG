using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace Vistas.Administrador.SubMenu_GestionPacientes
{
	public partial class AltaPaciente : System.Web.UI.Page 
	{
        private Paciente paciente;
        private NegocioPaciente negocioPaciente;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
                CargarProvincias();
                lblUsuarioAdministrador.Text = "Administrador";

                //Obtengo la fecha actual
                DateTime hoy = DateTime.Today;

                //Asigno al atributo "max" del control la fecha máxima de nacimiento como hoy
                //(para evitar que se pueda seleccionar una fecha futura)
                txtFechaNacimientoPaciente.Attributes["max"] = hoy.ToString("yyyy-MM-dd");

                //Establesco la fecha minima (para asegurar que no tenga más de 140 años) 
                DateTime fechaMinimaPermitida = hoy.AddYears(-140);
                string fechaMinimaFormateada = fechaMinimaPermitida.ToString("yyyy-MM-dd");

                //Asigno la fecha calculada como el atributo "min" del control.
                txtFechaNacimientoPaciente.Attributes["min"] = fechaMinimaFormateada;
            }
            else
            {
                lblMensaje.Text = string.Empty;
            }
        }

        private void Registrar_AltaPaciente()
        {
            paciente = getPaciente();
            bool resultado = negocioPaciente.AltaPaciente(paciente);

            if (resultado)
            {
                int legajo = negocioPaciente.ObtenerUltimoPacienteRegistrado();

                if (legajo > 0)
                {
                    lblMensaje.Text = "Paciente registrado correctamente (Legajo asignado: " + legajo + ")";
                }
                else
                {
                    lblMensaje.Text = "Paciente registrado correctamente.";
                }

                lblMensaje.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al registrar el paciente. Intente nuevamente.";
            }
            
            LimpiarCampos();
        }

        private void CargarProvincias()
        {
            negocioPaciente = new NegocioPaciente();
            ddlProvinciaPaciente.DataSource = negocioPaciente.getRegistrosProvincias();
            ddlProvinciaPaciente.DataTextField = "Descripcion_PR";
            ddlProvinciaPaciente.DataValueField = "CodProvincia_PR";
            ddlProvinciaPaciente.DataBind();
            ddlProvinciaPaciente.Items.Insert(0, new ListItem("Seleccione una provincia", ""));
        }

        private void LimpiarCampos()
        {
            txtDniPaciente.Text = string.Empty;
            txtNombrePaciente.Text = string.Empty;
            txtApellidoPaciente.Text = string.Empty;
            rblSexoPaciente.SelectedIndex = -1; 
            txtNacionalidadPaciente.Text = string.Empty;
            txtFechaNacimientoPaciente.Text = string.Empty;
            txtDireccionPaciente.Text = string.Empty;
            txtLocalidadPaciente.Text = string.Empty;
            ddlProvinciaPaciente.Items[0].Enabled = true;
            ddlProvinciaPaciente.SelectedIndex = 0;
            txtCorreoPaciente.Text = string.Empty;
            txtTelefonoPaciente.Text = string.Empty;
            paciente = new Paciente();
        }

        private Paciente getPaciente()
        {
            paciente = new Paciente();
            paciente = new Paciente();
            negocioPaciente = new NegocioPaciente();
            paciente.Dni = txtDniPaciente.Text;
            paciente.Nombre = txtNombrePaciente.Text;
            paciente.Apellido = txtApellidoPaciente.Text;
            paciente.Sexo = Convert.ToChar(rblSexoPaciente.SelectedValue);
            paciente.Nacionalidad = txtNacionalidadPaciente.Text;
            paciente.FechaNacimiento = Convert.ToDateTime(txtFechaNacimientoPaciente.Text);
            paciente.Direccion = txtDireccionPaciente.Text;
            paciente.Localidad = txtLocalidadPaciente.Text;
            paciente.CodProvincia = Convert.ToInt32(ddlProvinciaPaciente.SelectedValue);
            paciente.CorreoElectronico = txtCorreoPaciente.Text;
            paciente.Telefono = txtTelefonoPaciente.Text;
            return paciente;
        }

        protected void btnRegistrarPaciente_Click1(object sender, EventArgs e)
        {
            paciente = new Paciente();

            paciente = getPaciente();

            // Verificar si ya existe un paciente con ese DNI
            if (negocioPaciente.VerificarExistenciaPacienteXDNI(paciente))
            {
                string script = "if(confirm('Ya existe un paciente registrado con ese mismo DNI. ¿Está seguro de que desea registrarlo de todas formas?')) { " +
                        "__doPostBack('" + btnAux.UniqueID + "',''); " +
                        "}";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmarRegistro", script, true);
            }
            else
            {
                Registrar_AltaPaciente();
            }
        }

        protected void btnAux_Click1(object sender, EventArgs e)
        {
            try
            {
                Registrar_AltaPaciente();
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Paciente duplicado registrado exitosamente según su confirmación.";
                lblMensaje.CssClass = "mensaje-exito";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Ocurrió un error al registrar: " + ex.Message;
                lblMensaje.CssClass = "mensaje-error";
            }
        }

        protected void ddlProvinciaPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvinciaPaciente.SelectedIndex != 0)
            {
                ddlProvinciaPaciente.Items[0].Enabled = false;
            }
        }
    }
}