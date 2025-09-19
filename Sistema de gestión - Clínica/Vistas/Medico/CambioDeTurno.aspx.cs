using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using Entidades;
using Negocios;


namespace Vistas.Medico
{
    public partial class CambioDeTurno : System.Web.UI.Page
    {
        private NegocioTurno negocioTurno;
        private Turno turno;

        private NegocioPaciente negocioPaciente;
        private List<Paciente> listaPacientesSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Session["legajo"] == null)
            {
                lblMensaje.Text = "No se ha logrado encontrar el registro de legajo médico. Por favor, inicie sesión nuevamente.";
            }

            if (!IsPostBack)
            {
                lblUsuario.Text = Session["usuario"].ToString();
                CargarTurnosMedico();
                lblMensaje.Text = "Solo se habilita la edición para los turnos no pendientes.";
            }
        }

        private void CargarTurnosMedico()
        {
            negocioTurno = new NegocioTurno();
            DataTable TurnosMedico = negocioTurno.getTurnosXMedico(Convert.ToInt32(Session["legajo"]));
            gvActualizacionTurnos.DataSource = TurnosMedico;
            gvActualizacionTurnos.DataBind();
            if (TurnosMedico.Rows.Count <= 0)
            {
                lblMensaje.Text = "No se encontraron registros de turnos vigentes.";
            }
        }

        protected void gvActualizacionTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvActualizacionTurnos.EditIndex = e.NewEditIndex;
            CargarTurnosMedico();
        }

        protected void gvActualizacionTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvActualizacionTurnos.EditIndex = -1;
            CargarTurnosMedico();
        }

        protected void gvActualizacionTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActualizacionTurnos.EditIndex = -1;
            gvActualizacionTurnos.PageIndex = e.NewPageIndex;
            CargarTurnosMedico();
        }

        protected void gvActualizacionTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            negocioTurno = new NegocioTurno();
            turno = new Turno();

            turno.CodTurno = Convert.ToInt32(((Label)gvActualizacionTurnos.Rows[e.RowIndex].FindControl("lbl_et_IDTurno")).Text);
            turno.Fecha = DateTime.Parse(((Label)gvActualizacionTurnos.Rows[e.RowIndex].FindControl("lbl_et_Fecha")).Text);
            turno.Asistencia = ((RadioButtonList)gvActualizacionTurnos.Rows[e.RowIndex].FindControl("rbl_et_Asistencia")).SelectedValue;
            turno.Descripcion = ((TextBox)gvActualizacionTurnos.Rows[e.RowIndex].FindControl("txt_et_Descripcion")).Text;
            turno.Pendiente = 0;
            turno.Estado = true;


            int resultado = negocioTurno.ModificarTurno(turno);
            if (resultado > 0)
            {
                lblMensaje.Text = "Turno actualizado correctamente.";
                gvActualizacionTurnos.EditIndex = -1;
                CargarTurnosMedico();
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el turno. Verifique los datos ingresados.";
            }
        }

        protected void gvActualizacionTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    ProcesarFilaVisualizacion(e.Row);
                }
                else
                {
                    ProcesarFilaEdicion(e.Row, e.Row.DataItem);
                }
            }
        }

        private void ProcesarFilaVisualizacion(GridViewRow row)
        {
            Label lblFecha = (Label)row.FindControl("lbl_it_Fecha");
            DateTime fechaTurno = Convert.ToDateTime(lblFecha.Text);

            HabilitarBotonEditar(row, fechaTurno);

            Label lblAsistencia = (Label)row.FindControl("lbl_it_Asistencia");
            FormatearAsistencia(lblAsistencia);

            Label lblDescripcion = (Label)row.FindControl("lbl_it_Descripcion");
            MostrarDescripcionPorDefecto(lblDescripcion);
        }

        private void HabilitarBotonEditar(GridViewRow row, DateTime fechaTurno)
        {
            LinkButton btnEditar = (LinkButton)row.FindControl("lbtn_it_Editar");
            if(fechaTurno <= DateTime.Now)
            {
                btnEditar.Enabled = true;
            }
            else
            {
                btnEditar.Enabled = false;
            }
        }

        private void FormatearAsistencia(Label lblAsistencia)
        {
            if (lblAsistencia.Text != null)
            {
                string asistencia = lblAsistencia.Text.Trim().ToLower();
                if (asistencia == "false")
                {
                    lblAsistencia.Text = "Ausente";
                }
                else if (asistencia == "true")
                {
                    lblAsistencia.Text = "Presente";
                }
                else
                {
                    lblAsistencia.Text = "Sin registrar";
                }
            }
            else
            {
                lblAsistencia.Text = "Sin registrar";
            }
        }

        private void MostrarDescripcionPorDefecto(Label lblDescripcion)
        {
            if (string.IsNullOrEmpty(lblDescripcion.Text))
            {
                lblDescripcion.Text = "Sin Completar";
            }
        }

        private void ProcesarFilaEdicion(GridViewRow row, object dataItem)
        {
            RadioButtonList rblAsistencia = (RadioButtonList)row.FindControl("rbl_et_Asistencia");
            string asistencia = DataBinder.Eval(dataItem, "Asistencia")?.ToString();

            SeleccionarOpcionAsistencia(rblAsistencia, asistencia);

            TextBox txtDescripcion = (TextBox)row.FindControl("txt_et_Descripcion");
            if(string.IsNullOrEmpty(txtDescripcion.Text))
            {
                txtDescripcion.Text = "Sin Completar";
            }
        }

        private void SeleccionarOpcionAsistencia(RadioButtonList rblAsistencia, string asistencia)
        {
            if (!string.IsNullOrEmpty(asistencia))
            {
                if (asistencia.ToLower() == "true")
                {
                    rblAsistencia.SelectedValue = "1";
                }
                else if (asistencia.ToLower() == "false")
                {
                    rblAsistencia.SelectedValue = "0";
                }
            }
            else
            {
                rblAsistencia.ClearSelection();
            }
        }

        protected void btnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "Select")
            {
                int codTurno = Convert.ToInt32(e.CommandArgument);

                if (Session["PacientesSeleccionados"] != null)
                {
                    listaPacientesSeleccionado = Session["PacientesSeleccionados"] as List<Paciente>;
                }                    
                else
                {
                    listaPacientesSeleccionado = new List<Paciente>();
                }
                    
                negocioPaciente = new NegocioPaciente();
                                                
                Paciente pacienteSeleccionado = negocioPaciente.ObtenerPacienteXTurno(codTurno);

                if (listaPacientesSeleccionado.Any(p => p.Legajo == pacienteSeleccionado.Legajo))
                {
                    return;
                }                    

                listaPacientesSeleccionado.Add(pacienteSeleccionado);                

                Session["PacientesSeleccionados"] = listaPacientesSeleccionado;
            }
        }
    }
}