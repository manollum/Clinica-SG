using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DaoPaciente
    {
        private readonly AccesoDatos datos;
        private SqlCommand sqlCommand;


        private const string consultaBaseSQL = "SELECT P.Legajo_PA AS 'Legajo', P.DNI_PA AS 'DNI', P.Apellido_PA AS 'Apellido', P.Nombre_PA AS 'Nombre', P.Sexo_PA AS 'Sexo', P.Nacionalidad_PA AS 'Nacionalidad', FORMAT(P.FechaNacimiento_PA, 'dd/MM/yyyy') AS [Fecha de Nacimiento], P.Direccion_PA AS 'Direccion', P.Localidad_PA AS 'Localidad', Pr.Descripcion_Pr AS 'Provincia', Pr.CodProvincia_Pr AS 'CodProvincia', P.Correo_PA AS 'Correo', P.Telefono_PA AS 'Telefono' FROM Paciente AS P INNER JOIN Provincia AS Pr ON P.CodProvincia_PA = Pr.CodProvincia_Pr";        
        
        public DaoPaciente()
        {
            datos = new AccesoDatos();
        }

        //------------------------------------------------------ Alta Paciente ------------------------------------------------------
       
        private void ValidarOCrearProcedimientoAltaPaciente()
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consultaExiste = @"
                    SELECT COUNT(*) 
                    FROM sys.objects 
                    WHERE type = 'P' AND name = 'AltaPaciente_Grupo10'";
                SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                int cantidad = (int)cmdExiste.ExecuteScalar();
                if (cantidad == 0)
                {
                    string crearProc = @"
                        CREATE PROCEDURE AltaPaciente_Grupo10
                            @DNI_PA CHAR(8),
                            @Nombre_PA NVARCHAR(50),
                            @Apellido_PA NVARCHAR(50),
                            @Sexo_PA CHAR(1),
                            @Nacionalidad_PA NVARCHAR(50),
                            @FechaNacimiento_PA DATE,
                            @Direccion_PA NVARCHAR(100),
                            @Localidad_PA NVARCHAR(50),
                            @CodProvincia_PA INT,
                            @Correo_PA NVARCHAR(100),
                            @Telefono_PA NVARCHAR(15),
                            @Estado_PA BIT

                        AS
                        BEGIN
                            INSERT INTO Paciente (DNI_PA, Nombre_PA, Apellido_PA, Sexo_PA, Nacionalidad_PA, FechaNacimiento_PA, Direccion_PA, Localidad_PA, CodProvincia_PA, Correo_PA, Telefono_PA, Estado_PA)
                            VALUES (@DNI_PA, @Nombre_PA, @Apellido_PA, @Sexo_PA, @Nacionalidad_PA, @FechaNacimiento_PA, @Direccion_PA, @Localidad_PA, @CodProvincia_PA, @Correo_PA, @Telefono_PA, @Estado_PA);
                        END";
                    SqlCommand cmdCrear = new SqlCommand(crearProc, conexion);
                    cmdCrear.ExecuteNonQuery();
                }
            }
        }

        private void ArmarParametro_Alta_Paciente(ref SqlCommand command, Paciente paciente)
        {
            command.Parameters.AddWithValue("@DNI_PA", paciente.Dni);
            command.Parameters.AddWithValue("@Nombre_PA", paciente.Nombre);
            command.Parameters.AddWithValue("@Apellido_PA", paciente.Apellido);
            command.Parameters.AddWithValue("@Sexo_PA", paciente.Sexo);
            command.Parameters.AddWithValue("@Nacionalidad_PA", paciente.Nacionalidad);
            command.Parameters.AddWithValue("@FechaNacimiento_PA", paciente.FechaNacimiento);
            command.Parameters.AddWithValue("@Direccion_PA", paciente.Direccion);
            command.Parameters.AddWithValue("@Localidad_PA", paciente.Localidad);
            command.Parameters.AddWithValue("@CodProvincia_PA", paciente.CodProvincia);
            command.Parameters.AddWithValue("@Correo_PA", paciente.CorreoElectronico);
            command.Parameters.AddWithValue("@Telefono_PA", paciente.Telefono);
            command.Parameters.AddWithValue("@Estado_PA", true);
        }

        public int AltaPaciente(Paciente paciente)
        {
            sqlCommand = new SqlCommand();
            ArmarParametro_Alta_Paciente(ref sqlCommand, paciente);
            ValidarOCrearProcedimientoAltaPaciente();
            int resultado = datos.EjecutarProcedimientoAlmacenado("AltaPaciente_Grupo10", sqlCommand);
            return resultado;
        }

        private void ArmarParametro_DNIPaciente(ref SqlCommand command, Paciente paciente)
        {
            SqlParameter parametro = new SqlParameter();
            parametro = command.Parameters.Add("@DNI_PA", SqlDbType.Int);
            parametro.Value = paciente.Dni;
        }

        public bool VerificarExistenciaPacienteXDNI(Paciente paciente)
        {
            string consultaVerificacion = "SELECT * FROM Paciente WHERE DNI_PA = @DNI_PA";
            bool existe = false;
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                sqlCommand = new SqlCommand(consultaVerificacion, conexion);
                ArmarParametro_DNIPaciente(ref sqlCommand, paciente);
                SqlDataReader lector = sqlCommand.ExecuteReader();

                if (lector.Read())
                {
                    existe = true;
                }
            }

            return existe;
        }

        public DataTable getProvincias()
        {
            return  datos.ObtenerTabla("Provincias", "SELECT CodProvincia_PR, Descripcion_PR FROM Provincia ORDER BY Descripcion_PR");
        }

        // -----------------------------------------------------------------------------------------------------------------------------

        public int BajaLogicaPacientePorLegajo(string legajo)
        {
            try
            {
                using (SqlConnection conexion = datos.ObtenerConexion())
                {
                    string consultaTurnos = "UPDATE Turno SET Estado_TU = 0 WHERE LegajoPaciente_TU = @LegajoPaciente_TU AND Estado_TU = 1";
                    SqlCommand comandoTurnos = new SqlCommand(consultaTurnos, conexion);
                    comandoTurnos.Parameters.AddWithValue("@LegajoPaciente_TU", legajo);
                    comandoTurnos.ExecuteNonQuery();

                    string consultaPaciente = "UPDATE Paciente SET Estado_PA = 0 WHERE Legajo_PA = @Legajo_PA AND Estado_PA = 1";
                    SqlCommand comandoPaciente = new SqlCommand(consultaPaciente, conexion);
                    comandoPaciente.Parameters.AddWithValue("@Legajo_PA", legajo);
                    int filasAfectadas = comandoPaciente.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
            catch
            {
                return -1;
            }
        }

        public DataTable ObtenerPacientes()
        {
            string consulta = consultaBaseSQL + " WHERE P.Estado_PA = 1 ORDER BY P.Apellido_PA, P.Nombre_PA";
            return datos.ObtenerTabla("Pacientes", consulta);
        }

        private void ArmarParametro_FiltroPaciente(ref SqlCommand command, Paciente paciente)
        {
            if(!string.IsNullOrEmpty(paciente.Dni) && paciente.Dni.Length > 0)
            {
                command.Parameters.AddWithValue("@DNI_PA", paciente.Dni);
            }
            else
            {
                command.Parameters.AddWithValue("@CodProvincia_PA", paciente.CodProvincia);
            }
        }

        public DataTable ObtenerPacientes_Filtrados(Paciente paciente, bool FiltrosAvanzados, bool[,] filtros)
        {
            string consulta =
                "SELECT Legajo_PA AS 'Legajo', Apellido_PA AS 'Apellido', Nombre_PA  AS 'Nombre', Sexo_PA AS 'Sexo', Nacionalidad_PA AS 'Nacionalidad', " +
                "FORMAT(FechaNacimiento_PA, 'dd/MM/yyyy') AS [Fecha Nacimiento], Direccion_PA AS 'Dirección', Localidad_PA AS 'Localidad', " +
                "Descripcion_PR AS 'Provincia', Correo_PA AS 'Correo', Telefono_PA AS 'Teléfono', DNI_PA AS 'DNI' " +
                "FROM Paciente INNER JOIN Provincia ON CodProvincia_PA = CodProvincia_PR " +
                "WHERE Estado_PA = 1";

            if (!FiltrosAvanzados)
            {
                if (!string.IsNullOrEmpty(paciente.Dni) && paciente.Dni.Trim().Length > 0)
                {
                    consulta += " AND DNI_PA = @DNI_PA";
                }
                else if (paciente.CodProvincia > 0)
                {
                    consulta += " AND CodProvincia_PA = @CodProvincia_PA";
                }

                sqlCommand = new SqlCommand(consulta);
                ArmarParametro_FiltroPaciente(ref sqlCommand, paciente);

                return datos.ObtenerTablaFiltrada("Paciente", sqlCommand);
            }
            else
            {
                sqlCommand = GenerarConsultasAvanzada_Pacientes(paciente, filtros, consulta);
                return datos.ObtenerTablaFiltrada("Paciente", sqlCommand);
            }
        }

        private SqlCommand GenerarConsultasAvanzada_Pacientes(Paciente paciente, bool[,] filtros, string consulta)
        {
            sqlCommand = new SqlCommand();

            if (paciente.Dni.Trim().Length > 0)
            {
                if (filtros[0, 0]) // Igual a
                {
                    consulta += " AND DNI_PA = @DNI_PA";
                }
                else if (filtros[0, 1]) // Mayor a
                {
                    consulta += " AND DNI_PA > @DNI_PA";
                }
                else if (filtros[0, 2]) // Menor a
                {
                    consulta += " AND DNI_PA < @DNI_PA";
                }

                sqlCommand.Parameters.Add("@DNI_PA", SqlDbType.NVarChar, 8).Value = paciente.Dni;
            }
            if (paciente.Nombre.Trim().Length > 0)
            {
                if (filtros[1, 0]) // Contiene
                {
                    consulta += " AND Nombre_PA LIKE @Nombre_PA";
                    sqlCommand.Parameters.Add("@Nombre_PA", SqlDbType.NVarChar, 50).Value = "%" + paciente.Nombre + "%";
                }
                else if (filtros[1, 1]) // Empieza con
                {
                    consulta += " AND Nombre_PA LIKE @Nombre_PA";
                    sqlCommand.Parameters.Add("@Nombre_PA", SqlDbType.NVarChar, 50).Value = paciente.Nombre + "%";
                }
                else if (filtros[1, 2]) // Termina con
                {
                    consulta += " AND Nombre_PA LIKE @Nombre_PA";
                    sqlCommand.Parameters.Add("@Nombre_PA", SqlDbType.NVarChar, 50).Value = "%" + paciente.Nombre;
                }
            }
            if (paciente.Telefono.Trim().Length > 0)
            {
                if (filtros[2, 0]) // Contiene
                {
                    consulta += " AND Telefono_PA LIKE @Telefono_PA";
                    sqlCommand.Parameters.Add("@Telefono_PA", SqlDbType.NVarChar, 10).Value = "%" + paciente.Telefono + "%";
                }
                else if (filtros[2, 1]) // Empieza con
                {
                    consulta += " AND Telefono_PA LIKE @Telefono_PA";
                    sqlCommand.Parameters.Add("@Telefono_PA", SqlDbType.NVarChar, 10).Value = paciente.Telefono + "%";
                }
                else if (filtros[2, 2]) // Termina con
                {
                    consulta += " AND Telefono_PA LIKE @Telefono_PA";
                    sqlCommand.Parameters.Add("@Telefono_PA", SqlDbType.NVarChar, 10).Value = "%" + paciente.Telefono;
                }
            }

            sqlCommand.CommandText = consulta;

            return sqlCommand;
        }

        //Modificacion Paciente ------------------------------------------------------------------------
        private void ValidarOCrearProcedimientoModificacionPaciente()
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consultaExiste = "SELECT COUNT(*) FROM sys.objects WHERE type = 'P' AND name = 'ModificarPaciente_Grupo10'";
                SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                if ((int)cmdExiste.ExecuteScalar() == 0)
                {
                    string crearProc = @"
                        CREATE PROCEDURE ModificarPaciente_Grupo10
                            @Legajo_PA INT, @DNI_PA CHAR(8), @Nombre_PA VARCHAR(50), @Apellido_PA VARCHAR(50),
                            @Sexo_PA CHAR(1), @Nacionalidad_PA NVARCHAR(50), @FechaNacimiento_PA DATE,
                            @Direccion_PA NVARCHAR(100), @Localidad_PA NVARCHAR(50), @CodProvincia_PA INT,
                            @Correo_PA VARCHAR(100), @Telefono_PA VARCHAR(16)
                        AS
                        BEGIN
                            UPDATE Paciente SET 
                                DNI_PA = @DNI_PA, Nombre_PA = @Nombre_PA, Apellido_PA = @Apellido_PA, Sexo_PA = @Sexo_PA, 
                                Nacionalidad_PA = @Nacionalidad_PA, FechaNacimiento_PA = @FechaNacimiento_PA, 
                                Direccion_PA = @Direccion_PA, Localidad_PA = @Localidad_PA, 
                                CodProvincia_PA = @CodProvincia_PA, 
                                Correo_PA = @Correo_PA, Telefono_PA = @Telefono_PA
                            WHERE Legajo_PA = @Legajo_PA;
                        END";
                    SqlCommand cmdCrear = new SqlCommand(crearProc, conexion);
                    cmdCrear.ExecuteNonQuery();
                }
            }
        }

        private void CargarParametros(ref SqlCommand cmd, Paciente paciente)
        {
            if (paciente.Legajo != 0)
            {
                cmd.Parameters.Add("@Legajo_PA", SqlDbType.Int).Value = paciente.Legajo;
            }
            else
            {
                cmd.Parameters.Add("@Estado_PA", SqlDbType.Bit).Value = paciente.Estado;
            }
            cmd.Parameters.Add("@DNI_PA", SqlDbType.Char, 8).Value = paciente.Dni;
            cmd.Parameters.Add("@Nombre_PA", SqlDbType.VarChar, 50).Value = paciente.Nombre;
            cmd.Parameters.Add("@Apellido_PA", SqlDbType.VarChar, 50).Value = paciente.Apellido;
            cmd.Parameters.Add("@Sexo_PA", SqlDbType.Char).Value = paciente.Sexo;
            cmd.Parameters.Add("@Nacionalidad_PA", SqlDbType.NVarChar, 50).Value = paciente.Nacionalidad;
            cmd.Parameters.Add("@FechaNacimiento_PA", SqlDbType.Date).Value = paciente.FechaNacimiento;
            cmd.Parameters.Add("@Direccion_PA", SqlDbType.NVarChar, 100).Value = paciente.Direccion;
            cmd.Parameters.Add("@Localidad_PA", SqlDbType.NVarChar, 50).Value = paciente.Localidad;
            cmd.Parameters.Add("@CodProvincia_PA", SqlDbType.Int).Value = paciente.CodProvincia;
            cmd.Parameters.Add("@Correo_PA", SqlDbType.VarChar, 100).Value = paciente.CorreoElectronico;
            cmd.Parameters.Add("@Telefono_PA", SqlDbType.VarChar, 16).Value = paciente.Telefono;
        }

        public bool ExisteDniDuplicado(string dni, string nacionalidad, int legajoActual)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = @"SELECT COUNT(*) 
                            FROM Paciente 
                            WHERE DNI_PA = @Dni 
                              AND Nacionalidad_PA = @Nacionalidad 
                              AND Legajo_PA <> @Legajo 
                              AND Estado_PA = 1"; // Asumiendo que solo importan activos

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@Dni", dni);
                cmd.Parameters.AddWithValue("@Nacionalidad", nacionalidad);
                cmd.Parameters.AddWithValue("@Legajo", legajoActual);

                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }

        public int ModificacionPaciente(Paciente paciente)
        {
            sqlCommand = new SqlCommand();
            CargarParametros(ref sqlCommand, paciente);
            ValidarOCrearProcedimientoModificacionPaciente();
            return datos.EjecutarProcedimientoAlmacenado("ModificarPaciente_Grupo10", sqlCommand);
        }

        public Paciente ObtenerPacienteXCodTurno(int codTurno)
        {
            string consulta = @"SELECT P.Legajo_PA AS 'Legajo',
                              P.Nombre_PA AS 'Nombre',
                              P.Apellido_PA as 'Apellido',
                              P.DNI_PA AS 'DNI', 
                              P.Sexo_PA AS 'Sexo', 
                              P.Nacionalidad_PA AS 'Nacionalidad', 
                              P.FechaNacimiento_PA AS 'Fecha nacimiento',
                              P.Direccion_PA AS 'Direccion',                                
                              PRO.Descripcion_PR AS 'Provincia',
                              P.Correo_PA AS 'Correo', 
                              P.Telefono_PA AS 'Telefono' 
                              FROM Paciente P 
                              INNER JOIN Turno T ON T.LegajoPaciente_TU = P.Legajo_PA 
                              INNER JOIN Provincia PRO ON PRO.CodProvincia_PR = P.CodProvincia_PA 
                              WHERE T.CodTurno_TU = @CodTurno";

            Paciente paciente = new Paciente();

            using(SqlConnection conexion = datos.ObtenerConexion())
            {
                using(SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CodTurno", codTurno);

                    using(SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            paciente.Legajo = (int)lector["Legajo"];
                            paciente.Nombre = (string)lector["Nombre"];
                            paciente.Apellido = (string)lector["Apellido"];
                            paciente.Dni = lector["DNI"].ToString().Trim();
                            paciente.Sexo = Convert.ToChar(lector["Sexo"]);
                            paciente.Nacionalidad = (string)lector["Nacionalidad"];
                            paciente.FechaNacimiento = (DateTime)lector["Fecha nacimiento"];
                            paciente.Direccion = (string)lector["Direccion"];
                            paciente.Provincia = (string)lector["Provincia"];
                            paciente.CorreoElectronico = (string)lector["Correo"];
                            paciente.Telefono= (string)lector["Telefono"];
                        }
                    }
                }
            }

            return paciente;
        }

        public int ObtenerUltimoPacienteRegistrado()
        {
            string consulta = "SELECT MAX(Legajo_PA) AS [Ultimo Legajo] FROM Paciente";
            try
            {
                DataTable UltimoLegajo = datos.ObtenerTabla("Medico", consulta);
                if (UltimoLegajo.Rows.Count > 0)
                {
                    DataRow fila = UltimoLegajo.Rows[0];
                    int NumLegajo = Convert.ToInt32(fila["Ultimo Legajo"]);
                    if (NumLegajo > 0)
                    {
                        return NumLegajo;
                    }
                }

                return 0;
            }
            catch (Exception)
            {
                throw new Exception("Error al obtener el último paciente registrado.");
            }
        }
    }
}
