using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoDia
    {
        AccesoDatos datos;

        public DaoDia()
        {
            datos = new AccesoDatos();
        }

        public DataTable ObtenerTablaDia()
        {
            return datos.ObtenerTabla("Dia", "SELECT * FROM Dia");
        }
    }
}
