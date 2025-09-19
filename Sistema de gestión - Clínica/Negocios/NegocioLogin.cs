using System;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Negocios
{
    public class NegocioLogin
    {
        private readonly DaoLogin dao;

        public NegocioLogin()
        {
            dao = new DaoLogin();
        }

        // Validación para Administrador
        public bool ValidarUsuarioAdministrador(string usuario, string contrasena)
        {
            DataTable tabla = dao.ObtenerAdministrador(usuario, contrasena);
            return tabla.Rows.Count > 0;
        }

        // Validación para Médico
        public bool ValidarUsuarioMedico(string usuario, string contrasena)
        {
            DataTable tabla = dao.ObtenerMedico(usuario, contrasena);
            return tabla.Rows.Count > 0;
        }

        // Obtener el nombre completo del médico a partir del usuario y contraseña
        public string ObtenerNombreCompletoMedico(string usuario, string contrasena)
        {
            DataTable tabla = dao.ObtenerMedico(usuario, contrasena);

            if (tabla.Rows.Count > 0)
            {
                int legajoMedico = Convert.ToInt32(tabla.Rows[0]["LegajoMedico_UM"]);
                return dao.ObtenerNombreCompletoMedico(legajoMedico);
            }

            return string.Empty;
        }

        public string ObtenerLegajo(string usuario, string contrasena)
        {
            DataTable RegistroMedico = dao.ObtenerMedico(usuario, contrasena);
            if (RegistroMedico.Rows.Count > 0)
            {
                string legajo = RegistroMedico.Rows[0]["LegajoMedico_UM"].ToString();
                return legajo;

            }
            return null;
        }
    }
}