using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace Vistas.Administrador.SubMenu_GestionDisponibilidad
{
    public partial class BajaDisponibilidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
            }
        }

        protected void txtLegajoBaja_TextChanged(object sender, EventArgs e)
        { 
            string legajo = txtLegajoBaja.Text.Trim();

            if (!string.IsNullOrEmpty(legajo))
            {
                if (int.TryParse(legajo, out int legajoInt) && legajoInt > 0)
                {
                    lblResultadoBaja.Text = string.Empty;
                    pnlDias.Visible = true; 
                    NegocioDisponibilidad negocioDisponibilidad = new NegocioDisponibilidad();
                    Disponibilidad disponibilidad = new Disponibilidad();
                    disponibilidad.LegajoMedico = legajoInt;
                    CargarDDL(disponibilidad);
                    if(ddlDisponibilidad.Items.Count == 1)
                    {
                        pnlDias.Visible = false;
                        lblResultadoBaja.Text = "No se encontraron disponibilidades para el legajo médico ingresado.";
                    }
                    else
                    {
                        lblResultadoBaja.Text = "Seleccione un día de la semana para realizar la baja de disponibilidad.";
                    }
                }
                else
                {
                    pnlDias.Visible = false;
                    lblResultadoBaja.Text = "Por favor, ingresá un legajo medico válido.";
                }
            }
            else
            {
                pnlDias.Visible = false;
                lblResultadoBaja.Text = "Por favor, ingresá un legajo medico.";
            }
        }

        public void CargarDDL(Disponibilidad disponibilidad)
        {
            NegocioDisponibilidad negocioDisponibilidad = new NegocioDisponibilidad();
            ddlDisponibilidad.DataSource = negocioDisponibilidad.ObtenerDisponibilidad(disponibilidad);
            ddlDisponibilidad.DataTextField = "Día Semana";
            ddlDisponibilidad.DataValueField = "Numero Día Semana";
            ddlDisponibilidad.DataBind();
            ddlDisponibilidad.Items.Insert(0, new ListItem("Seleccione un día", "0"));
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string legajo = txtLegajoBaja.Text.Trim();
            if (pnlDias.Visible == true)
            {
                if(ddlDisponibilidad.SelectedIndex != 0)
                {
                    Disponibilidad disponibilidad = new Disponibilidad();
                    disponibilidad.LegajoMedico = int.Parse(legajo);
                    disponibilidad.NumDia = int.Parse(ddlDisponibilidad.SelectedValue);
                    NegocioDisponibilidad negocioDisponibilidad = new NegocioDisponibilidad();
                    int filasAfectadas = negocioDisponibilidad.BajaLogicaDisponibilidad(disponibilidad);
                    if (filasAfectadas == 1)
                    {
                        lblResultadoBaja.ForeColor = System.Drawing.Color.Green;
                        lblResultadoBaja.Text = "La baja de disponibilidad se realizó correctamente.";
                        pnlDias.Visible = false;
                    }
                    else
                    {
                        lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
                        lblResultadoBaja.Text = "No se pudo realizar la baja de disponibilidad. Verifique los datos ingresados.";
                    }
                    txtLegajoBaja.Text = string.Empty;
                }
                else
                {
                    lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
                    lblResultadoBaja.Text = "Por favor, seleccione un día de la semana.";
                }
            }
            else
            {
                lblResultadoBaja.ForeColor = System.Drawing.Color.Red;
                lblResultadoBaja.Text = "Por favor, ingresá un legajo medico.";
            }
        }

        protected void ddlDisponibilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDisponibilidad.SelectedIndex != 0)
            {
                ddlDisponibilidad.Items[0].Enabled = true;
            }
        }
    }
}