using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador.SubCarpeta_Reportes_Informes
{
    public partial class ResultadosReportes : System.Web.UI.Page
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
            
            if (Session["TituloInforme"] != null)
            {
                lblTituloInforme.Text = Session["TituloInforme"].ToString();
            }

            if (Session["TablaResultados"] != null)
            {
                gvResultados.DataSource = Session["TablaResultados"];
                gvResultados.DataBind();
            }
            else
            {
                gvResultados.DataSource = null;
                gvResultados.DataBind();
                lblTituloInforme.Text = null;

                lblMensaje.Text = "No hay resultados disponibles para realizar el informe especificado.";
            }
        }
        
        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultados.PageIndex = e.NewPageIndex;
            
            if (Session["TablaResultados"] != null)
            {
                gvResultados.DataSource = Session["TablaResultados"];
                gvResultados.DataBind();
            }
        }
    }
}