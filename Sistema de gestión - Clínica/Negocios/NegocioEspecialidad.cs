using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NegocioEspecialidad
    {
        private readonly DaoEspecialidad daoE;

        public NegocioEspecialidad()
        {
            daoE = new DaoEspecialidad();
        }

        public DataTable ObtenerEspecialidades()
        {
            return daoE.ObtenerTablaEspecialidad();
        }

        public SqlDataReader ObtenerSqlDataReaderEspecialidad()
        {
            return daoE.ObtenerSqlDataReaderEspecialidad();
        }
    }
}
