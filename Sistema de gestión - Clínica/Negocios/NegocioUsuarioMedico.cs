using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocios
{
    public class NegocioUsuarioMedico
    {
        DaoUsuarioMedico daoUsuarioMedico = new DaoUsuarioMedico();

        // 1️⃣ Verificar si existe el médico por legajo
        public bool ExisteMedicoPorLegajo(int legajoMedico)
        {
            return daoUsuarioMedico.ExisteMedicoPorLegajo(legajoMedico);
        }

        // 2️⃣ Verificar si ya tiene usuario asignado ese legajo
        public bool ExisteUsuarioMedicoPorLegajo(int legajoMedico)
        {
            return daoUsuarioMedico.ExisteUsuarioMedicoPorLegajo(legajoMedico);
        }

        // 3️⃣ Insertar un nuevo usuario médico
        public bool InsertarUsuarioMedico(int legajoMedico, string nombreUsuario, string contrasena)
        {
            int filasAfectadas = daoUsuarioMedico.InsertarUsuarioMedico(legajoMedico, nombreUsuario, contrasena);
            return filasAfectadas > 0;
        }
    }
}