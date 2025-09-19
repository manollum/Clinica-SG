using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Negocios;
using Entidades;
using System.Web.DynamicData;

namespace Vistas.Administrador.SubMenu_GestionMedicos
{
	public partial class ModificacionMedico : System.Web.UI.Page
	{
        private NegocioMedico negocioMedico;
        private Entidades.Medico medico;

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
                CargarMedicosTabla();
            }
            else
            {
                lblMensaje.Text = string.Empty;
            }
        }

        public void CargarMedicosTabla()
        {
            negocioMedico = new NegocioMedico();
            gvModificacionMedicos.DataSource = negocioMedico.ObtenerMedicos();
            gvModificacionMedicos.DataBind();
        }

        protected void gvModificacionMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModificacionMedicos.EditIndex = -1;
            gvModificacionMedicos.PageIndex = e.NewPageIndex;
            CargarMedicosTabla();
        }

        protected void gvModificacionMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModificacionMedicos.EditIndex = e.NewEditIndex;
            CargarMedicosTabla();
        }

        protected void gvModificacionMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModificacionMedicos.EditIndex = -1;
            CargarMedicosTabla();
        }

        protected void gvModificacionMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            medico = new Entidades.Medico();
            negocioMedico = new NegocioMedico(); 
            medico.Legajo = int.Parse(((Label)gvModificacionMedicos.Rows[e.RowIndex].FindControl("lbl_et_Legajo")).Text);
            medico.Nombre = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Nombre")).Text;
            medico.Apellido = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Apellido")).Text;
            medico.DNI = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_DNI")).Text;
            medico.Sexo = ((RadioButtonList)gvModificacionMedicos.Rows[e.RowIndex].FindControl("rbl_et_Sexo")).SelectedValue[0];
            medico.FechaNacimiento = DateTime.Parse(((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_FechaNacimiento")).Text);
            medico.Nacionalidad = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Nacionalidad")).Text;
            medico.CodigoProvincia = int.Parse(((DropDownList)gvModificacionMedicos.Rows[e.RowIndex].FindControl("ddl_et_Provincias")).SelectedValue);
            medico.Localidad = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Localidad")).Text;
            medico.Direccion = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Direccion")).Text;
            medico.Correo = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Correo")).Text;
            medico.Telefono = ((TextBox)gvModificacionMedicos.Rows[e.RowIndex].FindControl("txt_et_Telefono")).Text.Trim();
            medico.CodigoEspecialidad = int.Parse(((DropDownList)gvModificacionMedicos.Rows[e.RowIndex].FindControl("ddl_et_Especialidades")).SelectedValue);

            if (!(negocioMedico.ExisteDniDuplicado(medico.DNI, medico.Nacionalidad, medico.Legajo)))
            { 

                if (negocioMedico.ModificarMedico(medico))
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Médico modificado correctamente.";
                    gvModificacionMedicos.EditIndex = -1;
                    CargarMedicosTabla();
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Error al modificar el médico. Verifique los datos ingresados.";
                }
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al modificar el paciente. El DNI Ingresado ya se encuentra en el Sistema.";
            }
        }

        public void CargarDDL(DropDownList ddlP, DropDownList ddlE)
        {
             negocioMedico = new NegocioMedico();

            ddlP.DataSource = negocioMedico.readerProvincias();
            ddlP.DataTextField = "Descripcion_PR";
            ddlP.DataValueField = "CodProvincia_PR";
            ddlP.DataBind();
            ddlP.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            ddlE.DataSource = negocioMedico.readerEspecialidad();
            ddlE.DataTextField = "Descripcion_ES";
            ddlE.DataValueField = "CodEspecialidad_ES";
            ddlE.DataBind();
            ddlE.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        protected void gvModificacionMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlProvincias = (DropDownList)e.Row.FindControl("ddl_et_Provincias");
                    DropDownList ddlEspecialidades = (DropDownList)e.Row.FindControl("ddl_et_Especialidades");
                    RadioButtonList rblSexo = (RadioButtonList)e.Row.FindControl("rbl_et_Sexo");
                    TextBox txtFechaNacimiento = (TextBox)e.Row.FindControl("txt_et_FechaNacimiento");
                    TextBox telefono = (TextBox)e.Row.FindControl("txt_et_Telefono");

                    CargarDDL(ddlProvincias, ddlEspecialidades);

                    string IDProvincia = DataBinder.Eval(e.Row.DataItem, "CodProvincia").ToString();
                    string IDEspecialidad = DataBinder.Eval(e.Row.DataItem, "CodEspecialidad").ToString();
                    char sexo = Convert.ToChar(DataBinder.Eval(e.Row.DataItem, "Sexo").ToString()[0]);
                    string fechaStr = DataBinder.Eval(e.Row.DataItem, "Fecha de Nacimiento").ToString();
                    string numeroTelefono = DataBinder.Eval(e.Row.DataItem, "Telefono").ToString().Trim();

                    SeleccionarProvincia(ddlProvincias, IDProvincia);
                    SeleccionarEspecialidad(ddlEspecialidades, IDEspecialidad);
                    SeleccionarSexo(rblSexo, sexo);
                    SeleccionarFechaNacimiento(txtFechaNacimiento, fechaStr);
                    telefono.Text = numeroTelefono;
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

        private void SeleccionarEspecialidad(DropDownList ddlEspecialidades, string IDEspecialidad)
        {
            ListItem itemEspecialidad = ddlEspecialidades.Items.FindByValue(IDEspecialidad);
            if (itemEspecialidad != null)
            {
                ddlEspecialidades.SelectedValue = IDEspecialidad;
            }
            else
            {
                ddlEspecialidades.SelectedIndex = 0;
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
                if (DateTime.TryParseExact(fechaStr, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
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
        }

        protected void ddl_et_Provincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is DropDownList ddlProvincias)
            {
                if (ddlProvincias.SelectedIndex != 0)
                {
                    ddlProvincias.Items[0].Enabled = false;
                }
            }
        }

        protected void ddl_et_Especialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is DropDownList ddlEspecialidades)
            {
                if (ddlEspecialidades.SelectedIndex != 0)
                {
                    ddlEspecialidades.Items[0].Enabled = false;
                }
            }

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
