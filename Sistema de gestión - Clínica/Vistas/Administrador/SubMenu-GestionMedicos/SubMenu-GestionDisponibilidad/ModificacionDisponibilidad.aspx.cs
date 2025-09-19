using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace Vistas.Administrador.SubMenu_GestionDisponibilidad
{
	public partial class ModificacionDisponibilidad : System.Web.UI.Page
	{
        //Variable del form
        private readonly NegocioDisponibilidad disponibilidad = new NegocioDisponibilidad();

        //Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Codigo para que anden las validaciones
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            //verifico que se haya iniciado como admin
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Postback
            if (!IsPostBack)
            {
                //Marco el usuario como admin
                lblUsuarioAdministrador.Text = "Administrador";

                //Cargo la tabla
                cargarGV();
            }
        }
        
        //Funcion cargar gv
        private void cargarGV()
        {
            DataTable tabla = disponibilidad.TablaDisponibilidad();
            gvModificacionDisponibilidad.DataSource = tabla;
            gvModificacionDisponibilidad.DataBind();
        }

        //Cambio de pagina
        protected void gvModificacionDisponibilidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModificacionDisponibilidad.PageIndex = e.NewPageIndex;
            gvModificacionDisponibilidad.EditIndex = -1;
            cargarGV();
        }

        //Edicion de grilla
        protected void gvModificacionDisponibilidad_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModificacionDisponibilidad.EditIndex = e.NewEditIndex;
            cargarGV();
        }

        //Cancelar edicion
        protected void gvModificacionDisponibilidad_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Cancelar y cargar gv
            gvModificacionDisponibilidad.EditIndex = -1;
            cargarGV();
        }

        //Actualizar edicion
        protected void gvModificacionDisponibilidad_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Me guardo el nombre del dia
            string numeroDia = ((Label)gvModificacionDisponibilidad.Rows[e.RowIndex].FindControl("lbl_eit_Dia")).Text;

            //Creo un obj disponibilidad
            Disponibilidad disp = new Disponibilidad();

            //Lo paso a numero
            switch (numeroDia)
            {
                case "Lunes":
                    disp.NumDia = 1;
                    break;

                case "Martes":
                    disp.NumDia = 2;
                    break;

                case "Miércoles":
                    disp.NumDia = 3;
                    break;

                case "Jueves":
                    disp.NumDia = 4;
                    break;

                case "Viernes":
                    disp.NumDia = 5;
                    break;

                case "Sábado":
                    disp.NumDia = 6;
                    break;

                case "Domingo":
                    disp.NumDia = 7;
                    break;
            }

            //Seteo el resto de parametros en un objeto disponibilidad 
            disp.LegajoMedico = Convert.ToInt32(((Label)gvModificacionDisponibilidad.Rows[e.RowIndex].FindControl("lbl_eit_Legajo")).Text);
            disp.Estado = ((CheckBox)gvModificacionDisponibilidad.Rows[e.RowIndex].FindControl("cb_eit_Estado")).Checked;
            disp.HorarioInicio = TimeSpan.Parse(((DropDownList)gvModificacionDisponibilidad.Rows[e.RowIndex].FindControl("ddl_eit_HoraInicio")).SelectedItem.ToString());
            disp.HorarioFin = TimeSpan.Parse(((DropDownList)gvModificacionDisponibilidad.Rows[e.RowIndex].FindControl("ddl_eit_HoraFin")).SelectedItem.ToString());

            if (disp.HorarioInicio >= disp.HorarioFin)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "El horario de inicio debe ser menor al horario de fin.";
                return;
            }

            //Ejecuto el update
            if (disponibilidad.ModificarDisponibilidad(disp))
            {
                //Muestro mensaje
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Se modifico la base de datos.";
            }
            else
            {
                //Muestro mensaje
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se pudo modificar la base de datos.";
            }

            //Termino la actualizacion
            gvModificacionDisponibilidad.EditIndex = -1;
            cargarGV();

        }

        private void CargarHorarios(DropDownList ddlInicio, DropDownList ddlFin )
        {
            ddlInicio.Items.Insert(0, new ListItem("-- Hora Inicio --", ""));
            ddlFin.Items.Insert(0,new ListItem("-- Hora fin --", ""));

            TimeSpan hora = new TimeSpan(8, 0, 0); // 08:00
            TimeSpan horaFin = new TimeSpan(20, 0, 0); // 20:00
            TimeSpan intervalo = new TimeSpan(1, 0, 0); // 1 hora 

            while (hora <= horaFin)
            {
                string horaStr = hora.ToString(@"hh\:mm");
                ddlInicio.Items.Add(new ListItem(horaStr, horaStr));
                ddlFin.Items.Add(new ListItem(horaStr, horaStr));
                hora = hora.Add(intervalo);
            }
        }

        protected void gvModificacionDisponibilidad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool encontrado = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlInicio = (DropDownList)e.Row.FindControl("ddl_eit_HoraInicio");
                    DropDownList ddlFin = (DropDownList)e.Row.FindControl("ddl_eit_HoraFin");

                    CargarHorarios(ddlInicio, ddlFin);

                    string horaInicio = DataBinder.Eval(e.Row.DataItem, "Inicio").ToString();
                    string horaFin = DataBinder.Eval(e.Row.DataItem, "Fin").ToString();

                    foreach (ListItem item in ddlInicio.Items)
                    {
                        if (item.Text + ":00" == horaInicio)
                        {
                            encontrado = true;
                            item.Selected = true;
                            break;
                        }
                    }
                    if (!encontrado)
                    {
                        ddlInicio.SelectedIndex = 0; 
                    }
                    else
                    {
                        encontrado = false;
                    }

                    foreach (ListItem item in ddlFin.Items)
                    {
                        if (item.Text + ":00" == horaFin)
                        {
                            encontrado = true;
                            item.Selected = true;
                            break;
                        }
                    }
                    if (!encontrado)
                    {
                        ddlFin.SelectedIndex = 0;
                    }

                    CheckBox cbEstado = (CheckBox)e.Row.FindControl("cb_eit_Estado");
                    string Estado = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
                    if (Estado.ToLower() == "true")
                    {
                        cbEstado.Checked = true;
                    }
                    else
                    {
                        cbEstado.Checked = false;
                    }
                }
            }
        }

        protected void ddl_eit_HoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is DropDownList ddlInicio)
            {
                if (ddlInicio.SelectedIndex != 0)
                {
                    ddlInicio.Items[0].Enabled = false; 
                }
            }
        }

        protected void ddl_eit_HoraFin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is DropDownList ddlFin)
            {
                if (ddlFin.SelectedIndex != 0)
                {
                    ddlFin.Items[0].Enabled = false;
                }
            }
        }
    }
}