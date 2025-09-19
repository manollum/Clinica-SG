using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
	public partial class ListarMedicos : System.Web.UI.Page
	{
        private readonly NegocioMedico negocioMedico = new Negocios.NegocioMedico();
        private Entidades.Medico medico;
        private bool aplicarFiltroAvanzado = false;
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

                CargarTablaMedicos();
                CargarDias();
                Session["TablaFiltrada"] = null;
            }
        }

        private void CargarDias()
        {
            gvDisponibilidad.DataSource = negocioMedico.getDias();
            gvDisponibilidad.DataBind();
        }

        private void CargarTablaMedicos()
        {
            gvListaMedicos.DataSource = negocioMedico.getTablaMedicos();
            gvListaMedicos.DataBind();
        }

        protected void gvListaMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaMedicos.PageIndex = e.NewPageIndex;
            if (Session["TablaFiltrada"] != null)
            {
                gvListaMedicos.DataSource = (DataTable)Session["TablaFiltrada"];
                gvListaMedicos.DataBind();
            }
            else
            {
                CargarTablaMedicos();
            }
        }

        protected void btnFiltrarMedicoLegajo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                medico = new Entidades.Medico();
                medico.Legajo = Convert.ToInt32(txtFiltroLegajoMedico.Text.Trim());
                aplicarFiltroAvanzado = false;

                DataTable tablaFiltrada = negocioMedico.getTablaMedicosFiltrada(medico, aplicarFiltroAvanzado, filtros);
                Session["TablaFiltrada"] = tablaFiltrada;

                if (tablaFiltrada.Rows.Count == 0)
                {
                    lblLegajoNoEncontrado.Text = "El legajo ingresado no existe.";
                    gvListaMedicos.DataSource = null;
                    gvListaMedicos.DataBind();
                }
                else
                {
                    lblLegajoNoEncontrado.Text = string.Empty; // Oculta mensaje si sí encontró                    
                    gvListaMedicos.PageIndex = 0;
                    gvListaMedicos.DataSource = tablaFiltrada;
                    gvListaMedicos.DataBind();
                }

                txtFiltroLegajoMedico.Text = string.Empty;
                lblFiltrosAvanzadosVacios.Text = string.Empty;
            }
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            aplicarFiltroAvanzado = false;
            Session["TablaFiltrada"] = null;

            gvListaMedicos.PageIndex = 0;
            CargarTablaMedicos();

            txtFiltroLegajoMedico.Text = string.Empty;
            lblFiltrosAvanzadosVacios.Text = string.Empty;
            lblLegajoNoEncontrado.Text = string.Empty;
        }

        protected void btnMenuFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            if (btnMenuFiltrosAvanzados.Text == "Aplicar Filtros Avanzados")
            {
                pnlFiltrosAvanzados.Visible = true;
                btnMenuFiltrosAvanzados.Text = "Ocultar Filtros Avanzados";
                lblFiltrosAvanzadosVacios.Text = string.Empty;
            }
            else
            {
                pnlFiltrosAvanzados.Visible = false;
                btnMenuFiltrosAvanzados.Text = "Aplicar Filtros Avanzados";
            }
        }

        protected void btnAplicarFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                medico = new Entidades.Medico();
                aplicarFiltroAvanzado = true;
                txtFiltroLegajoMedico.Text = string.Empty;

                medico.DNI = txtIDniMedico.Text.Trim();
                medico.Apellido = txtApellidoMedico.Text.Trim();
                medico.Correo = txtCorreoMedico.Text.Trim();

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

                DataTable tablaFiltrada = negocioMedico.getTablaMedicosFiltrada(medico, aplicarFiltroAvanzado, filtros);
                Session["TablaFiltrada"] = tablaFiltrada;

                if (tablaFiltrada.Rows.Count == 0)
                {
                    lblFiltrosAvanzadosVacios.ForeColor = System.Drawing.Color.Red;
                    lblFiltrosAvanzadosVacios.Text = "No se encontraron resultados con los filtros aplicados.";
                }

                gvListaMedicos.PageIndex = 0;
                gvListaMedicos.DataSource = tablaFiltrada;
                gvListaMedicos.DataBind();

                LimpiarTxtFiltrosAvanzados();
                LimpiarValoresFiltrosAvanzados();
            }
        }

        protected void btnLimpiarFiltrosMedicos_Click(object sender, EventArgs e)
        {
            aplicarFiltroAvanzado = false;
            
            if (IsFiltrosVacios())
            {
                LimpiarTxtFiltrosAvanzados();
                lblFiltrosAvanzadosVacios.ForeColor = System.Drawing.Color.Red;
                lblFiltrosAvanzadosVacios.Text = "No se llenó ningún filtro particular.";
                return;
            }
            else
            {
                Session["TablaFiltrada"] = null;
                LimpiarTxtFiltrosAvanzados();
                LimpiarValoresFiltrosAvanzados();
                gvListaMedicos.PageIndex = 0;
                CargarTablaMedicos();
            }
            
            lblFiltrosAvanzadosVacios.Text = string.Empty;
        }

        private bool IsFiltrosVacios()
        {
            if ((txtIDniMedico.Text.Trim().Length == 0) && (txtApellidoMedico.Text.Trim().Length == 0) && (txtCorreoMedico.Text.Trim().Length == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool[,] VerificarFiltroAvanzado()
        {
            if (IsFiltrosVacios())
            {
                return filtros;
            }

            if (txtIDniMedico.Text.Trim().Length > 0 && ddlOperatorsDni.SelectedValue != null)
            {
                switch (ddlOperatorsDni.SelectedValue)
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
                }
            }

            if (txtApellidoMedico.Text.Trim().Length > 0 && ddlOperatorsApellido.SelectedValue != null)
            {
                switch (ddlOperatorsApellido.SelectedValue)
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

            if (txtCorreoMedico.Text.Trim().Length > 0 && ddlOperatorsCorreo.SelectedValue != null)
            {
                switch (ddlOperatorsCorreo.SelectedValue)
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

        private void LimpiarTxtFiltrosAvanzados()
        {
            txtFiltroLegajoMedico.Text = string.Empty;
            txtIDniMedico.Text = string.Empty;
            txtApellidoMedico.Text = string.Empty;
            txtCorreoMedico.Text = string.Empty;
            ddlOperatorsDni.SelectedIndex = 0;
            ddlOperatorsApellido.SelectedIndex = 0;
            ddlOperatorsCorreo.SelectedIndex = 0;
        }

        private void LimpiarValoresFiltrosAvanzados()
        {
            for (int i = 0; i < filtros.GetLength(0); i++)
            {
                for (int j = 0; j < filtros.GetLength(1); j++)
                {
                    filtros[i, j] = false;
                }
            }
        }

        protected void btnDia_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "FiltroDias")
            {
                medico = new Entidades.Medico();
                medico.DiaDisponible = Convert.ToInt32(e.CommandArgument);
                
                DataTable tablaFiltrada = negocioMedico.getTablaMedicosFiltrada(medico, false, filtros);
                Session["TablaFiltrada"] = tablaFiltrada;
                
                gvListaMedicos.PageIndex = 0;
                gvListaMedicos.DataSource = tablaFiltrada;
                gvListaMedicos.DataBind();
                
                if (tablaFiltrada.Rows.Count == 0)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se encontraron resultados para el día seleccionado.";
                }
                else
                {
                    lblMensaje.Text = string.Empty;
                }
            }
        }
    }
}