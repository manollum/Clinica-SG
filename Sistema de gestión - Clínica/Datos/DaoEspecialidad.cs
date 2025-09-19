using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoEspecialidad
    {
        private readonly AccesoDatos datos;
        private const string consultaBase = "SELECT * FROM Especialidad";
        private string consultaPorEspecialidad;
       
        public DaoEspecialidad()
        {
            datos = new AccesoDatos();
        }

        public SqlDataReader ObtenerSqlDataReaderEspecialidad()
        {
            return datos.ObtenerLista(consultaBase);
        }

        public DataTable ObtenerTablaEspecialidad()
        {
            return datos.ObtenerTabla("Especialidad", consultaBase);
        }

        public DataTable ObtenerTablaEspecialidadPorCod(int codEspecialidad)
        {        
            consultaPorEspecialidad = consultaBase + " WHERE CodEspecialidad_ES = " + codEspecialidad;
            return datos.ObtenerTabla("Especialidad", consultaPorEspecialidad);
        }
    }
}
