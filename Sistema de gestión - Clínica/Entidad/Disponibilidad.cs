using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Disponibilidad
    {
        // Parámetros
        private int _numDia;
        private int _legajoMedico;
        private string _nombreDia;
        private TimeSpan _horarioInicio;
        private TimeSpan _horarioFin;
        private bool _estado;

        // Métodos
        public Disponibilidad() { /*Constructor vacio*/ }

        // Constructor con parámetros
        public Disponibilidad(int numDia, int legajoMedico, TimeSpan horarioInicio, TimeSpan horarioFin, bool estado)
        {
            _numDia = numDia;
            _legajoMedico = legajoMedico;
            _horarioInicio = horarioInicio;
            _horarioFin = horarioFin;
            _estado = estado;
        }

        public int NumDia
        {
            get { return _numDia; }
            set { _numDia = value; }
        }

        public int LegajoMedico
        {
            get { return _legajoMedico; }
            set { _legajoMedico = value; }
        }

        public string NombreDia
        {
            get { return _nombreDia; }

            set { _nombreDia = value; }
        }

        public TimeSpan HorarioInicio
        {
            get { return _horarioInicio; }
            set { _horarioInicio = value; }
        }

        public TimeSpan HorarioFin
        {
            get { return _horarioFin; }
            set { _horarioFin = value; }
        }

        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}