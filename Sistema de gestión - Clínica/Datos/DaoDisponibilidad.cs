using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Net.NetworkInformation;

namespace Datos
{
    public class DaoDisponibilidad
    {
        private AccesoDatos datos;
        private const string consultaBase = @"SELECT E.Descripcion_ES AS 'Especialidad', 
            M.Apellido_ME + ' ' + 
            M.Nombre_ME AS 'Medico', 
            DIA.Descripcion_DI AS 'Dia', 
            CONVERT(VARCHAR(5), HorarioInicio_DIS, 108) + ' - ' + CONVERT(VARCHAR(5), 
            HorarioFin_DIS, 108) AS 'Horario',
            Estado_DIS AS 'Estado'
            FROM Disponibilidad DIS 
            INNER JOIN Dia DIA ON DIA.NumDia_DI = DIS.NumDia_DIS 
            INNER JOIN Medico M ON M.Legajo_ME = DIS.LegajoMedico_DIS 
            INNER JOIN Especialidad E ON E.CodEspecialidad_ES = M.CodigoEspecialidad_ME";

        private const string ordenamiento = " ORDER BY [Especialidad] ASC";

        private const string consultaModificacionDisponibilidad = @"SELECT Descripcion_DI AS Dia, Nombre_ME + ' ' + Apellido_ME AS Nombre, 
                                LegajoMedico_DIS AS Legajo, HorarioInicio_DIS AS Inicio, HorarioFin_DIS AS Fin, Estado_DIS AS Estado FROM Disponibilidad 
                                INNER JOIN Medico ON LegajoMedico_DIS = Legajo_ME
                                INNER JOIN Dia ON NumDia_DIS = NumDia_DI";

        private const string ordenamientoPorDia = " ORDER BY NumDia_DI";

        public DaoDisponibilidad()
        {
            datos = new AccesoDatos();
        }

        public DataTable ObtenerTablaDisponibilidad()
        {
            return datos.ObtenerTabla("Disponibilidad", consultaBase + " AND Estado_DIS = 1 " + ordenamiento);
        }

