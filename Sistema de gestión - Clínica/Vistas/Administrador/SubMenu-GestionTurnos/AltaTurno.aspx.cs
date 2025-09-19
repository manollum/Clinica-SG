using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Administrador.SubMenu_GestionTurnos
{
    public partial class AltaTurno : System.Web.UI.Page
    {
        private readonly NegocioDisponibilidad negocioDisponibilidad = new NegocioDisponibilidad();
        private readonly NegocioEspecialidad negocioEspecialidad = new NegocioEspecialidad();
        private readonly NegocioMedico negocioMedico = new NegocioMedico();
        private readonly NegocioTurno negocioTurno = new NegocioTurno();

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
                CargarDDLEspecialidad();
            }
        }

        private void CargarDDLEspecialidad()
        {
            try
            {
                ddlEspecialidad.DataSource = negocioEspecialidad.ObtenerSqlDataReaderEspecialidad();
                ddlEspecialidad.DataTextField = "Descripcion_ES";
                ddlEspecialidad.DataValueField = "CodEspecialidad_ES";
                ddlEspecialidad.DataBind();

                ddlEspecialidad.Items.Insert(0, new ListItem("--Seleccione--", "0"));

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error al cargar especialidades.";
            }
        }

        private void CargarDDLMedico(string cod)
        {
            try
            {
                if (cod != "0")
                {
                    ddlMedico.DataSource = negocioMedico.ObtenerSqlDataReaderMedicoPorEspecialidad(cod); ;
                    ddlMedico.DataTextField = "Medico";
                    ddlMedico.DataValueField = "Legajo_ME";
                    ddlMedico.DataBind(); 
                    
                    ddlMedico.Items.Insert(0, new ListItem("--Seleccione un médico--", "0"));
                }

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = "Error al cargar médicos.";
            }
        }

        private void CargarDDLSemana(int legajoMedico)
        {
            List<Disponibilidad> listaDisponibilidadMedico = negocioDisponibilidad.ObtenerListaDisponibilidadMedico(legajoMedico);
            List<Turno> listaTurnos = negocioTurno.ObtenerListaTurnos(legajoMedico);

            ddlSemana.Items.Clear();

            DateTime hoy = DateTime.Now.Date;
            int diaSemanaHoy = ((int)hoy.DayOfWeek + 6) % 7; // lunes = 0

            // Calcular lunes de esta semana
            DateTime lunesActual = hoy.AddDays(-diaSemanaHoy);

            for (int semana = 0; semana < 7; semana++)
            {
                DateTime inicioSemana = lunesActual.AddDays(semana * 7);
                DateTime finSemana = inicioSemana.AddDays(6);
                int disponiblesEnSemana = 0;

                foreach (var d in listaDisponibilidadMedico)
                {
                    DateTime fechaDelDia = inicioSemana.AddDays(d.NumDia - 1);
                    bool esHoy = fechaDelDia.Date == DateTime.Now.Date;
                    bool yaPasoTodoHoy = esHoy && d.HorarioFin <= DateTime.Now.TimeOfDay;
                    bool diaCompletamentePasado = fechaDelDia.Date < hoy;

                    if (diaCompletamentePasado || yaPasoTodoHoy)
                        continue;

                    int cantidadTurnos = 0;
                    foreach (var turno in listaTurnos)
                    {
                        if (turno.Estado && turno.Fecha.Date == fechaDelDia.Date)
                            cantidadTurnos++;
                    }

                    int maxTurnos = (int)(d.HorarioFin - d.HorarioInicio).TotalHours;
                    int disponibles = maxTurnos - cantidadTurnos;

                    // En el día actual, descartamos turnos que ya pasaron la hora
                    if (esHoy)
                    {
                        TimeSpan hora = d.HorarioInicio;
                        while (hora < d.HorarioFin)
                        {
                            DateTime posibleTurno = fechaDelDia.Add(hora);
                            if (posibleTurno <= DateTime.Now)
                            {
                                hora = hora.Add(TimeSpan.FromHours(1));
                                continue;
                            }

                            bool ocupado = false;
                            foreach (var t in listaTurnos)
                            {
                                if (t.Estado && t.Fecha == posibleTurno)
                                {
                                    ocupado = true;
                                    break;
                                }
                            }

                            if (!ocupado)
                                disponiblesEnSemana++;

                            hora = hora.Add(TimeSpan.FromHours(1));
                        }
                    }
                    else if (disponibles > 0)
                    {
                        disponiblesEnSemana += disponibles;
                    }
                }

                if (disponiblesEnSemana > 0)
                {
                    string texto = $"{inicioSemana:dd-MM} al {finSemana:dd-MM} ({disponiblesEnSemana} disponibles)";
                    string valor = inicioSemana.ToString("yyyy-MM-dd");
                    ddlSemana.Items.Add(new ListItem(texto, valor));
                }
            }

            if (ddlSemana.Items.Count == 0)
            {
                ddlSemana.Items.Add(new ListItem("No hay semanas con turnos disponibles", "0"));
            }
        }

        private void CargarDDLDia(int legajoMedico)
        {
            List<Disponibilidad> listaDisponibilidadMedico = negocioDisponibilidad.ObtenerListaDisponibilidadMedico(legajoMedico);
            List<Turno> listaTurnos = negocioTurno.ObtenerListaTurnos(legajoMedico);

            DateTime fechaInicioSemana = DateTime.ParseExact(ddlSemana.SelectedValue, "yyyy-MM-dd", null);
            DateTime ahora = DateTime.Now;

            ddlDia.Items.Clear();

            foreach (var d in listaDisponibilidadMedico)
            {
                DateTime fechaDelDia = fechaInicioSemana.AddDays(d.NumDia - 1);
                bool esHoy = fechaDelDia.Date == ahora.Date;
                bool yaPasoTodoElBloqueHoy = esHoy && d.HorarioFin <= ahora.TimeOfDay;
                bool diaCompletamenteEnElPasado = fechaDelDia.Date < ahora.Date;

                if (diaCompletamenteEnElPasado || yaPasoTodoElBloqueHoy)
                    continue; // ignorar días ya expirados

                // Contar cuántos turnos ya hay cargados para ese día
                int cantidadTurnos = 0;
                foreach (var turno in listaTurnos)
                {
                    if (turno.Fecha.Date == fechaDelDia.Date)
                        cantidadTurnos++;
                }

                int maxTurnos = (int)(d.HorarioFin - d.HorarioInicio).TotalHours;
                int disponibles = maxTurnos - cantidadTurnos;

                // En el día actual, eliminar también los horarios que ya pasaron
                if (esHoy)
                {
                    TimeSpan hora = d.HorarioInicio;
                    int bloquesDisponibles = 0;

                    while (hora < d.HorarioFin)
                    {
                        DateTime turnoHora = fechaDelDia.Add(hora);
                        bool yaPaso = turnoHora <= ahora;
                        bool ocupado = false;

                        foreach (var t in listaTurnos)
                        {
                            if (t.Fecha == turnoHora && t.Estado)
                            {
                                ocupado = true;
                                break;
                            }
                        }

                        if (!yaPaso && !ocupado)
                            bloquesDisponibles++;

                        hora = hora.Add(TimeSpan.FromHours(1));
                    }

                    if (bloquesDisponibles > 0)
                    {
                        string texto = $"{d.NombreDia} ({bloquesDisponibles} disponibles)";
                        ddlDia.Items.Add(new ListItem(texto, d.NumDia.ToString()));
                    }
                }
                else if (disponibles > 0)
                {
                    string texto = $"{d.NombreDia} ({disponibles} disponibles)";
                    ddlDia.Items.Add(new ListItem(texto, d.NumDia.ToString()));
                }
            }

            ddlDia.Items.Insert(0, new ListItem("-- Seleccione un día --", "0"));
        }

        private void CargarDDLHorario(int legajoMedico, int numDiaSeleccionado)
        {
            ddlHorario.Items.Clear();

            List<Disponibilidad> listaDisponibilidadMedico = negocioDisponibilidad.ObtenerListaDisponibilidadMedico(legajoMedico);
            List<Turno> listaTurnos = negocioTurno.ObtenerListaTurnos(legajoMedico);
            DateTime fechaSeleccionada = ObtenerFechaSeleccionada();
            DateTime ahora = DateTime.Now;

            // Buscar la disponibilidad correspondiente al día seleccionado
            Disponibilidad disponibilidad = null;

            foreach (Disponibilidad d in listaDisponibilidadMedico)
            {
                if (d.NumDia == numDiaSeleccionado)
                {
                    disponibilidad = d;
                    break;
                }
            }

            // Si no tiene disponibilidad ese día, mostramos un mensaje
            if (disponibilidad == null)
            {
                ddlHorario.Items.Add(new ListItem("No hay horarios disponibles", "0"));
                return;
            }

            // Obtener todas las horas ya ocupadas del médico en esa fecha
            List<TimeSpan> horasOcupadas = new List<TimeSpan>();

            foreach (Turno turno in listaTurnos)
            {
                if (turno.Fecha.Date == fechaSeleccionada.Date)
                {
                    horasOcupadas.Add(turno.Fecha.TimeOfDay);
                }
            }

            TimeSpan horaInicio = disponibilidad.HorarioInicio;
            TimeSpan horaFin = disponibilidad.HorarioFin;

            while (horaInicio < horaFin)
            {
                DateTime horaCompleta = fechaSeleccionada.Add(horaInicio);

                bool esHoy = fechaSeleccionada.Date == ahora.Date;
                bool yaPaso = esHoy && horaCompleta <= ahora;

                if (!yaPaso && !horasOcupadas.Contains(horaInicio))
                {
                    string franja = $"{horaInicio:hh\\:mm} a {horaInicio.Add(TimeSpan.FromHours(1)):hh\\:mm}";
                    ddlHorario.Items.Add(new ListItem(franja, horaInicio.ToString(@"hh\:mm")));
                }

                horaInicio = horaInicio.Add(TimeSpan.FromHours(1));
            }

            ddlHorario.Items.Insert(0, new ListItem("-- Seleccione un horario --", "0"));
        }

        private DateTime ObtenerFechaSeleccionada()
        {
            // Recuperar la fecha de inicio de la semana desde el Value del ddlSemana
            DateTime fechaInicioSemana = DateTime.ParseExact(ddlSemana.SelectedValue, "yyyy-MM-dd", null);

            // Obtener el número del día (1 = lunes ... 7 = domingo)
            int numDia = int.Parse(ddlDia.SelectedValue);

            // Calcular la fecha exacta del día dentro de esa semana
            DateTime fechaSeleccionada = fechaInicioSemana.AddDays(numDia - 1);

            return fechaSeleccionada;
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cod = ddlEspecialidad.SelectedValue;

            ddlMedico.Items.Clear();
            ddlSemana.Items.Clear();
            ddlDia.Items.Clear();
            ddlHorario.Items.Clear();
            lblInfoTurnos.Text = string.Empty;

            if (ddlEspecialidad.SelectedIndex != 0)
            {
                ddlEspecialidad.Items[0].Enabled = false;
            }

            //Cargar médicos y restaurar selección si corresponde
            CargarDDLMedico(cod);
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSemana.Items.Clear();
            ddlDia.Items.Clear();
            ddlHorario.Items.Clear();
            lblInfoTurnos.Text = string.Empty;

            if (ddlMedico.SelectedIndex != 0)
            {
                ddlMedico.Items[0].Enabled = false;
            }

            int legajoMedico = Convert.ToInt32(ddlMedico.SelectedValue);

            List<Disponibilidad> listaDisponibilidadMedico = negocioDisponibilidad.ObtenerListaDisponibilidadMedico(legajoMedico);
            List<Turno> listaTurnos = negocioTurno.ObtenerListaTurnos(legajoMedico);

            if (listaTurnos != null)
            {
                int cantidadDisponibles = CalcularTurnosDisponibles(listaDisponibilidadMedico, listaTurnos);

                if (cantidadDisponibles > 0)
                {
                    lblInfoTurnos.Text = $"Este médico tiene {cantidadDisponibles} turnos disponibles en las próximas semanas.";
                    lblInfoTurnos.ForeColor = System.Drawing.Color.Black;
                }
                else if (ddlMedico.SelectedValue != "0")
                {
                    lblInfoTurnos.Text = "Este médico no tiene turnos próximos disponibles.";
                    lblInfoTurnos.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblInfoTurnos.Text = "No se encontraron datos de turnos para este médico.";
                lblInfoTurnos.ForeColor = System.Drawing.Color.Red;
            }

            if (legajoMedico != 0)
            {
                CargarDDLSemana(legajoMedico);

                if (ddlSemana.Items.Count == 0)
                {
                    ddlSemana.Items.Add("No hay turnos disponibles para esta semana...");
                    return;
                }
                else
                {
                    ddlSemana.Items.Insert(0, new ListItem("-Seleccione una opción-", "0"));
                }
            }
        }

        private int CalcularTurnosDisponibles(List<Disponibilidad> listaDisponibilidad, List<Turno> listaTurnos)
        {
            int disponibles = 0;

            DateTime hoy = DateTime.Now.Date;
            int diaSemanaHoy = ((int)hoy.DayOfWeek + 6) % 7; // lunes = 0
            DateTime lunesActual = hoy.AddDays(-diaSemanaHoy);

            for (int semana = 0; semana < 7; semana++)
            {
                DateTime inicioSemana = lunesActual.AddDays(semana * 7);

                foreach (var disp in listaDisponibilidad)
                {
                    DateTime fechaDia = inicioSemana.AddDays(disp.NumDia - 1);
                    bool esHoy = fechaDia.Date == DateTime.Now.Date;
                    bool diaPasado = fechaDia.Date < DateTime.Now.Date;
                    bool yaPasoTodoHoy = esHoy && disp.HorarioFin <= DateTime.Now.TimeOfDay;

                    if (diaPasado || yaPasoTodoHoy)
                        continue;

                    TimeSpan hora = disp.HorarioInicio;
                    while (hora < disp.HorarioFin)
                    {
                        DateTime posibleTurno = fechaDia.Add(hora);
                        if (esHoy && posibleTurno <= DateTime.Now)
                        {
                            hora = hora.Add(TimeSpan.FromHours(1));
                            continue;
                        }

                        bool ocupado = false;
                        foreach (var t in listaTurnos)
                        {
                            if (t.Estado && t.Fecha == posibleTurno)
                            {
                                ocupado = true;
                                break;
                            }
                        }

                        if (!ocupado)
                            disponibles++;

                        hora = hora.Add(TimeSpan.FromHours(1));
                    }
                }
            }

            return disponibles;
        }

        protected void ddlSemana_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDia.Items.Clear();
            ddlHorario.Items.Clear();

            if (ddlSemana.SelectedValue != "0")
            {
                ddlSemana.Items[0].Enabled = false;
                int legajoMedico = Convert.ToInt32(ddlMedico.SelectedValue);

                CargarDDLDia(legajoMedico);
            }
        }

        protected void ddlDia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlHorario.Items.Clear();

            if (ddlDia.SelectedValue != "0")
            {
                ddlDia.Items[0].Enabled = false;
                int legajoMedico = Convert.ToInt32(ddlMedico.SelectedValue);
                int diaSeleccionado = int.Parse(ddlDia.SelectedValue);

                CargarDDLHorario(legajoMedico, diaSeleccionado);
            }
        }

        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHorario.SelectedValue != "0")
            {
                ddlHorario.Items[0].Enabled = false;
            }
        }

        protected void btnAgregarT_Click(object sender, EventArgs e)
        {
            if (ddlMedico.SelectedValue == "0" || ddlDia.SelectedValue == "0" || ddlSemana.SelectedValue == "0" || ddlHorario.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtDniPaciente.Text.Trim()))
            {
                lblResultadoAltaT.Text = "Debe completar todos los campos.";
                lblResultadoAltaT.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                int legajoMedico = int.Parse(ddlMedico.SelectedValue);
                string dniPaciente = txtDniPaciente.Text;
                int legajoPaciente = negocioTurno.ObtenerLegajoPacientePorDNI(dniPaciente);
                string descripcion = "";

                if (legajoPaciente <= 0)
                {
                    lblResultadoAltaT.Text = "No se encontró un paciente con ese DNI.";
                    lblResultadoAltaT.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                DateTime fechaTurno = ObtenerFechaSeleccionada();
                TimeSpan horaSeleccionada = TimeSpan.Parse(ddlHorario.SelectedValue);
                DateTime fechaYHoraTurno = fechaTurno.Add(horaSeleccionada);

                if (negocioTurno.ValidarTurnoMedicoOcupado(legajoMedico, fechaYHoraTurno))
                {
                    lblResultadoAltaT.Text = "El médico ya tiene un turno asignado en ese horario.";
                    lblResultadoAltaT.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (negocioTurno.ValidarTurnoPacienteDuplicado(legajoPaciente, fechaTurno))
                {
                    lblResultadoAltaT.Text = "El paciente ya tiene un turno asignado ese día.";
                    lblResultadoAltaT.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                bool exito = negocioTurno.AgendarTurno(legajoMedico, legajoPaciente, descripcion, fechaYHoraTurno);

                if (exito)
                {
                    lblResultadoAltaT.Text = "Turno agendado correctamente.";
                    lblResultadoAltaT.ForeColor = System.Drawing.Color.Green;
                    LimpiarCampos();
                }
                else
                {
                    lblResultadoAltaT.Text = "No se pudo agendar el turno.";
                    lblResultadoAltaT.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResultadoAltaT.Text = "Error al agendar el turno: " + ex.Message;
                lblResultadoAltaT.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void LimpiarCampos()
        {
            ddlEspecialidad.Items[0].Enabled = true;
            ddlEspecialidad.SelectedIndex = 0;
            ddlMedico.Items.Clear();
            txtDniPaciente.Text = string.Empty;
            ddlSemana.Items.Clear();
            ddlDia.Items.Clear();
            ddlHorario.Items.Clear();
            lblInfoTurnos.Text = string.Empty;
        }
    }
}