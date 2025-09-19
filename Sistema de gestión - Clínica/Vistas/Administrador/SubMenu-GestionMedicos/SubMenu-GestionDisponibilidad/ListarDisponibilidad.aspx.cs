using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;

namespace Vistas.Administrador.SubMenu_GestionDisponibilidad
{
	public partial class ListarDisponibilidad : System.Web.UI.Page
	{

        private readonly NegocioDisponibilidad negocioDisponibilidad = new NegocioDisponibilidad();
        private readonly NegocioEspecialidad negocioEspecialidad = new NegocioEspecialidad();
        private readonly NegocioDia negocioDia = new NegocioDia();


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
                CargarEspecialidades();
                CargarDisponibilidad();
                CargarDia();
            }
        }

        private void CargarEspecialidades()
        {
            gvEspecialidades.DataSource = negocioEspecialidad.ObtenerEspecialidades();
            gvEspecialidades.DataBind();
        }

        private void CargarDia()
        {
            DataTable tablaDias = negocioDia.ObtenerTablaDia();

            DataRow filaTodos = tablaDias.NewRow();
            filaTodos["Descripcion_DI"] = "Todos";
            filaTodos["NumDia_DI"] = 0; // Value = '0'

            tablaDias.Rows.InsertAt(filaTodos, 0); // lo agrega en la posición 0 (principio)

            gvDias.DataSource = tablaDias;
            gvDias.DataBind();
        }

        private void CargarDisponibilidad()
        {
            Session["TablaDisponibilidad"] = negocioDisponibilidad.ObtenerTablaDisponibilidad(0, 0, 0);
            gvDisponibilidades.DataSource = (DataTable)Session["TablaDisponibilidad"];

            gvDisponibilidades.DataBind();            
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            ResetearColoresBotonesEspecialidad();
            ResetearColoresBotonesDia();
            LimpiarFiltros();

            Session["NombreEspecialidad"] = null;
            lblDisponibilidadActiva.Text = "Usted está viendo: disponibilidades para todos los días.";

            CargarDisponibilidad();

            VerificarNumeroRegistros();
        }

        protected void btnEspecialidad_Command(object sender, CommandEventArgs e)
        {
            ResetearColoresBotonesEspecialidad();
            ResetearColoresBotonesDia();
            LimpiarFiltros();

            Button btnEspecialidadSeleccionada = (Button)sender;
            btnEspecialidadSeleccionada.BackColor = Color.DarkGray;
            
            int codEspecialidad = Convert.ToInt32(e.CommandArgument);
            string nombreEspecialidad = btnEspecialidadSeleccionada.Text;

            if (e.CommandName == "FiltroEspecialidad")
            {                
                Session["CodEspecialidad"] = codEspecialidad;
                Session["NombreEspecialidad"] = nombreEspecialidad;      
                Session["TablaDisponibilidad"] = negocioDisponibilidad.ObtenerTablaDisponibilidad(codEspecialidad, 0, 0);

                gvDisponibilidades.DataSource = (DataTable)Session["TablaDisponibilidad"];
                gvDisponibilidades.DataBind();
                lblDisponibilidadActiva.Text = $"Usted está viendo: disponibilidades para {nombreEspecialidad} para todos los días.";

                VerificarNumeroRegistros();
            }
        }

        protected void btnDia_Command(object sender, CommandEventArgs e)
        {
            ResetearColoresBotonesDia();
            LimpiarFiltros();

            DataTable tablaDisponibilidad;

            Button btnDiaSeleccionado = (Button)sender;
            btnDiaSeleccionado.BackColor = Color.DarkGray;

            int numDiaSeleccionado  = Convert.ToInt32(e.CommandArgument);
            string nombreDia = btnDiaSeleccionado.Text;

            if(Session["NombreEspecialidad"] != null)
            {
                int codEspecialidad = (int)Session["CodEspecialidad"];
                string nombreEspecialidad = (string)Session["NombreEspecialidad"];

                if (numDiaSeleccionado > 0)
                {
                    tablaDisponibilidad = negocioDisponibilidad.ObtenerTablaDisponibilidad(codEspecialidad, numDiaSeleccionado, 0);
                    lblDisponibilidadActiva.Text = $"Usted está viendo: disponibilidades para {nombreEspecialidad} para los días {nombreDia}.";
                }
                else
                {
                    tablaDisponibilidad = negocioDisponibilidad.ObtenerTablaDisponibilidad(codEspecialidad, 0, 0);
                    lblDisponibilidadActiva.Text = $"Usted está viendo: disponibilidades para {nombreEspecialidad} para todos los días.";
                } 
                
            }

            else
            {
                if(numDiaSeleccionado > 0)
                {
                    tablaDisponibilidad = negocioDisponibilidad.ObtenerTablaDisponibilidad(0, numDiaSeleccionado, 0);
                    lblDisponibilidadActiva.Text = $"Usted está viendo: disponibilidades para los días {nombreDia}.";
                }
                else
                {
                    tablaDisponibilidad = negocioDisponibilidad.ObtenerTablaDisponibilidad(0, 0, 0);
                    lblDisponibilidadActiva.Text = "Usted está viendo: disponibilidades para todos los días.";
                }                               
            }

            Session["TablaDisponibilidad"] = tablaDisponibilidad;

            gvDisponibilidades.DataSource = tablaDisponibilidad;
            gvDisponibilidades.DataBind();

            VerificarNumeroRegistros();            
        }

        protected void btnFiltrarMedicoLegajo_Click(object sender, EventArgs e)
        {
            ResetearColoresBotonesDia();
            ResetearColoresBotonesEspecialidad();


            int legajoMedico = Convert.ToInt32(txtFiltroLegajoMedico.Text);
            string numeroLegajo = legajoMedico.ToString();

            Session["TablaDisponibilidad"] = negocioDisponibilidad.ObtenerTablaDisponibilidad(0, 0, legajoMedico);

            gvDisponibilidades.DataSource = (DataTable)Session["TablaDisponibilidad"];
            gvDisponibilidades.DataBind();

            if(gvDisponibilidades.Rows.Count == 0)
            {
                lblLegajoNoEncontrado.Visible = true;
                lblSinRegistros.Visible = false;
                lblDisponibilidadActiva.Text = string.Empty;                
            }
            else
            {
                lblDisponibilidadActiva.Text = $"Usted está viendo: disponibilidades para el médico con legajo {numeroLegajo} para todos los días.";
                lblLegajoNoEncontrado.Visible = false;
            }

            Session["CodEspecialidad"] = null;
            Session["NombreEspecialidad"] = null;

            LimpiarFiltros();
          
        }
        

        protected void btnAplicarFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            
            ResetearColoresBotonesDia();
            ResetearColoresBotonesEspecialidad();

            string cadenaFiltroAvanzado = ObtenerCadenaFiltroAvanzado();
            
            if(cadenaFiltroAvanzado == null)
            {
                lblSinFiltroAvanzado.Visible = true;
                return;
            }

            else
            {
                Session["TablaDisponibilidad"] = negocioDisponibilidad.ObtenerTablaDisponibilidadFiltroAvanzado(cadenaFiltroAvanzado);
                gvDisponibilidades.DataSource = (DataTable)Session["TablaDisponibilidad"];
                gvDisponibilidades.DataBind();
            }

            lblDisponibilidadActiva.Text = string.Empty;
            lblSinFiltroAvanzado.Visible = false;
            LimpiarFiltros();
        }

 

        protected void gvDisponibilidades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDisponibilidades.PageIndex = e.NewPageIndex;

            if (Session["TablaDisponibilidad"] != null)
            {
                gvDisponibilidades.DataSource = (DataTable)Session["TablaDisponibilidad"];
                gvDisponibilidades.DataBind();
            }
        }

        private string ObtenerCadenaFiltroAvanzado()
        {
            string cadena = null;

            string parametroRangoHorario = ddlOperatorsRangoHorario.SelectedValue;
            string parametroEspecialidad = ddlOperatorsEspecialidad.SelectedValue;

            string horario = ddlSeleccionarHorario.SelectedValue;
            string especialidad = txtEspecialidad.Text;

            

            if (horario != "0")
            {
                cadena = " WHERE ";

                switch (parametroRangoHorario)
                {
                    case "a partir de":
                        cadena += $"HorarioInicio_DIS >= '{horario}'";
                        break;

                    default:
                        cadena += $"HorarioInicio_DIS < '{horario}'";
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtEspecialidad.Text))
            {
                if(horario != "0")
                {
                    cadena += " AND ";
                }
                else
                {
                    cadena = " WHERE ";
                }

                switch (parametroEspecialidad)
                {
                    case "contiene":
                        cadena += $"E.Descripcion_ES LIKE '%{especialidad}%'";
                        break;
                    case "empieza con":
                        cadena += $"E.Descripcion_ES LIKE '{especialidad}%'";
                        break;
                    default:
                        cadena += $"E.Descripcion_ES LIKE '%{especialidad}'";
                        break;
                }
            }

            return cadena;
        }



        private void ResetearColoresBotonesEspecialidad()
        {
            // Especialidades
            foreach (GridViewRow fila in gvEspecialidades.Rows)
            {
                Button btn = (Button)fila.FindControl("btnEspecialidad");
                if (btn != null)
                {
                    btn.BackColor = System.Drawing.Color.Empty;
                }
            }
        }

        private void ResetearColoresBotonesDia()
        {
            foreach (GridViewRow fila in gvDias.Rows)
            {
                Button btn = (Button)fila.FindControl("btnDia");
                if (btn != null)
                {
                    btn.BackColor = System.Drawing.Color.Empty;
                }
            }
        }

        private void LimpiarFiltros()
        {
            txtFiltroLegajoMedico.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;

        }

        private void VerificarNumeroRegistros()
        {
            if (gvDisponibilidades.Rows.Count == 0)
            {
                lblSinRegistros.Visible = true;
            }
            else
            {
                lblLegajoNoEncontrado.Visible = false;
                lblSinRegistros.Visible = false;
            }
        }


        protected void btnLimpiarFiltrosMedicos_Click(object sender, EventArgs e)
        {
            ResetearColoresBotonesDia();
            ResetearColoresBotonesEspecialidad();

            ddlOperatorsRangoHorario.SelectedIndex = 0;
            ddlOperatorsEspecialidad.SelectedIndex = 0;
            ddlSeleccionarHorario.SelectedIndex = 0;
            txtEspecialidad.Text = string.Empty;
        }


        protected void btnMenuFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            if (btnMenuFiltrosAvanzados.Text == "Aplicar Filtros Avanzados")
            {
                pnlFiltrosAvanzados.Visible = true;
                btnMenuFiltrosAvanzados.Text = "Ocultar Filtros Avanzados";
            }
            else
            {
                pnlFiltrosAvanzados.Visible = false;
                btnMenuFiltrosAvanzados.Text = "Aplicar Filtros Avanzados";
            }
        }
    }
}