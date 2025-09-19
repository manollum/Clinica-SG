using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Datos
{
    public class DaoTurno
    {
        private const string ConsultaBase = "SELECT CodTurno_TU AS [ID Consulta], Nombre_ME + ' ' + Apellido_ME AS Medico, " +
            "LegajoMedico_TU AS [Legajo Medico], Nombre_PA + ' ' + Apellido_PA AS [Paciente], FORMAT(Fecha_TU, 'dd/MM/yyyy HH:mm') AS Turno, Pendiente_TU AS Pendiente," +
            "Asistencia_TU AS Asistencia, Descripcion_TU AS Descripcion, Estado_TU AS Estado FROM Turno " +
            "INNER JOIN Medico ON LegajoMedico_TU = Legajo_ME " +
            "INNER JOIN Paciente ON LegajoPaciente_TU = Legajo_PA WHERE Estado_TU = 1";

        private readonly AccesoDatos datos;

        public DaoTurno()
        {
            datos = new AccesoDatos();
        }

        private void ValidarOCrearProcedimientoAgendarTurno()
        {
            SqlConnection conexion = datos.ObtenerConexion();
            using (conexion)
            {
                // Verificar si existe el procedimiento
                string consultaExiste = @"
                    SELECT COUNT(*) 
                    FROM sys.objects 
                    WHERE type = 'P' AND name = 'SP_AgendarTurno'";

                SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                int cantidad = (int)cmdExiste.ExecuteScalar();

                if (cantidad == 0)
                {
                    // Crear el procedimiento si no existe
                    string crearProc = @"
                        CREATE PROCEDURE SP_AgendarTurno
                            @LegajoMedico INT,
                            @LegajoPaciente INT,
                            @Descripcion VARCHAR (300),
                            @Fecha DATETIME
                        AS
                        BEGIN
                            INSERT INTO Turno 
                            (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, 
                            Asistencia_TU, Descripcion_TU, Estado_TU) 
                            VALUES 
                            (@LegajoMedico, @LegajoPaciente, @Fecha, 1, NULL, @Descripcion, 1)                                                        
                        END";

                    SqlCommand cmdCrear = new SqlCommand(crearProc, conexion);
                    cmdCrear.ExecuteNonQuery();
                }
            }
        }

        public List<Turno> ObtenerListaTurnos(int legajoMedico)
        {
            List<Turno> listaTurnosMedico = new List<Turno>();

            try
            {                                                
                using (SqlConnection conexion = datos.ObtenerConexion())
                {
                    string consulta = "SELECT Fecha_TU, Estado_TU FROM Turno WHERE LegajoMedico_TU = @LegajoMedico AND Fecha_TU > GETDATE() AND Fecha_TU <= DATEADD(Day, 30, GETDATE())";

                    using(SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("LegajoMedico", legajoMedico);
                        SqlDataReader lector = comando.ExecuteReader();

                        using (lector)
                        {
                            while (lector.Read())
                            {
                                if ((bool)lector["Estado_TU"])
                                {
                                    Turno turno = new Turno();
                                    turno.Fecha = (DateTime)lector["Fecha_TU"];
                                    turno.Estado = (bool)lector["Estado_TU"];

                                    listaTurnosMedico.Add(turno);
                                }
                            }
                        }
                    }
                }               
            }
            catch (Exception)
            {
                throw;
            }

            return listaTurnosMedico;
        }

        //Consigue la tabla
        public DataTable getTabla()
        {
            DataTable table = datos.ObtenerTabla("Turnos", ConsultaBase);
            return table;
        }

        //Consigue la tabla por código de turno
        public DataTable getTablaPorCodigoTurno(int numero)
        {
            string consulta = ConsultaBase + " AND CodTurno_TU = @CodTurno_TU";

            //Creo el sqlCommand y lo cargo
            SqlCommand sqlComand = new SqlCommand(consulta);
            sqlComand.Parameters.AddWithValue("@CodTurno_TU", numero);

            //Devuelvo
            return datos.ObtenerTablaFiltrada("Turnos", sqlComand);
        }

        //Consige la consulta de la tabla filtrada
        private SqlCommand ObtenerConsultaFiltrada(int caso, string dni, DateTime? fecha)
        {
            //Variable
            string consulta;

            //Pregunto que caso es
            if (caso == 1)
            {
                //Establezco la consulta
                consulta = ConsultaBase + " AND DNI_ME = @DNI_ME AND Fecha_TU = @Fecha_TU";

                //Creo el sqlCommand y lo cargo
                SqlCommand sqlComand = new SqlCommand(consulta);
                sqlComand.Parameters.AddWithValue("@DNI_ME", Convert.ToInt32(dni));
                sqlComand.Parameters.AddWithValue("@Fecha_TU", fecha);

                //Devuelvo el sqlCommand
                return sqlComand;
            }
            else if (caso == 2)
            {
                //Establezco la consulta
                consulta = ConsultaBase + " AND DNI_PA = @DNI_PA AND Fecha_TU = @Fecha_TU";

                //Creo el sqlCommand y lo cargo
                SqlCommand sqlComand = new SqlCommand(consulta);
                sqlComand.Parameters.AddWithValue("@DNI_PA", Convert.ToInt32(dni));
                sqlComand.Parameters.AddWithValue("@Fecha_TU", fecha);

                //Devuelvo el sqlCommand
                return sqlComand;
            }
            else if (caso == 3)
            {
                //Establezco la consulta
                consulta = ConsultaBase + " AND DNI_ME = @DNI_ME";

                //Creo el sqlCommand y lo cargo
                SqlCommand sqlComand = new SqlCommand(consulta);
                sqlComand.Parameters.AddWithValue("@DNI_ME", Convert.ToInt32(dni));

                //Devuelvo el sqlCommand
                return sqlComand;
            }
            else if (caso == 4)
            {

                //Establezco la consulta
                consulta = ConsultaBase + " AND DNI_PA = @DNI_PA";

                //Creo el sqlCommand y lo cargo
                SqlCommand sqlComand = new SqlCommand(consulta);
                sqlComand.Parameters.AddWithValue("@DNI_PA", Convert.ToInt32(dni));

                //Devuelvo el sqlCommand
                return sqlComand;
            }
            else if (caso == 5)
            {
                //Establezco la consulta
                consulta = ConsultaBase + " AND Fecha_TU = @Fecha_TU";

                //Creo el sqlCommand y lo cargo
                SqlCommand sqlComand = new SqlCommand(consulta);
                sqlComand.Parameters.AddWithValue("@Fecha_TU", fecha);

                //Devuelvo el sqlCommand
                return sqlComand;
            }
            else
            {
                return null;
            }
        }

        //Consigue la tabla filtrada
        public DataTable getTablaFiltrada(int caso, string dni, DateTime? fecha)
        {
            SqlCommand sql = ObtenerConsultaFiltrada(caso, dni, fecha);

            return datos.ObtenerTablaFiltrada("Turnos", sql);
        }

        public DataTable getTurnosXMedico(int legajoMedico)
        {
            string consultaTurnosM = ConsultaBase + " AND LegajoMedico_TU = @LegajoMedico_TU ORDER BY Fecha_TU";

            SqlCommand sqlComand = new SqlCommand(consultaTurnosM);
            sqlComand.Parameters.AddWithValue("@LegajoMedico_TU", legajoMedico);
            return datos.ObtenerTablaFiltrada("Turnos", sqlComand);
        }

        public bool EjecutarAgendarTurno(int legajoMedico, int legajoPaciente, string descripcion, DateTime fecha)
        {
            ValidarOCrearProcedimientoAgendarTurno();

            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SP_AgendarTurno", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@LegajoMedico", legajoMedico);
                comando.Parameters.AddWithValue("@LegajoPaciente", legajoPaciente);
                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.Parameters.AddWithValue("@Fecha", fecha);

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        private void ValidarOCrearProcedimiento_ModificacionTurno()
        {
            SqlConnection conexion = datos.ObtenerConexion();
            using (conexion)
            {
                try
                {
                    // Verificar si existe el procedimiento
                    string consultaExiste = @"
                        SELECT COUNT(*) 
                        FROM sys.objects 
                        WHERE type = 'P' AND name = 'SP_ModificacionTurno_Grupo10'";
                    
                    SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                    int cantidad = (int)cmdExiste.ExecuteScalar();
                    
                    if (cantidad == 0)
                    {
                        // Crear el procedimiento si no existe
                        string crearProcedimiento = @"
                            CREATE PROCEDURE SP_ModificacionTurno_Grupo10 
                                @Fecha_TU DateTime,           
                                @CodTurno_TU INT,
                                @Pendiente_TU BIT,
                                @Asistencia_TU BIT,
                                @Descripcion_TU NVARCHAR(300),
                                @Estado_TU BIT
                            AS
                            BEGIN
                                UPDATE Turno 
                                SET 
                                    Fecha_TU = @Fecha_TU,    
                                    Pendiente_TU = @Pendiente_TU,
                                    Asistencia_TU = @Asistencia_TU, 
                                    Descripcion_TU = @Descripcion_TU, 
                                    Estado_TU = @Estado_TU 
                                WHERE CodTurno_TU = @CodTurno_TU;                                                        
                            END";
                        SqlCommand cmdCrear = new SqlCommand(crearProcedimiento, conexion);
                        cmdCrear.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear el procedimiento almacenado: " + ex.Message);
                }
            }
        }

        private SqlCommand CargarParametros_ModificacionTurno(Turno turno)
        {
            SqlCommand sqlComand = new SqlCommand("SP_ModificacionTurno_Grupo10");
            sqlComand.CommandType = CommandType.StoredProcedure;

            sqlComand.Parameters.AddWithValue("@Fecha_TU", turno.Fecha);
            sqlComand.Parameters.AddWithValue("@CodTurno_TU", turno.CodTurno);
            sqlComand.Parameters.AddWithValue("@Pendiente_TU", turno.Pendiente);
            sqlComand.Parameters.AddWithValue("@Asistencia_TU", string.IsNullOrEmpty(turno.Asistencia) ? (object)DBNull.Value : Convert.ToInt32(turno.Asistencia));
            sqlComand.Parameters.AddWithValue("@Descripcion_TU", turno.Descripcion);
            sqlComand.Parameters.AddWithValue("@Estado_TU", turno.Estado ? 1 : 0);

            return sqlComand;
        }

        public int ModificarTurno(Turno turno)
        {
            ValidarOCrearProcedimiento_ModificacionTurno();
            SqlCommand sqlComand = CargarParametros_ModificacionTurno(turno);
            return datos.EjecutarProcedimientoAlmacenado("SP_ModificacionTurno_Grupo10 ", sqlComand);
        }

        public int BajaLogicaTurno(int codTurno)
        {
            string consulta = "UPDATE Turno SET Estado_TU = 0 WHERE CodTurno_TU = @CodTurno_TU AND Estado_TU = 1";

            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CodTurno_TU", codTurno);
                    return comando.ExecuteNonQuery(); // Devuelve 1 si se actualizó
                }
            }
        }

        public int ObtenerLegajoPacientePorDNI(string dni)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "SELECT Legajo_PA FROM Paciente WHERE DNI_PA = @DNI";
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@DNI", dni);

                object result = cmd.ExecuteScalar(); //ExecuteScalar() devuelve un solo valor -primera celda encontrada con 'x' valor-. Devuelve un "object"
                
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    throw new Exception("No se encontró un paciente con ese DNI.");
            }
        }

        public bool ExisteTurnoMedicoFecha(int legajoMedico, DateTime fecha)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "SELECT COUNT(*) FROM Turno WHERE LegajoMedico_TU = @LegajoMedico AND Fecha_TU = @Fecha AND Estado_TU = 1";

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@LegajoMedico", legajoMedico);
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }

        public bool PacienteYaTieneTurnoElDia(int legajoPaciente, DateTime fecha)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = @"
                    SELECT COUNT(*) 
                    FROM Turno 
                    WHERE LegajoPaciente_TU = @LegajoPaciente 
                      AND CONVERT(date, Fecha_TU) = @Fecha 
                      AND Estado_TU = 1";

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@LegajoPaciente", legajoPaciente);
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date); // Solo fecha, sin hora

                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }

        public DataTable ObtenerPorcentajeTurnosPorDia()
        {
            string consulta = @"
                SET DATEFIRST 1;
                SET LANGUAGE Spanish;

                WITH DiasSemana AS (
                    SELECT 1 AS NumeroDia_DS, 'Lunes' AS NombreDia_DS UNION ALL
                    SELECT 2, 'Martes' UNION ALL
                    SELECT 3, 'Miércoles' UNION ALL
                    SELECT 4, 'Jueves' UNION ALL
                    SELECT 5, 'Viernes' UNION ALL
                    SELECT 6, 'Sábado' UNION ALL
                    SELECT 7, 'Domingo'
                )

                SELECT 
                    NombreDia_DS AS [Día de la Semana],
                    ISNULL(COUNT(Fecha_TU), 0) AS [Turnos Asignados],
                    ISNULL(SUM(CASE WHEN Asistencia_TU = 1 THEN 1 ELSE 0 END), 0) AS [Turnos Asistidos],
                    CASE 
                        WHEN COUNT(Fecha_TU) = 0 THEN '0.00 %'
                        ELSE CONCAT(FORMAT(100.0 * SUM(CASE WHEN Asistencia_TU = 1 THEN 1 ELSE 0 END) / COUNT(Fecha_TU), 'N2'), ' %')
                    END AS [Porcentaje de Asistencia]
                FROM DiasSemana LEFT JOIN Turno
                ON DATEPART(weekday, Fecha_TU) = NumeroDia_DS AND Estado_TU = 1 AND Pendiente_TU = 0
                GROUP BY NumeroDia_DS, NombreDia_DS
                ORDER BY NumeroDia_DS";

            return datos.ObtenerTabla("ConcurrenciaTurnosPorDia", consulta);
        }

        //Obtener horario mas solicitado
        public DataTable ObtenerTablaHorarioMasSolicitado()
        {
            /*
            El "WITH" es una forma de "crear una tabla" en la que selecciono x campo (ej: turnosRedondeados) y 
            le pongo para que muestre AS y le establezco que vale lo mismo que otra consulta.

            Los agrupo por la hora con el GROUP BY

            Ya que es un DATE TIME tengo que separar la parte de date de la de time con el DATEPART

            El COUNT(*) cuenta la cantidad de registros de esta tabla

            Uso el cast para convertir el time en texto y asi sumarle lo que le quiera sumar

            Format redondea el numero

            Uso el CROSS JOIN para calcular el procentaje
            */

            string consulta =
            "WITH TurnosRedondeados AS (" +
            " SELECT DATEPART(HOUR, Fecha_TU) AS Hora, COUNT(*) AS Cantidad " +
            " FROM Turno " +
            " GROUP BY DATEPART(HOUR, Fecha_TU) " +
            "), " +
            "TotalTurnos AS (" +
            " SELECT COUNT(*) AS Total FROM Turno " +
            ") " +
            "SELECT " +
            " CAST(t.Hora AS VARCHAR) + ':00 hs' AS Horario, " +
            " t.Cantidad AS [Cantidad turnos], " +
            " FORMAT(t.Cantidad * 100.0 / tt.Total, 'N2') + '%' AS Porcentaje " +
            "FROM TurnosRedondeados t " +
            "CROSS JOIN TotalTurnos tt;";

            return datos.ObtenerTabla("ConcurrenciaTurnosPorDia", consulta);
        }
    }
}
