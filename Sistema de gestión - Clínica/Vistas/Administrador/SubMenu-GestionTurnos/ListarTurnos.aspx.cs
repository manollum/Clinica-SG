using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador.SubMenu_GestionTurnos
{
    public partial class ListarTurnos : System.Web.UI.Page
    {
        //Variable del form
        private readonly NegocioTurno negocioTurno = new NegocioTurno();

        //page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Codigo para que anden las validaciones
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            //Si se ingreso por error que devuelva a login
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Postback
            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
                cargarGV();
                Session["TablaFiltrada"] = null;
            }
        }

        //Filtro avanzado
        protected void btnAplicarFiltroAvanzado0_Click(object sender, EventArgs e)
        {
            if(panelListarTurnos.Visible == false)
            {
                panelListarTurnos.Visible = true;
                btnMostrarFiltrosAvanzado0.Text = "Ocultar Filtros Avanzados";
            }
            else
            {
                panelListarTurnos.Visible = false;
                btnMostrarFiltrosAvanzado0.Text = "Aplicar Filtros Avanzados";
            }
        }

        //Funcion que carga la gv con parametros default
        private void cargarGV()
        {
            DataTable tabla = negocioTurno.getTabla();
            gvTablaTurnos.DataSource = tabla;
            Session["TablaFiltrada"] = tabla; 
            gvTablaTurnos.DataBind();
        }

        //Evento de cambio de pagina
        protected void gvTablaTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTablaTurnos.PageIndex = e.NewPageIndex;
            if (Session["TablaFiltrada"] != null)
            {
                gvTablaTurnos.DataSource = (DataTable)Session["TablaFiltrada"];
                gvTablaTurnos.DataBind();
            }
            else
            {
                cargarGV();
            }
        }

        //boton de filtro principal
        protected void btnFiltarTurno_Click(object sender, EventArgs e)
        {
            //Relleno el dataTable y lo bindeo
            
            DataTable tabla = negocioTurno.getTablaPorCodigoTurno(Convert.ToInt32(txtListarTurno.Text.Trim()));
            Session["TablaFiltrada"] = tabla; 
            gvTablaTurnos.DataSource = tabla;
            gvTablaTurnos.DataBind();

            //Si la tabla tiene 0 filas pongo un mensaje diciendo que no se pudo encontrar nada
            if (tabla.Rows.Count <= 0)
            {
                lblMensaje.Text = "No se ha encontrado ninguna sucursal " + txtListarTurno.Text.Trim() + ".";
            }
            else
            {
                lblMensaje.Text = "";
            }

            //Limpio el txt
            txtListarTurno.Text = "";
        }

        //Limpio filtros
        protected void btnLimpiarFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            //Cargo la gv normal
            cargarGV();

            //Limpio los filtros
            txtListarTurno.Text = "";
            txtFiltroDni.Text = "";
            txtFiltroFecha.Text = "";
            ddlDniPaciente.SelectedValue = "0";
        }

        //Evento click filtro avanzado
        protected void btnAplicarFiltroAvanzado_Click(object sender, EventArgs e)
        {
            //Me fijo si hay algun campo seleccionado
            if( (string.IsNullOrEmpty(txtFiltroFecha.Text) && txtFiltroDni.Text == string.Empty) || (ddlDniPaciente.SelectedValue == "0" && (string.IsNullOrEmpty(txtFiltroFecha.Text))))
            {
                //Mensaje
                lblResultadoFiltroAvanzado.ForeColor = System.Drawing.Color.Red;
                lblResultadoFiltroAvanzado.Text = "*Se deben ingresar campos para poder usar los filtros avanzados.";
            }
            else
            {
                //Variable
                DateTime? fecha;
                try
                {
                    fecha = Convert.ToDateTime(txtFiltroFecha.Text);
                }
                catch
                {
                    fecha = null;
                }
                DataTable dt = negocioTurno.getTablaFiltrada(fecha, txtFiltroDni.Text, ddlDniPaciente.SelectedValue);
                Session["TablaFiltrada"] = dt; 
                gvTablaTurnos.DataSource = dt;
                gvTablaTurnos.DataBind();

                //Mensaje si no encontro nada
                if(dt.Rows.Count == 0)
                {
                    lblResultadoFiltroAvanzado.ForeColor = System.Drawing.Color.Red;
                    lblResultadoFiltroAvanzado.Text = "*No se encontraron resultados con esos filtros.";
                }

                //Limpio los filtros
                txtListarTurno.Text = "";
                txtFiltroDni.Text = "";
                txtFiltroFecha.Text = "";
                ddlDniPaciente.SelectedValue = "0";
            }
        }
    }
}