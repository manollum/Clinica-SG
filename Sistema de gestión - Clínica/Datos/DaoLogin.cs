using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Datos

{
    public class DaoLogin
    {
        private readonly AccesoDatos datos;

        public DaoLogin()
        {
            datos = new AccesoDatos();
        }

        // LOGIN PARA ADMINISTRADORES
        public DataTable ObtenerAdministrador(string usuario, string contrasena)
        {
            string consulta = "SELECT * FROM UsuarioAdministrador WHERE Nombre_UA = @usuario AND Contraseña_UA = @contrasena AND Estado_UA = 1";

            SqlCommand cmd = new SqlCommand(consulta);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@contrasena", contrasena);

            return datos.ObtenerTablaFiltrada("UsuarioAdministrador", cmd);
        }

        // LOGIN PARA MÉDICOS
        public DataTable ObtenerMedico(string usuario, string contrasena)
        {
            string consulta = "SELECT * FROM UsuarioMedico WHERE Nombre_UM = @usuario AND Contraseña_UM = @contrasena AND Estado_UM = 1";

            SqlCommand cmd = new SqlCommand(consulta);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@contrasena", contrasena);

            return datos.ObtenerTablaFiltrada("UsuarioMedico", cmd);
        }

        // OBTENER NOMBRE + APELLIDO DEL MÉDICO (para mostrar en el label luego del login)
        public string ObtenerNombreCompletoMedico(int legajoMedico)
        {
            string consulta = @"SELECT M.Nombre_ME, M.Apellido_ME 
                        FROM Medico M 
                        INNER JOIN UsuarioMedico UM ON M.Legajo_ME = UM.LegajoMedico_UM
                        WHERE UM.LegajoMedico_UM = @legajo";

            SqlCommand cmd = new SqlCommand(consulta);
            cmd.Parameters.AddWithValue("@legajo", legajoMedico);

            DataTable tabla = datos.ObtenerTablaFiltrada("Medico", cmd);

            if (tabla.Rows.Count > 0)
            {
                string nombre = tabla.Rows[0]["Nombre_ME"].ToString();
                string apellido = tabla.Rows[0]["Apellido_ME"].ToString();
                return $"{nombre} {apellido}";
            }

            return string.Empty;
        }

    }
}
