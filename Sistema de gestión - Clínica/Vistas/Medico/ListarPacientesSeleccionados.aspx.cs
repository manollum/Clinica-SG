using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidades;

namespace Vistas.Medico
{
    public partial class ListarPacientesSeleccionados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuario.Text = Session["usuario"].ToString();

                if(Session["PacientesSeleccionados"] != null)
                {
                    lblSinPacientes.Visible = false;
                    gvPacientes.DataSource = Session["PacientesSeleccionados"] as List<Paciente>;
                    gvPacientes.DataBind();

                    
                }
                else
                {
                    lblSinPacientes.Visible = true;
                }
            }
        }

        protected void btnEliminarTodo_Click(object sender, EventArgs e)
        {
            Session["PacientesSeleccionados"] = null;
            gvPacientes.DataSource = null;
            gvPacientes.DataBind();

            lblSinPacientes.Visible = true;
        }
    }
}