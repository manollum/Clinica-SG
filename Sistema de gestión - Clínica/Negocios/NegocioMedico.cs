using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegocioMedico
    {
        ///---------------------------------------------------- Propiedades -------------------------------------------------------------------------------
        private readonly DaoMedico daoMedico;

        ///--------------------------------------------------- Constructores ------------------------------------------------------------------------------
        public NegocioMedico()
        {
            daoMedico = new DaoMedico();
        }

        ///------------------------------------------------------ Metodos ---------------------------------------------------------------------------------
        
        //Alta Medico---------------------------------------------
        public SqlDataReader readerProvincias()
        {
            return daoMedico.readerProvincias();
        }

        public SqlDataReader readerEspecialidad()
        {
            return daoMedico.readerEspecialidad();
        }

        public SqlDataReader ObtenerSqlDataReaderMedicoPorEspecialidad(string cod)
        {
            return daoMedico.ObtenerSqlDataReaderMedicoPorEspecialidad(cod);
        }

        public DataTable getTablaMedicos()
        {
            return daoMedico.getTablaMedicos();
        }

        public DataTable getTablaMedicosFiltrada(Medico medico, bool aplicarFiltroAvanzado, bool[,] filtros)
        {
            return daoMedico.getTablaMedicosFiltrada(medico, aplicarFiltroAvanzado, filtros);
        }

        public bool AgregarMedico(Medico medico)
        {
            return daoMedico.AgregarMedico(medico);
        }

        public DataTable getDias()
        {
            return daoMedico.getDias();
        }

        public bool ExisteDniDuplicado(string dni, string nacionalidad, int legajoActual)
        {
            return daoMedico.ExisteDniDuplicado(dni, nacionalidad, legajoActual);
        }

        //Modificar Medico----------------------------------------
        public DataTable ObtenerMedicos()
        {
            return daoMedico.ObtenerTablaCompleta_Medico();
        }

        public bool ModificarMedico(Medico medico)
        {
            if (daoMedico.ModificacionMedico(medico) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int BajaLogicaMedicoPorLegajo(string legajo)
        {
            return daoMedico.BajaLogicaMedicoPorLegajo(legajo);
        }

        public DataTable ObtenerMedicosXEspecialidad()
        {
            return daoMedico.ObtenerMedicosXEspecialidad();
        }

        public DataTable ObtenerMedicoMasSolicitado()
        {
            DaoMedico daoMedico = new DaoMedico();
            return daoMedico.ObtenerMedicoMasSolicitado();
        }

        public int ValidarExistenciaMedicoXDNI(Medico dni)
        {
            return daoMedico.ValidarExistenciaMedicoXDni(dni);
        }

        public int ObtenerUltimoLegajoMedico()
        {
            return daoMedico.ObtenerUltimoMedicoRegistrado();
        }
    }
}
