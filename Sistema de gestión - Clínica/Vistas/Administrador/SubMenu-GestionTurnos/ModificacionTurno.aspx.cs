using Entidades;
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
    public partial class ModificacionTurno : System.Web.UI.Page
    {
        private readonly NegocioTurno negocioTurno = new NegocioTurno();
        private readonly NegocioDisponibilidad negocioDisponibilidad = new NegocioDisponibilidad();
        private int[,] Calendario = new int[12, 31];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioAdministrador.Text = "Administrador";
                cargarGV();
                gvModificarTurnos.RowDataBound += gvModificarTurnos_RowDataBound;
                Session["fechaModificable"] = false;
            }
            else
            {
                lblModificacionMensaje.Text = string.Empty;
            }
        }

        private void cargarGV()
        {
            gvModificarTurnos.DataSource = negocioTurno.getTabla();
            gvModificarTurnos.DataBind();
        }

        protected void gvModificarTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModificarTurnos.EditIndex = -1;
            gvModificarTurnos.PageIndex = e.NewPageIndex;
            cargarGV();
        }

        protected void gvModificarTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModificarTurnos.EditIndex = -1; // Salir de modo edición
            cargarGV(); // Recargar la tabla sin cambios
        }

        protected void gvModificarTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModificarTurnos.EditIndex = e.NewEditIndex;
            cargarGV(); // Vuelve a cargar el GridView mostrando los controles editables en esa fila
        }

        protected void gvModificarTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Turno turnoModificado = new Turno();

                GridViewRow fila = gvModificarTurnos.Rows[e.RowIndex];

                // Obtener el ID del turno (clave primaria)
                int idConsulta = Convert.ToInt32(gvModificarTurnos.DataKeys[e.RowIndex].Value);

                turnoModificado.CodTurno = idConsulta;

                bool  bandera = Convert.ToBoolean(Session["fechaModificable"]);
                if (bandera == true)
                {
                    DropDownList ddlFechas = fila.FindControl("ddl_et_FechasTurnos") as DropDownList;
                    DateTime fecha = Convert.ToDateTime(ddlFechas.SelectedItem.Text);
                    turnoModificado.Fecha = fecha; 
                }
                else
                {
                    Label lblFechaTurnoActual = fila.FindControl("lbl_et_Turno") as Label;
                    DateTime fecha = Convert.ToDateTime(lblFechaTurnoActual.Text);
                    turnoModificado.Fecha = fecha;
                }

                // Obtener el CheckBox de Pendiente
                CheckBox chkPendiente = fila.FindControl("chk_eit_Pendiente") as CheckBox;
                int pendiente = (chkPendiente != null && chkPendiente.Checked) ? 1 : 0;
                turnoModificado.Pendiente = pendiente;

                // Obtener el CheckBox de Asistencia
                CheckBox chkAsistencia = fila.FindControl("chk_eit_Asistencia") as CheckBox;
                string asistencia = (chkAsistencia != null && chkAsistencia.Checked) ? "1" : "0";
                turnoModificado.Asistencia = asistencia;

                // Traer la descripción actual (ya que la SP la requiere)
                TextBox txtDescripcion = fila.FindControl("txt_et_Descripcion") as TextBox;
                string descripcion = txtDescripcion != null ? txtDescripcion.Text.Trim() : "Sin Completar";
                turnoModificado.Descripcion = descripcion;

                turnoModificado.Estado = true;

                // Llamar a NegocioTurno para actualizar en la BD
                int filasAfectadas = negocioTurno.ModificarTurno(turnoModificado);

                if (filasAfectadas > 0)
                {
                    lblModificacionMensaje.ForeColor = System.Drawing.Color.Green;
                    lblModificacionMensaje.Text = "Modificación exitosa.";
                }
                else
                {
                    lblModificacionMensaje.ForeColor = System.Drawing.Color.Red;
                    lblModificacionMensaje.Text = "No fue posible realizar la modificación.";
                }

                gvModificarTurnos.EditIndex = -1;
                cargarGV();
            }
            catch (Exception ex)
            {
                lblModificacionMensaje.ForeColor = System.Drawing.Color.Red;
                lblModificacionMensaje.Text = "❌ Error: " + ex.Message;
            }
        }

        protected void gvModificarTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Nos aseguramos de que es una fila de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificamos si la fila está en modo de edición
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    //Encontrar el control dentro de la fila
                    CheckBox chkPendiente = e.Row.FindControl("chk_eit_Pendiente") as CheckBox;
                    CheckBox chkAsistencia = e.Row.FindControl("chk_eit_Asistencia") as CheckBox;
                    TextBox txtDescripcion = (TextBox)e.Row.FindControl("txt_et_Descripcion");
                    if (string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        txtDescripcion.Text = "Sin Completar";
                    }

                    Label lblFechaTurnoActual = e.Row.FindControl("lbl_et_Turno") as Label;
                    DropDownList ddlfechasTurnos = e.Row.FindControl("ddl_et_FechasTurnos") as DropDownList;

                    if (chkPendiente.Checked)
                    {
                        txtDescripcion.Enabled = false;
                        lblModificacionMensaje.ForeColor = System.Drawing.Color.Blue;
                        lblModificacionMensaje.Text = "Este turno aun esta pendiente, por lo que su descripcion aun no es modificables.";

                        Label lblIDConsulta = e.Row.FindControl("lbl_et_IDConsulta") as Label;
                        Label legajo = e.Row.FindControl("lbl_et_Legajo") as Label;

                        int idTurno = Convert.ToInt32(lblIDConsulta.Text);
                        int legajoMedico = Convert.ToInt32(legajo.Text);
                        DateTime fechaTurnoActual = Convert.ToDateTime(lblFechaTurnoActual.Text);

                        List<DateTime> fechasTurnosLibres = ObtenerTurnosDisponibles(idTurno, fechaTurnoActual);

                        CargarDDL_Fechas(fechasTurnosLibres, ddlfechasTurnos);

                        string fechaSeleccionada = fechaTurnoActual.ToString();

                        for (int i = 0; i < ddlfechasTurnos.Items.Count; i++)
                        {

                            if (ddlfechasTurnos.Items[i].Text == fechaSeleccionada)
                            {

                                ddlfechasTurnos.SelectedIndex = i;
                                break;
                            }
                        }

                        if (ddlfechasTurnos.SelectedIndex == -1)
                        {
                            if (ddlfechasTurnos.Items.Count <= 0)
                            {
                                lblModificacionMensaje.ForeColor = System.Drawing.Color.Blue;
                                lblModificacionMensaje.Text = "Actualmente este medico no cuenta con disponibilidad vigente para poder modificar la fecha de este turno.";
                                lblFechaTurnoActual.Visible = true;
                                ddlfechasTurnos.Visible = false;
                            }
                            else
                            {
                                ddlfechasTurnos.Items.Insert(0, new ListItem("Seleccione una fecha", "0"));
                                Session["fechaModificable"] = true;
                            }

                            ddlfechasTurnos.SelectedIndex = 0;
                        }
                        else
                        {
                            Session["fechaModificable"] = true;
                        }
                    }

                    else
                    {
                        lblModificacionMensaje.ForeColor = System.Drawing.Color.Blue;
                        lblModificacionMensaje.Text = "Este turno no esta pendiente, por lo que su fecha no es modificable.";
                        txtDescripcion.Enabled = true;
                        lblFechaTurnoActual.Visible = true;
                        ddlfechasTurnos.Visible = false;
                        Session["fechaModificable"] = false;
                    }
                }
            }
        }

        private void Calendario_Vacio()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    Calendario[i, j] = 0;
                }
            }
        }

        private int[,] ObtenerCalendarioMedico(int idTurno)
        {
            Calendario_Vacio();
            DataTable registroTurno = negocioTurno.getTablaPorCodigoTurno(idTurno);
            int legajo = Convert.ToInt32(registroTurno.Rows[0]["Legajo Medico"]);
            List<Disponibilidad> listaDisponibilidad = negocioDisponibilidad.ObtenerListaDisponibilidadMedico(legajo);

            for (int i = 0; i < 12; i++)
            {
                Calendario[i, 0] = i + 1;
                int diasMes = DateTime.DaysInMonth(DateTime.Now.Year, i+1);

                for (int j = 0; j < diasMes; j++)
                {
                    DateTime fecha = new DateTime(DateTime.Now.Year, i + 1, j + 1);
                    int numeroDia = ((int)fecha.DayOfWeek == 0) ? 7 : (int)fecha.DayOfWeek;

                    foreach (Disponibilidad disponibilidad in listaDisponibilidad)
                    {
                        if (disponibilidad.NumDia == numeroDia)
                        {
                            Calendario[i, j] = j + 1;
                        }
                    }
                }
            }

            return Calendario;
        }

        private List<TimeSpan> ObtenerHoras(int legajoMedico, List<Disponibilidad> ListaDisponibilidad)
        {
            List<TimeSpan> horasDisponibles = new List<TimeSpan>();

            for (int i = 0; i < ListaDisponibilidad.Count; i++)
            {
                TimeSpan horaInicio = ListaDisponibilidad[i].HorarioInicio;
                TimeSpan horaFin = ListaDisponibilidad[i].HorarioFin;

                while (horaInicio <= horaFin)
                {
                    horasDisponibles.Add(horaInicio);
                    horaInicio = horaInicio.Add(new TimeSpan(1, 0, 0));
                }
            }

            return horasDisponibles;
        }

        private List<DateTime> ListarFechasTurnos(int[,] calendario, int legajoMedico)
        {
            List<DateTime> fechasTurnos = new List<DateTime>();
            List<Disponibilidad> ListaDisponibilidad = negocioDisponibilidad.ObtenerListaDisponibilidadMedico(legajoMedico);
            List<TimeSpan> horarios;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (calendario[i, j] != 0)
                    {
                        DateTime fecha = new DateTime(DateTime.Now.Year, i + 1, j + 1);

                        int numeroDia = ((int)fecha.DayOfWeek == 0) ? 7 : (int)fecha.DayOfWeek;

                        horarios = ObtenerHoras(legajoMedico, ListaDisponibilidad.Where(d => d.NumDia == numeroDia).ToList());

                        for (int k = 0; k < horarios.Count; k++)
                        {
                            TimeSpan hora = horarios[k];
                            DateTime fechaTurno = new DateTime(fecha.Year, fecha.Month, calendario[i, j], hora.Hours, hora.Minutes, 0);
                            if (fechaTurno >= DateTime.Now)
                            {
                                fechasTurnos.Add(fechaTurno);
                            }
                        }
                    }
                }
            }

            return fechasTurnos;
        }

        private List<DateTime> DescontarTurnosOcupados(List<DateTime> fechasTurnos, int legajoMedico, DateTime fechaTurnoActual)
        {
            List<Turno> turnosOcupados = negocioTurno.ObtenerListaTurnos(legajoMedico);

            for (int i = fechasTurnos.Count - 1; i >= 0; i--)
            {
                DateTime fechaPotencial = fechasTurnos[i];

                for(int j = 0; j < turnosOcupados.Count; j++)
                {
                    if (turnosOcupados[j].Fecha == fechaPotencial)
                    {
                        if (turnosOcupados[j].Fecha != fechaTurnoActual)
                        {
                            fechasTurnos.RemoveAt(i);
                            break; 
                        }
                    }
                }
            }

            return fechasTurnos;
        }

        private List<DateTime> ObtenerTurnosDisponibles(int idTurno, DateTime fechaTurnoActual)
        {
            //Obtener el calendario del médico para el turno dado
            int[,] calendario = ObtenerCalendarioMedico(idTurno);

            //Obtener el legajo del médico a partir del idTurno
            DataTable registroTurno = negocioTurno.getTablaPorCodigoTurno(idTurno);
            int legajoMedico = Convert.ToInt32(registroTurno.Rows[0]["Legajo Medico"]);

            //Listar todas las fechas posibles de turnos según el calendario, legajo y especialidad
            List<DateTime> fechasTurnos = ListarFechasTurnos(calendario, legajoMedico);

            //Descontar los turnos ya ocupados
            List<DateTime> turnosDisponibles = DescontarTurnosOcupados(fechasTurnos, legajoMedico, fechaTurnoActual);

            return turnosDisponibles;
        }

        private void CargarDDL_Fechas(List<DateTime> fechasTurnosLibres, DropDownList ddlfechasTurnos)
        {
            ddlfechasTurnos.DataSource = fechasTurnosLibres;
            ddlfechasTurnos.DataBind();
        }
    }
}
