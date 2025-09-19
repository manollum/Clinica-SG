using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocios
{
    public class NegocioDia
    {
        private readonly DaoDia daoDia;

        public NegocioDia()
        {
            daoDia = new DaoDia();
        }

        public DataTable ObtenerTablaDia()
        {
            return daoDia.ObtenerTablaDia();
        }
    }
}
