using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador.SubMenu_GestionPacientes
{
	public partial class ModificacionPaciente : System.Web.UI.Page
	{
		private readonly NegocioPaciente negocioPaciente = new NegocioPaciente();
        private Paciente paciente;
        
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
                CargarPacientesTabla();
            }
        }

        private void CargarPacientesTabla()
        {
            gvModificacionPacientes.DataSource = negocioPaciente.ObtenerPacientes();
            gvModificacionPacientes.DataBind();
        }

        private void CargarDDLProvincias(DropDownList ddl)
        {
            ddl.DataSource = negocioPaciente.getRegistrosProvincias();
            ddl.DataTextField = "Descripcion_PR";
            ddl.DataValueField = "CodProvincia_PR";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        protected void gvModificacionPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModificacionPacientes.EditIndex = -1;
            gvModificacionPacientes.PageIndex = e.NewPageIndex;
            CargarPacientesTabla();
        }

        protected void gvModificacionPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModificacionPacientes.EditIndex = -1;
            CargarPacientesTabla();
        }

        protected void gvModificacionPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModificacionPacientes.EditIndex = e.NewEditIndex;
            CargarPacientesTabla();
        }

        protected void gvModificacionPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            paciente = new Paciente();

            paciente.Legajo = int.Parse(((Label)gvModificacionPacientes.Rows[e.RowIndex].FindControl("lbl_et_Legajo")).Text);
            paciente.Apellido = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Apellido")).Text;
            paciente.Nombre = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Nombre")).Text;
            paciente.Dni = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_DNI")).Text;
            paciente.Sexo = ((RadioButtonList)gvModificacionPacientes.Rows[e.RowIndex].FindControl("rbl_et_Sexo")).SelectedValue[0];
            paciente.FechaNacimiento = DateTime.Parse(((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_FechaNacimiento")).Text);
            paciente.Nacionalidad = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Nacionalidad")).Text;
            paciente.CodProvincia = int.Parse(((DropDownList)gvModificacionPacientes.Rows[e.RowIndex].FindControl("ddl_et_Provincias")).SelectedValue);
            paciente.Localidad = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Localidad")).Text;
            paciente.Direccion = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Direccion")).Text;
            paciente.Telefono = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Telefono")).Text;
            paciente.CorreoElectronico = ((TextBox)gvModificacionPacientes.Rows[e.RowIndex].FindControl("txt_et_Correo")).Text;

            if (!(negocioPaciente.ExisteDniDuplicado(paciente.Dni, paciente.Nacionalidad, paciente.Legajo)))
            {
                if (negocioPaciente.ModificarPaciente(paciente))
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Paciente modificado correctamente.";
                    gvModificacionPacientes.EditIndex = -1;
                    CargarPacientesTabla();
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Error al modificar el paciente. Verifique los datos ingresados.";
                }
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al modificar el paciente. El DNI Ingresado ya se encuentra en el Sistema.";
            }
        }

        protected void gvModificacionPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Asegurar que es una fila de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar si la fila está en modo de edición
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    // Encontrar el control dentro de la fila
                    DropDownList ddlProvincias = (DropDownList)e.Row.FindControl("ddl_et_Provincias");
                    RadioButtonList rblSexo = (RadioButtonList)e.Row.FindControl("rbl_et_Sexo");
                    TextBox txtFechaNacimiento = (TextBox)e.Row.FindControl("txt_et_FechaNacimiento");
                    TextBox txtTelefono = (TextBox)e.Row.FindControl("txt_et_Telefono");

                    // Llamar al método para cargar los datos en el DropDownList
                    CargarDDLProvincias(ddlProvincias);

                    // Se capturan los valores de algunos campos que serán necesarios
                    string IDProvincia = DataBinder.Eval(e.Row.DataItem, "CodProvincia").ToString();
                    char sexo = Convert.ToChar(DataBinder.Eval(e.Row.DataItem, "Sexo").ToString()[0]);
                    string fechaStr = DataBinder.Eval(e.Row.DataItem, "Fecha de Nacimiento").ToString();
                    string numeroTelefono = txtTelefono.Text.Trim();

                    // Establezco como predefinido el valor actual de los campos en la BD
                    SeleccionarProvincia(ddlProvincias, IDProvincia);
                    SeleccionarSexo(rblSexo, sexo);
                    SeleccionarFechaNacimiento(txtFechaNacimiento, fechaStr);
                    txtTelefono.Text = numeroTelefono;
                }
            }
        }

        private void SeleccionarProvincia(DropDownList ddlProvincias, string IDProvincia)
        {
            ListItem itemProvincia = ddlProvincias.Items.FindByValue(IDProvincia);
            if (itemProvincia != null)
            {
                ddlProvincias.SelectedValue = IDProvincia;
            }
            else
            {
                ddlProvincias.SelectedIndex = 0;
            }
        }

        private void SeleccionarSexo(RadioButtonList rblSexo, char sexo)
        {
            if (sexo == 'M')
            {
                rblSexo.SelectedValue = "M";
            }
            else if (sexo == 'F')
            {
                rblSexo.SelectedValue = "F";
            }
            else
            {
                rblSexo.SelectedValue = null;
            }
        }

        private void SeleccionarFechaNacimiento(TextBox txtFechaNacimiento, string fechaStr)
        {
            if (txtFechaNacimiento != null)
            {
                if (DateTime.TryParse(fechaStr, out DateTime fecha))
                {
                    txtFechaNacimiento.Text = fecha.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtFechaNacimiento.Text = "";
                }
            }
            else
            {
                txtFechaNacimiento.Text = "";
            }

            // Configuro el rango permitido
            DateTime hoy = DateTime.Today;
            DateTime fechaMinima = hoy.AddYears(-140);

            txtFechaNacimiento.Attributes["max"] = hoy.ToString("yyyy-MM-dd");
            txtFechaNacimiento.Attributes["min"] = fechaMinima.ToString("yyyy-MM-dd");
        }

        protected void ddl_et_Provincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is DropDownList ddl_et_Provincias)
            {
                if (ddl_et_Provincias.SelectedIndex != 0)
                {
                    ddl_et_Provincias.Items[0].Enabled = false;
                }
            }
        }
    }
}