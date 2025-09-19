using System;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class DaoMedico
    {
        ///---------------------------------------------------- Propiedades -------------------------------------------------------------------------------

        private readonly AccesoDatos datos;
        private SqlCommand sqlCommand;


        ///--------------------------------------------------- Constructores ------------------------------------------------------------------------------
        public DaoMedico()
        {
            datos = new AccesoDatos();
        }

        ///------------------------------------------------------ Metodos ---------------------------------------------------------------------------------


        //Alta Medico----------------------------------------------------------------------------------

        //devuelve el reader para provincia
        public SqlDataReader readerProvincias()
        {
            return datos.ObtenerLista("SELECT * FROM Provincia");
        }

        //devuelve el reader para especialidad
        public SqlDataReader readerEspecialidad()
        {
            return datos.ObtenerLista("SELECT * FROM Especialidad");
        }

        public DataTable getTablaMedicos()
        {
            const string consulta =
                "SELECT Apellido_ME AS Apellido, Nombre_ME AS Nombre, Legajo_ME AS Legajo, Sexo_ME AS Sexo, Nacionalidad_ME AS Nacionalidad, " +
                "FORMAT(FechaNacimiento_ME, 'dd/MM/yyyy') AS [Fecha de Nacimiento], Direccion_ME AS Dirección, Localidad_ME AS Localidad, " +
                "Descripcion_PR AS Provincia, Correo_ME AS Correo, Telefono_ME AS Teléfono, Descripcion_ES AS Especialidad, " +
                "DNI_ME AS DNI " +
                "FROM (Medico INNER JOIN Provincia " +
                "ON CodProvincia_ME = CodProvincia_PR) INNER JOIN Especialidad " +
                "ON CodigoEspecialidad_ME = CodEspecialidad_ES " +
                "WHERE Estado_ME = 1 " +
                "ORDER BY Apellido";
            
            DataTable dataTable = datos.ObtenerTabla("Medico", consulta);
            
            return dataTable;
        }

        public DataTable getTablaMedicosFiltrada(Medico medico, bool aplicarFiltroAvanzado, bool[,] filtros)
        {
            string consulta =
                "SELECT Legajo_ME AS Legajo, Apellido_ME AS Apellido, Nombre_ME AS Nombre, Sexo_ME AS Sexo, Nacionalidad_ME AS Nacionalidad, " +
                "FORMAT(FechaNacimiento_ME, 'dd/MM/yyyy') AS [Fecha de Nacimiento], Direccion_ME AS Dirección, Localidad_ME AS Localidad, " +
                "Descripcion_PR AS Provincia, Correo_ME AS Correo, Telefono_ME AS Teléfono, Descripcion_ES AS Especialidad, " +
                "DNI_ME AS DNI " +
                "FROM ((Medico INNER JOIN Provincia " +
                "ON CodProvincia_ME = CodProvincia_PR) INNER JOIN Especialidad " +
                "ON CodigoEspecialidad_ME = CodEspecialidad_ES) LEFT JOIN Disponibilidad " +
                "ON Legajo_ME = LegajoMedico_DIS " +
                "WHERE Estado_ME = 1";

            if (!aplicarFiltroAvanzado)
            {
                if (medico.DiaDisponible > 0)
                {
                    consulta += " AND NumDia_DIS = @NumDia_DIS AND Estado_DIS = 1";
                }
                else
                {
                    consulta += " AND Legajo_ME = @Legajo_ME";
                }

                consulta += " ORDER BY Apellido";

                sqlCommand = new SqlCommand(consulta);
                ArmarParametro_FiltroMedico(ref sqlCommand, medico);

                return datos.ObtenerTablaFiltrada("Medico", sqlCommand);
            }
            else
            {
                sqlCommand = GenerarConsultaAvanzada(medico, filtros, consulta);
                return datos.ObtenerTablaFiltrada("Medico", sqlCommand);
            }
        }

        public bool ExisteDniDuplicado(string dni, string nacionalidad, int legajoActual)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = @"SELECT COUNT(*) 
                            FROM Medico 
                            WHERE DNI_ME = @Dni 
                              AND Nacionalidad_ME = @Nacionalidad 
                              AND Legajo_ME <> @Legajo 
                              AND Estado_ME = 1"; // Asumiendo que solo importan activos

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@Dni", dni);
                cmd.Parameters.AddWithValue("@Nacionalidad", nacionalidad);
                cmd.Parameters.AddWithValue("@Legajo", legajoActual);

                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }

        private SqlCommand GenerarConsultaAvanzada(Medico medico, bool[,] filtros, string consulta)
        {
            sqlCommand = new SqlCommand();

            if (medico.DNI.Trim().Length > 0)
            {
                if (filtros[0, 0]) // Igual a
                {
                    consulta += " AND DNI_ME = @DNI_ME";
                    sqlCommand.Parameters.Add("@DNI_ME", SqlDbType.Char, 8).Value = medico.DNI;
                }
                else if (filtros[0, 1]) // Mayor a
                {
                    consulta += " AND DNI_ME > @DNI_ME";
                    sqlCommand.Parameters.Add("@DNI_ME", SqlDbType.Char, 8).Value = medico.DNI;
                }
                else if (filtros[0, 2]) // Menor a
                {
                    consulta += " AND DNI_ME < @DNI_ME";
                    sqlCommand.Parameters.Add("@DNI_ME", SqlDbType.Char, 8).Value = medico.DNI;
                }
            }

            if (medico.Apellido.Trim().Length > 0)
            {
                if (filtros[1, 0]) // Contiene
                {
                    consulta += " AND Apellido_ME LIKE @Apellido_ME";
                    sqlCommand.Parameters.Add("@Apellido_ME", SqlDbType.NVarChar, 50).Value = "%" + medico.Apellido + "%";
                }
                else if (filtros[1, 1]) // Empieza con
                {
                    consulta += " AND Apellido_ME LIKE @Apellido_ME";
                    sqlCommand.Parameters.Add("@Apellido_ME", SqlDbType.NVarChar, 50).Value = medico.Apellido + "%";
                }
                else if (filtros[1, 2]) // Termina con
                {
                    consulta += " AND Apellido_ME LIKE @Apellido_ME";
                    sqlCommand.Parameters.Add("@Apellido_ME", SqlDbType.NVarChar, 50).Value = "%" + medico.Apellido;
                }
            }

            if (medico.Correo.Trim().Length > 0)
            {
                if (filtros[2, 0]) // Contiene
                {
                    consulta += " AND Correo_ME LIKE @Correo_ME";
                    sqlCommand.Parameters.Add("@Correo_ME", SqlDbType.NVarChar, 100).Value = "%" + medico.Correo + "%";
                }
                else if (filtros[2, 1]) // Empieza con
                {
                    consulta += " AND Correo_ME LIKE @Correo_ME";
                    sqlCommand.Parameters.Add("@Correo_ME", SqlDbType.NVarChar, 100).Value = medico.Correo + "%";
                }
                else if (filtros[2, 2]) // Termina con
                {
                    consulta += " AND Correo_ME LIKE @Correo_ME";
                    sqlCommand.Parameters.Add("@Correo_ME", SqlDbType.NVarChar, 100).Value = "%" + medico.Correo;
                }
            }

            consulta += " ORDER BY Apellido";
            
            sqlCommand.CommandText = consulta;

            return sqlCommand;
        }

        public SqlDataReader ObtenerSqlDataReaderMedicoPorEspecialidad(string cod)
        {
            SqlDataReader reader;
            SqlConnection conexion;

            try
            {
                ValidarOCrearProcedimientoMedicoPorEspecialidad();

                conexion = datos.ObtenerConexion();
                SqlCommand comando = new SqlCommand("SP_ObtenerListaMedicosPorEspecialidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CodigoEspecialidad_ME", cod);

                reader = comando.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }

            return reader;
        }

        private void ValidarOCrearProcedimientoMedicoPorEspecialidad()
        {
            SqlConnection conexion = datos.ObtenerConexion();
            using (conexion)
            {
                // Verificar si existe el procedimiento
                string consultaExiste = @"
                    SELECT COUNT(*) 
                    FROM sys.objects 
                    WHERE type = 'P' AND name = 'SP_ObtenerListaMedicosPorEspecialidad'";

                SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                int cantidad = (int)cmdExiste.ExecuteScalar();

                if (cantidad == 0)
                {
                    // Crear el procedimiento si no existe
                    string crearProc = @"
                        CREATE PROCEDURE SP_ObtenerListaMedicosPorEspecialidad(
                        @CodigoEspecialidad_ME INT
                        )
                        AS
                        BEGIN
                        SELECT Legajo_ME,
                               Apellido_ME + ' ' + Nombre_ME AS 'Medico', 
                               CodigoEspecialidad_ME
                        FROM Medico
                        WHERE CodigoEspecialidad_ME = @CodigoEspecialidad_ME AND Estado_Me = 1
                        END";

                    SqlCommand cmdCrear = new SqlCommand(crearProc, conexion);
                    cmdCrear.ExecuteNonQuery();
                }
            }
        }

        public DataTable getDias()
        {
            return datos.ObtenerTabla("Dia", "SELECT NumDia_DI, Descripcion_DI FROM Dia");
        }

        //Carga los parametros en el SQL command
        private void CargarParametros(ref SqlCommand cmd, Medico medico)
        {
            if (medico.Legajo != 0)
            {
                cmd.Parameters.Add("@Legajo_ME", SqlDbType.Int).Value = medico.Legajo;
            }
            else
            {
                cmd.Parameters.Add("@Estado_ME", SqlDbType.Bit).Value = medico.Estado;
            }
            cmd.Parameters.Add("@Nombre_ME", SqlDbType.NVarChar, 50).Value = medico.Nombre;
            cmd.Parameters.Add("@Apellido_ME", SqlDbType.NVarChar, 50).Value = medico.Apellido;
            cmd.Parameters.Add("@Sexo_ME", SqlDbType.Char).Value = medico.Sexo;
            cmd.Parameters.Add("@Nacionalidad_ME", SqlDbType.NVarChar, 50).Value = medico.Nacionalidad;
            cmd.Parameters.Add("@FechaNacimiento_ME", SqlDbType.Date).Value = medico.FechaNacimiento;
            cmd.Parameters.Add("@Direccion_ME", SqlDbType.NVarChar, 100).Value = medico.Direccion;
            cmd.Parameters.Add("@Localidad_ME", SqlDbType.NVarChar, 50).Value = medico.Localidad;
            cmd.Parameters.Add("@CodProvincia_ME", SqlDbType.Int).Value = medico.CodigoProvincia;
            cmd.Parameters.Add("@Correo_ME", SqlDbType.NVarChar, 100).Value = medico.Correo;
            cmd.Parameters.Add("@Telefono_ME", SqlDbType.VarChar, 16).Value = medico.Telefono;
            cmd.Parameters.Add("@CodigoEspecialidad_ME", SqlDbType.Int).Value = medico.CodigoEspecialidad;
            cmd.Parameters.Add("@DNI_ME", SqlDbType.Char, 8).Value = medico.DNI;
        }

        private void ArmarParametro_FiltroMedico(ref SqlCommand command, Medico medico)
        {
            if (medico.DiaDisponible > 0)
            {
                command.Parameters.Add("@NumDia_DIS", SqlDbType.Int).Value = medico.DiaDisponible;
            }
            else
            {
                command.Parameters.Add("@Legajo_ME", SqlDbType.Int).Value = medico.Legajo;
            }
        }

        //Agregar Medico
        public bool AgregarMedico(Medico medico)
        {
            //Variable consulta
            const string consulta = "INSERT INTO Medico ([Nombre_ME], [Apellido_ME], [Sexo_ME], [Nacionalidad_ME], [FechaNacimiento_ME], [Direccion_ME], [Localidad_ME], [CodProvincia_ME], [Correo_ME], [Telefono_ME], [CodigoEspecialidad_ME], [DNI_ME], [Estado_ME])" +
                                    " VALUES (@Nombre_ME, @Apellido_ME, @Sexo_ME, @Nacionalidad_ME, @FechaNacimiento_ME, @Direccion_ME, @Localidad_ME, @CodProvincia_ME, @Correo_ME, @Telefono_ME, @CodigoEspecialidad_ME, @DNI_ME, @Estado_ME)";

            //Creo el SqlCommand
            sqlCommand = new SqlCommand(consulta);

            //Cargo los parametros
            CargarParametros(ref sqlCommand, medico);
            return datos.AltaPorParametros(sqlCommand);
        }

        //Modificacion Medico ------------------------------------------------------------------------
        public DataTable ObtenerTablaCompleta_Medico()
        {
            string consultaModificacion = "SELECT M.Legajo_ME AS 'Legajo', M.Nombre_ME As 'Nombre', M.Apellido_ME As 'Apellido', " + 
                "M.DNI_ME As 'DNI', CASE M.Sexo_ME WHEN 'F' Then 'Femenino' ELSE 'Masculino' END AS 'Sexo', FORMAT(M.FechaNacimiento_ME, " + 
                "'yyyy/MM/dd') AS [Fecha de Nacimiento], M.Nacionalidad_ME AS 'Nacionalidad' ,(SELECT P.Descripcion_PR FROM Provincia  AS P " +
                "WHERE P.CodProvincia_PR = M.CodProvincia_ME) AS 'Provincia', M.CodProvincia_ME AS 'CodProvincia', M.Localidad_ME AS 'Localidad', " + 
                "M.Direccion_ME AS 'Direccion', M.Telefono_ME AS 'Telefono', M.Correo_ME AS 'Correo', " + 
                "(SELECT E.Descripcion_ES FROM Especialidad AS E WHERE E.CodEspecialidad_ES = M.CodigoEspecialidad_ME) AS 'Especialidad' " +
                ", M.CodigoEspecialidad_ME AS 'CodEspecialidad' FROM Medico AS M WHERE M.Estado_ME = 1 ORDER BY Apellido ";

            return datos.ObtenerTabla("Medico", consultaModificacion);
        }

        private void ValidarOCrearProcedimientoModificacionMedico()
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consultaExiste = "SELECT COUNT(*) FROM sys.objects WHERE type = 'P' AND name = 'ModificarMedico_Grupo10'";
                SqlCommand cmdExiste = new SqlCommand(consultaExiste, conexion);
                if ((int)cmdExiste.ExecuteScalar() == 0)
                {
                    string crearProc = @"
                        CREATE PROCEDURE ModificarMedico_Grupo10
                            @Legajo_ME INT, @DNI_ME CHAR(8), @Nombre_ME VARCHAR(50), @Apellido_ME VARCHAR(50),
                            @Sexo_ME CHAR(1), @Nacionalidad_ME VARCHAR(50), @FechaNacimiento_ME DATE,
                            @Direccion_ME VARCHAR(100), @Localidad_ME VARCHAR(50), @CodProvincia_ME INT,
                            @CodigoEspecialidad_ME INT, @Correo_ME VARCHAR(100), @Telefono_ME CHAR(12)
                        AS
                        BEGIN
                            UPDATE Medico SET 
                                DNI_ME = @DNI_ME, Nombre_ME = @Nombre_ME, Apellido_ME = @Apellido_ME, Sexo_ME = @Sexo_ME, 
                                Nacionalidad_ME = @Nacionalidad_ME, FechaNacimiento_ME = @FechaNacimiento_ME, 
                                Direccion_ME = @Direccion_ME, Localidad_ME = @Localidad_ME, 
                                CodProvincia_ME = @CodProvincia_ME, 
                                CodigoEspecialidad_ME = @CodigoEspecialidad_ME, 
                                Correo_ME = @Correo_ME, Telefono_ME = @Telefono_ME
                            WHERE Legajo_ME = @Legajo_ME;
                        END";
                    SqlCommand cmdCrear = new SqlCommand(crearProc, conexion);
                    cmdCrear.ExecuteNonQuery();
                }
            }
        }

        public int ModificacionMedico(Medico medico)
        {
            sqlCommand = new SqlCommand();
            CargarParametros(ref sqlCommand, medico);
            ValidarOCrearProcedimientoModificacionMedico();
            return datos.EjecutarProcedimientoAlmacenado("ModificarMedico_Grupo10", sqlCommand);
        }

        public int BajaLogicaMedicoPorLegajo(string legajo)
        {
            try
            {
                using (SqlConnection conexion = datos.ObtenerConexion())
                {
                    string consulta = "UPDATE Turno SET Estado_TU = 0 WHERE LegajoMedico_TU = @LegajoMedico_TU AND Estado_TU = 1";
                    SqlCommand comandoTurnos = new SqlCommand(consulta, conexion);
                    comandoTurnos.Parameters.AddWithValue("@LegajoMedico_TU", legajo);
                    comandoTurnos.ExecuteNonQuery();

                    consulta = "UPDATE Disponibilidad SET Estado_DIS = 0 WHERE LegajoMedico_DIS = @LegajoMedico_DIS AND Estado_DIS = 1";
                    SqlCommand comandoDisponibilidad = new SqlCommand(consulta, conexion);
                    comandoDisponibilidad.Parameters.AddWithValue("@LegajoMedico_DIS", legajo);
                    comandoDisponibilidad.ExecuteNonQuery();

                    consulta = "UPDATE Medico SET Estado_ME = 0 WHERE Legajo_ME = @Legajo_ME AND Estado_ME = 1";
                    SqlCommand comandoMedico = new SqlCommand(consulta, conexion);
                    comandoMedico.Parameters.AddWithValue("@Legajo_ME", legajo);
                    int filasAfectadas = comandoMedico.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
            catch
            {
                return -1;
            }
        }

        public DataTable ObtenerMedicosXEspecialidad()
        {
            string consulta = "SELECT E.Descripcion_ES AS 'Especialidad', COUNT(M.Legajo_ME) AS 'Cantidad de Médicos' FROM Medico AS M "
                            + "INNER JOIN Especialidad AS E ON M.CodigoEspecialidad_ME = E.CodEspecialidad_ES "
                            + "WHERE M.Estado_ME = 1 GROUP BY E.Descripcion_ES";

            return datos.ObtenerTabla("MedicosXEspecialidad", consulta);
        }

        public DataTable ObtenerMedicoMasSolicitado()
        {
            string consulta = @"
                SELECT TOP 1 
                    M.Legajo_ME AS 'Legajo',
                    M.Nombre_ME AS 'Nombre',
                    M.Apellido_ME AS 'Apellido',
                    COUNT(T.CodTurno_TU) AS 'Cantidad de Turnos'
                FROM 
                    Turno T
                INNER JOIN 
                    Medico M ON T.LegajoMedico_TU = M.Legajo_ME
                WHERE 
                    T.Estado_TU = 1
                GROUP BY 
                    M.Legajo_ME, M.Nombre_ME, M.Apellido_ME
                ORDER BY 
                    COUNT(T.CodTurno_TU) DESC;";

            return datos.ObtenerTabla("MedicoMasSolicitado", consulta);
        }

        public int ValidarExistenciaMedicoXDni(Medico dni)
        {
            string consultaValidacion = "SELECT * FROM Medico WHERE DNI_ME = @DNI_ME AND Estado_ME = 1";
            sqlCommand = new SqlCommand(consultaValidacion);
            sqlCommand.Parameters.Add("@DNI_ME", SqlDbType.Char, 8).Value = dni.DNI;
            try
            {
                return datos.ObtenerTablaFiltrada("Medico", sqlCommand).Rows.Count;
            }
            catch (Exception)
            {
                throw new Exception("Error al validar la existencia del médico por DNI.");
            }
        }

        public int ObtenerUltimoMedicoRegistrado()
        {
            string consulta = "SELECT MAX(Legajo_ME) AS [Ultimo Legajo] FROM Medico";
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
                throw new Exception("Error al obtener el último médico registrado.");
            }
        }
    }
}
