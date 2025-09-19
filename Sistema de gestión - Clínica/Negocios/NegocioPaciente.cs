using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocios
{   
    public class NegocioPaciente
    {
        private readonly DaoPaciente daoP;
        private bool[,] filtros = new bool[3, 3];
        private Paciente paciente1 = new Paciente();

        public NegocioPaciente()
        {
            daoP = new DaoPaciente();
        }

        // -------------------- Alta Paciente ------------------------------------
        public bool AltaPaciente(Paciente paciente)
        {
            if (daoP.AltaPaciente(paciente) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public DataTable getRegistrosProvincias()
        {
            return daoP.getProvincias();
        }
        // ------------------------------------------------------------------------





        //-------------------- Validaciones y Verificaciones Pacientes ----------------------

        // Para evitar repetidos, revisar los metodos existentes antes de crear uno nuevo.
        public bool VerificarExistenciaPacienteXDNI(Paciente paciente)
        {
            return daoP.VerificarExistenciaPacienteXDNI(paciente);
        }

        public bool ExisteDniDuplicado(string dni, string nacionalidad, int legajoActual)
        {
            return daoP.ExisteDniDuplicado(dni, nacionalidad, legajoActual);
        }
        // ------------------------------------------------------------------------

        public int BajaLogicaPacientePorLegajo(string legajo)
        {
            return daoP.BajaLogicaPacientePorLegajo(legajo);
        }

        public DataTable ObtenerPacientes()
        {
            return daoP.ObtenerPacientes();
        }

        public DataTable ObtenerPacientes_Filtrados(Paciente paciente, bool FiltrosAvanzados, bool[,] filtros)
        {
            return daoP.ObtenerPacientes_Filtrados(paciente, FiltrosAvanzados, filtros);
        }

        //Modificar Paciente----------------------------------------
        public bool ModificarPaciente(Paciente paciente)
        {
            if (daoP.ModificacionPaciente(paciente) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Paciente ObtenerPacienteXTurno(int codTurno)
        {
            return daoP.ObtenerPacienteXCodTurno(codTurno);
        }

        public int ObtenerUltimoPacienteRegistrado()
        {
            return daoP.ObtenerUltimoPacienteRegistrado();
        }
    }
}