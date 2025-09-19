using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;
using static System.Collections.Specialized.BitVector32;

namespace Vistas.Administrador.SubMenu_GestionPacientes
{
    public partial class ListarPacientes : System.Web.UI.Page
    {
        private readonly NegocioPaciente negocioPaciente = new NegocioPaciente();
        private Paciente paciente = new Paciente();
        private bool[,] filtros = new bool[3, 3];

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
                CargarTablaPacientes();
                CargarProvincias();
                Session["TablaFiltrada"] = null;
            }
        }

        private void CargarProvincias()
        {
            gvProvincias.DataSource = negocioPaciente.getRegistrosProvincias();
            gvProvincias.DataBind();
        }

        private void CargarTablaPacientes()
        {
            gvListadoPacientes.DataSource = negocioPaciente.ObtenerPacientes();
            gvListadoPacientes.DataBind();
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

        private bool[,] VerificarFiltroAvanzado()
        {

            if (txtDniPaciente.Text?.Trim().Length > 0)
            {
                string seleccion = ddlOperatorsDni.SelectedValue.Trim();    
                switch (seleccion)
                {
                    case "1":
                        filtros[0, 0] = true;
                        break;

                    case "2":
                        filtros[0, 1] = true;
                        break;

                    case "3":
                        filtros[0, 2] = true;
                        break;
                    default:

                        break;
                }
            }

            if (txtNombrePaciente.Text?.Trim().Length > 0)
            {
                switch (ddlOperatorsNombre.SelectedValue.Trim())
                {
                    case "1":
                        filtros[1, 0] = true;
                        break;

                    case "2":
                        filtros[1, 1] = true;
                        break;

                    case "3":
                        filtros[1, 2] = true;
                        break;
                }
            }

            if (txtTelefonoPaciente.Text?.Trim().Length > 0)
            {
                switch (ddlOperatorsTelefono.SelectedValue.Trim())
                {
                    case "1":
                        filtros[2, 0] = true;
                        break;

                    case "2":
                        filtros[2, 1] = true;
                        break;

                    case "3":
                        filtros[2, 2] = true;
                        break;
                }
            }

            return filtros;
        }

        private bool IsFiltrosVacios()
        {
            if (string.IsNullOrWhiteSpace(txtDniPaciente.Text) && string.IsNullOrWhiteSpace(txtNombrePaciente.Text) && string.IsNullOrWhiteSpace(txtTelefonoPaciente.Text))
            {
                return true;
            }
            return false;
        }

        protected void btnAplicarFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                txtFiltroDNIPaciente.Text = string.Empty;

                paciente.Dni = txtDniPaciente.Text?.Trim();
                paciente.Nombre = txtNombrePaciente.Text?.Trim();
                paciente.Telefono = txtTelefonoPaciente.Text?.Trim();

                if (IsFiltrosVacios())
                {
                    lblFiltrosAvanzadosVacios.ForeColor = System.Drawing.Color.Red;
                    lblFiltrosAvanzadosVacios.Text = "No se llenó ningún filtro particular.";
                    return;
                }
                else
                {
                    lblFiltrosAvanzadosVacios.Text = string.Empty;
                }

                filtros = VerificarFiltroAvanzado();

                DataTable tablaFiltrada = negocioPaciente.ObtenerPacientes_Filtrados(paciente, true, filtros);
                Session["TablaFiltrada"] = tablaFiltrada;

                if (tablaFiltrada.Rows.Count == 0)
                {
                    lblFiltrosAvanzadosVacios.ForeColor = System.Drawing.Color.Red;
                    lblFiltrosAvanzadosVacios.Text = "No se encontraron resultados con los filtros aplicados.";
                }

                gvListadoPacientes.PageIndex = 0;
                gvListadoPacientes.DataSource = tablaFiltrada;
                gvListadoPacientes.DataBind();
                paciente = new Paciente();
            }

            LimpiarFiltrosAvanzados();
        }

        private void LimpiarFiltrosAvanzados()
        {
            txtDniPaciente.Text = string.Empty;
            txtNombrePaciente.Text = string.Empty;
            txtTelefonoPaciente.Text = string.Empty;
            ddlOperatorsDni.SelectedIndex = 0;
            ddlOperatorsNombre.SelectedIndex = 0;
            ddlOperatorsTelefono.SelectedIndex = 0;
            filtros = new bool[3, 3];
        }

        protected void btnLimpiarFiltrosPacientes_Click(object sender, EventArgs e)
        {
            if (IsFiltrosVacios())
            {
                lblFiltrosAvanzadosVacios.ForeColor = System.Drawing.Color.Red;
                lblFiltrosAvanzadosVacios.Text = "No se llenó ningún filtro particular.";
                return;
            }
            else
            {
                Session["TablaFiltrada"] = null;
                LimpiarFiltrosAvanzados();
                gvListadoPacientes.PageIndex = 0;
                CargarTablaPacientes();
            }

            lblFiltrosAvanzadosVacios.Text = string.Empty;
        }

        protected void btnFiltrarPacienteDNI_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblFiltrosAvanzadosVacios.Text = string.Empty;
                lblMensaje.Text = string.Empty;

                paciente.Dni = txtFiltroDNIPaciente.Text.Trim();
                DataTable tablaFiltrada = negocioPaciente.ObtenerPacientes_Filtrados(paciente, false, filtros);
                Session["TablaFiltrada"] = tablaFiltrada;

                if (tablaFiltrada.Rows.Count == 0)
                {
                    lblDNInoEncontrado.ForeColor = System.Drawing.Color.Red;
                    lblDNInoEncontrado.Text = "El DNI ingresado no existe.";
                }
                else
                {
                    lblDNInoEncontrado.Text = string.Empty;
                }

                gvListadoPacientes.PageIndex = 0;
                gvListadoPacientes.DataSource = tablaFiltrada;
                gvListadoPacientes.DataBind();
                paciente = new Paciente();
                txtFiltroDNIPaciente.Text = string.Empty;
            }
        }

        protected void btnMostrarTodosPacientes_Click(object sender, EventArgs e)
        {
            LimpiarFiltrosAvanzados();
            txtFiltroDNIPaciente.Text = string.Empty;
            lblDNInoEncontrado.Text = string.Empty;
            Session["TablaFiltrada"] = null;

            gvListadoPacientes.PageIndex = 0; 
            CargarTablaPacientes();
        }

        protected void gvListadoPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListadoPacientes.PageIndex = e.NewPageIndex;
            if (Session["TablaFiltrada"] != null)
            {
                gvListadoPacientes.DataSource = (DataTable)Session["TablaFiltrada"];
                gvListadoPacientes.DataBind();
            }
            else
            {
                CargarTablaPacientes();
            }
        }

        protected void btnProvincia_Command1(System.Object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "FiltroProvincias")
            {
                paciente.CodProvincia = Convert.ToInt32(e.CommandArgument);
                DataTable tabla = negocioPaciente.ObtenerPacientes_Filtrados(paciente, false, filtros);
                gvListadoPacientes.DataSource = tabla;
                gvListadoPacientes.DataBind();
                if (tabla.Rows.Count <= 0)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se encontraron resultados para la provincia seleccionada.";
                }
                else
                {
                    lblMensaje.Text = string.Empty;
                }

                gvListadoPacientes.PageIndex = 0;
                paciente.CodProvincia = 0;
            }
        }
    }
}