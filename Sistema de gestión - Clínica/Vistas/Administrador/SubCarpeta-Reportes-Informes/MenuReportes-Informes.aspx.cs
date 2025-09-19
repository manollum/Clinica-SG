using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Vistas.Administrador.SubCarpeta_Reportes_Informes
{
	public partial class MenuReportes_Informes : System.Web.UI.Page
	{
        private DataTable dataTable;
        private NegocioMedico negocioMedico;
        private NegocioTurno negocioTurno;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
                Session["TablaResultados"] = null;
                Session["TituloInforme"] = null;
            }
        }

        protected void btnMedicoMasSolicitado_Click(object sender, EventArgs e)
        {
            negocioMedico = new NegocioMedico();
            dataTable = negocioMedico.ObtenerMedicoMasSolicitado();

            Session["TablaResultados"] = dataTable;
            Session["TituloInforme"] = "Médico Más Solicitado";

            Response.Redirect("ResultadosReportes-Informes.aspx");
        }

        protected void btnbtnPromedioMEspecialidad_Click(object sender, EventArgs e)
        {
            negocioMedico = new NegocioMedico();
            dataTable = negocioMedico.ObtenerMedicosXEspecialidad();
            
            Session["TablaResultados"] = dataTable;
            Session["TituloInforme"] = "Cantidad de Médicos por Especialidad";
            
            Response.Redirect("ResultadosReportes-Informes.aspx");
        }

        protected void btnPorcentajeAsistenciaTurnos_Click(object sender, EventArgs e)
        {
            negocioTurno = new NegocioTurno();
            dataTable = negocioTurno.ObtenerPorcentajeTurnosPorDia();

            Session["TablaResultados"] = dataTable;
            Session["TituloInforme"] = "Porcentaje de Asistencia de Turnos por Día";

            Response.Redirect("ResultadosReportes-Informes.aspx");
        }

        protected void btnHorarioMasSolicitado_Click(object sender, EventArgs e)
        {
            negocioTurno = new NegocioTurno();
            dataTable = negocioTurno.ObtenerTablaHorarioMasSolicitado();

            Session["TablaResultados"] = dataTable;
            Session["TituloInforme"] = "Horario mas solicitado";

            Response.Redirect("ResultadosReportes-Informes.aspx");
        }
    }
}