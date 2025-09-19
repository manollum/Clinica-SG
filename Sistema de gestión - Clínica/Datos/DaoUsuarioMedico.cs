using System;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class DaoUsuarioMedico
    {
        AccesoDatos datos = new AccesoDatos();

        // 1️ Verificar si existe el médico con ese legajo
        public bool ExisteMedicoPorLegajo(int legajoMedico)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "SELECT COUNT(*) FROM Medico WHERE Legajo_ME = @Legajo";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Legajo", legajoMedico);

                int cantidad = (int)comando.ExecuteScalar();
                return cantidad > 0;
            }
        }

        // 2️ Verificar si ya existe un usuario médico para ese legajo
        public bool ExisteUsuarioMedicoPorLegajo(int legajoMedico)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "SELECT COUNT(*) FROM UsuarioMedico WHERE LegajoMedico_UM = @Legajo AND Estado_UM = 1";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Legajo", legajoMedico);

                int cantidad = (int)comando.ExecuteScalar();
                return cantidad > 0;
            }
        }

        // 3️ Insertar un nuevo usuario médico
        public int InsertarUsuarioMedico(int legajoMedico, string nombreUsuario, string contrasena)
        {
            using (SqlConnection conexion = datos.ObtenerConexion())
            {
                string consulta = "INSERT INTO UsuarioMedico (LegajoMedico_UM, Nombre_UM, Contraseña_UM, Estado_UM) " +
                                  "VALUES (@Legajo, @Nombre, @Contrasena, 1)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Legajo", legajoMedico);
                comando.Parameters.AddWithValue("@Nombre", nombreUsuario);
                comando.Parameters.AddWithValue("@Contrasena", contrasena);

                return comando.ExecuteNonQuery();
            }
        }
    }
}