        public List<Disponibilidad> ObtenerListaDisponibilidadMedico(int legajoMedico)
        {
            List<Disponibilidad> listaDisponibilidadMedico = new List<Disponibilidad>();

            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataReader lector;

            try
            {
                string consulta = "SELECT NumDia_DIS, LegajoMedico_DIS, HorarioInicio_DIS, HorarioFin_DIS, Estado_DIS, Descripcion_DI FROM Disponibilidad INNER JOIN Dia ON NumDia_DI = NumDia_DIS WHERE @LegajoMedico = LegajoMedico_DIS ORDER BY NumDia_DIS ASC";
                conexion = datos.ObtenerConexion();
                comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@LegajoMedico", legajoMedico);
                lector = comando.ExecuteReader();

                using (lector)
                {
                    while (lector.Read())
                    {
                        if ((bool)lector["Estado_DIS"])
                        {
                            Disponibilidad disponibilidad = new Disponibilidad();
                            disponibilidad.NumDia = (int)lector["NumDia_DIS"];
                            disponibilidad.NombreDia = (string)lector["Descripcion_DI"];
                            disponibilidad.LegajoMedico = (int)lector["LegajoMedico_DIS"];
                            disponibilidad.HorarioInicio = (TimeSpan)lector["HorarioInicio_DIS"];
                            disponibilidad.HorarioFin = (TimeSpan)lector["HorarioFin_DIS"];

                            listaDisponibilidadMedico.Add(disponibilidad);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return listaDisponibilidadMedico;
        }

        public DataTable ObtenerTablaDisponibilidadParaModificacion()
        {
            return datos.ObtenerTabla("Disponibilidad", consultaModificacionDisponibilidad + ordenamientoPorDia);
        }

        //PRUEBA
        public DataTable ObtenerTablaDisponibilidad(int codEspecialidad = 0, int diaSeleccionado = 0, int legajoMedico = 0)
        {
            DataTable tablaDisponibilidad = new DataTable();

            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = consultaBase;
                List<string> condiciones = new List<string>();

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;

                if (codEspecialidad > 0)
                {
                    condiciones.Add("CodEspecialidad_ES = @CodEspecialidad");
                    comando.Parameters.AddWithValue("@CodEspecialidad", codEspecialidad);
                }

                if (diaSeleccionado > 0)
                {
                    condiciones.Add("NumDia_Dis = @NumDia");
                    comando.Parameters.AddWithValue("@NumDia", diaSeleccionado);
                }

                if (legajoMedico > 0)
                {
                    condiciones.Add("LegajoMedico_DIS = @LegajoMedico");
                    comando.Parameters.AddWithValue("@LegajoMedico", legajoMedico);
                }

                if (condiciones.Count > 0)
                {
                    //string.Join() concatena elementos de una lista, y entre medio, agrega en este caso "AND"
                    consulta += " WHERE " + string.Join(" AND ", condiciones) + " AND Estado_DIS = 1 " ;
                }
                else
                {
                    consulta += " WHERE Estado_DIS = 1 ";
                }

                consulta += ordenamiento;

                comando.CommandText = consulta;

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    tablaDisponibilidad.Load(reader);
                }
            }

            return tablaDisponibilidad;
        }

        public DataTable ObtenerTablaDisponibilidadFiltroAvanzado(string cadena)
        {
            DataTable tablaDisponibilidadFiltrada = new DataTable();            

            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consultaFiltroAvanzado = consultaBase + cadena + " AND Estado_DIS = 1 " + ordenamiento;
                
                using(SqlCommand comando = new SqlCommand(consultaFiltroAvanzado, conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        tablaDisponibilidadFiltrada.Load(lector);
                    }
                }

            }

            return tablaDisponibilidadFiltrada;
        }

        public DataTable Obtener_Disponibilidad(Disponibilidad disponibilidad)
        {
            DataTable tablaDisponibilidad = new DataTable();
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "SELECT NumDia_DIS AS 'Numero Día Semana', LegajoMedico_DIS AS 'Legajo Medico', HorarioInicio_DIS AS 'Horario de Inicio', HorarioFin_DIS AS 'Horario de Finalizacion', Descripcion_DI AS 'Día Semana' FROM Disponibilidad INNER JOIN Dia ON NumDia_DI = NumDia_DIS WHERE LegajoMedico_DIS = @Legajo AND Estado_DIS = 1";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Legajo", disponibilidad.LegajoMedico);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        tablaDisponibilidad.Load(reader);
                    }
                }
            }
            return tablaDisponibilidad;
        }

        public int BajaLogicaDisponibilidad(Disponibilidad disponibilidad)
        {
            // Usar variables distintas para las consultas mejora la legibilidad.
            string consultaTurnos = @" SET DATEFIRST 1 UPDATE Turno SET Estado_TU = 0 WHERE LegajoMedico_TU = @LegajoMedico AND DATEPART(weekday, Fecha_TU) = @NumDia AND Fecha_TU >= GETDATE() AND Estado_TU = 1;";

            string consultaDisponibilidad = @" UPDATE Disponibilidad SET Estado_DIS = 0 WHERE LegajoMedico_DIS = @LegajoMedico  AND NumDia_DIS = @NumDia AND Estado_DIS = 1;";

            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // Comando para actualizar los turnos
                        using (SqlCommand comandoTurnos = new SqlCommand(consultaTurnos, conexion, transaccion))
                        {
                            comandoTurnos.Parameters.AddWithValue("@LegajoMedico", disponibilidad.LegajoMedico);
                            comandoTurnos.Parameters.AddWithValue("@NumDia", disponibilidad.NumDia);
                            comandoTurnos.ExecuteNonQuery();
                        }

                        // Comando para actualizar la disponibilidad
                        int filasAfectadas;
                        using (SqlCommand comandoDisponibilidad = new SqlCommand(consultaDisponibilidad, conexion, transaccion))
                        {
                            comandoDisponibilidad.Parameters.AddWithValue("@LegajoMedico", disponibilidad.LegajoMedico);
                            comandoDisponibilidad.Parameters.AddWithValue("@NumDia", disponibilidad.NumDia);
                            filasAfectadas = comandoDisponibilidad.ExecuteNonQuery();
                        }

                        // Si todo fue exitoso, confirma la transacción.
                        transaccion.Commit();
                        return filasAfectadas;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        Console.WriteLine("Error al realizar la baja lógica de disponibilidad: " + ex.Message);

                        return -1;
                    }
                }
            }
        }

        public DataTable ObtenerDias()
        {
            DataTable tablaDias = new DataTable();
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "SELECT NumDia_DI, Descripcion_DI FROM Dia";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    using (SqlDataReader reader = comando.ExecuteReader()) //recorre fila a fila el resultado de la columna
                    {
                        tablaDias.Load(reader);
                    }
                }
            }
            return tablaDias;
        }

        public int AgregarDisponibilidad(Disponibilidad disponibilidad)
        {
            try
            {
                using (SqlConnection conexion = datos.ObtenerConexion())
                {
                    // Verificar si existe dado de baja
                    string consultaVerificar = @"SELECT COUNT(*) FROM Disponibilidad 
                        WHERE LegajoMedico_DIS = @LegajoMedico 
                        AND NumDia_DIS = @NumDia 
                        AND Estado_DIS = 0";

                    using (SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexion))
                    {
                        comandoVerificar.Parameters.AddWithValue("@LegajoMedico", disponibilidad.LegajoMedico);
                        comandoVerificar.Parameters.AddWithValue("@NumDia", disponibilidad.NumDia);

                        int existe = (int)comandoVerificar.ExecuteScalar();

                        if (existe > 0)
                        {
                            // Reactivar el registro dado de baja
                            string consultaReactivar = @"UPDATE Disponibilidad 
                                SET Estado_DIS = 1,
                                    HorarioInicio_DIS = @HorarioInicio,
                                    HorarioFin_DIS = @HorarioFin
                                WHERE LegajoMedico_DIS = @LegajoMedico 
                                AND NumDia_DIS = @NumDia 
                                AND Estado_DIS = 0";

                            using (SqlCommand comandoReactivar = new SqlCommand(consultaReactivar, conexion))
                            {
                                comandoReactivar.Parameters.AddWithValue("@LegajoMedico", disponibilidad.LegajoMedico);
                                comandoReactivar.Parameters.AddWithValue("@NumDia", disponibilidad.NumDia);
                                comandoReactivar.Parameters.AddWithValue("@HorarioInicio", disponibilidad.HorarioInicio);
                                comandoReactivar.Parameters.AddWithValue("@HorarioFin", disponibilidad.HorarioFin);

                                return comandoReactivar.ExecuteNonQuery(); // Devuelve 1 si se reactivó
                            }
                        }
                        else
                        {
                            // Insertar nuevo registro
                            string consultaInsertar = @"INSERT INTO Disponibilidad 
                                (NumDia_DIS, LegajoMedico_DIS, HorarioInicio_DIS, HorarioFin_DIS, Estado_DIS) 
                                VALUES (@NumDia, @LegajoMedico, @HorarioInicio, @HorarioFin, @Estado)";

                            using (SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexion))
                            {
                                comandoInsertar.Parameters.AddWithValue("@NumDia", disponibilidad.NumDia);
                                comandoInsertar.Parameters.AddWithValue("@LegajoMedico", disponibilidad.LegajoMedico);
                                comandoInsertar.Parameters.AddWithValue("@HorarioInicio", disponibilidad.HorarioInicio);
                                comandoInsertar.Parameters.AddWithValue("@HorarioFin", disponibilidad.HorarioFin);
                                comandoInsertar.Parameters.AddWithValue("@Estado", disponibilidad.Estado);

                                return comandoInsertar.ExecuteNonQuery(); // Devuelve 1 si se insertó
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en AgregarDisponibilidad: " + ex.Message);
            }
        }

        public bool ExisteDisponibilidad(int legajoMedico, int numDia)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = @"SELECT COUNT(*) FROM Disponibilidad 
                    WHERE LegajoMedico_DIS = @LegajoMedico 
                    AND NumDia_DIS = @NumDia 
                    AND Estado_DIS = 1";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@LegajoMedico", legajoMedico);
                    comando.Parameters.AddWithValue("@NumDia", numDia);

                    int count = (int)comando.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    
        //Valido si el procedimiento ya esta creado
        public void ValidarOCrearProcedimiento_ModificacionDisponibilidad()
        {
            //Conecto a la bd
            SqlConnection conexion = datos.ObtenerConexion();
            using (conexion)
            {
                //Intento
                try
                {
                    // Verificar si existe el procedimiento
                    string consultaExiste = @"
                    SELECT COUNT(*) 
                    FROM sys.objects 
                    WHERE type = 'P' AND name = 'SP_ModificacionDisponibilidad'";
                    SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                    int cantidad = (int)cmdExiste.ExecuteScalar();

                    //Si no existe
                    if (cantidad == 0)
                    {
                        // Crear el procedimiento si no existe
                        string crearProcedimiento = @"
                        CREATE PROCEDURE SP_ModificacionDisponibilidad
                            @NumDia_DIS INT,  
                            @LegajoMedico_DIS INT,           
                            @HorarioInicio_DIS TIME,
                            @HorarioFin_DIS TIME,
                            @Estado_DIS BIT
                        AS
                        BEGIN
                            UPDATE Disponibilidad
                            SET 
                                NumDia_DIS = @NumDia_DIS,
                                LegajoMedico_DIS = @LegajoMedico_DIS,    
                                HorarioInicio_DIS = @HorarioInicio_DIS, 
                                HorarioFin_DIS = @HorarioFin_DIS, 
                                Estado_DIS = @Estado_DIS 
                            WHERE NumDia_DIS = @NumDia_DIS AND LegajoMedico_DIS = @LegajoMedico_DIS;                                                        
                        END";
                        SqlCommand cmdCrear = new SqlCommand(crearProcedimiento, conexion);
                        cmdCrear.ExecuteNonQuery();
                    }
                } //SI no puedo
                catch (Exception ex)
                {
                    throw new Exception("Error al crear el procedimiento almacenado: " + ex.Message);
                }
            }
        }

        //Cargar los parametros
        private SqlCommand CargarParametros_ModificacionDsiponibilidad(Disponibilidad disp)
        {
            SqlCommand sqlComand = new SqlCommand("SP_ModificacionDisponibilidad");
            sqlComand.CommandType = CommandType.StoredProcedure;
            sqlComand.Parameters.AddWithValue("@NumDia_DIS", disp.NumDia);
            sqlComand.Parameters.AddWithValue("@LegajoMedico_DIS", disp.LegajoMedico);
            sqlComand.Parameters.AddWithValue("@HorarioInicio_DIS", disp.HorarioInicio);
            sqlComand.Parameters.AddWithValue("@HorarioFin_DIS", disp.HorarioFin);
            sqlComand.Parameters.AddWithValue("@Estado_DIS", disp.Estado);
            return sqlComand;
        }

        public int ModificarDisponibilidad(Disponibilidad disp)
        {
            ValidarOCrearProcedimiento_ModificacionDisponibilidad();
            SqlCommand sqlComand = CargarParametros_ModificacionDsiponibilidad(disp);
            return datos.EjecutarProcedimientoAlmacenado("SP_ModificacionDisponibilidad", sqlComand);
        }
    }
}
