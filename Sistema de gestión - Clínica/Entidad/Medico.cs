using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico
    {
        ///---------------------------------------------------- Propiedades -------------------------------------------------------------------------------
        private int _Legajo;
        private string _Nombre = "";
        private string _Apellido = "";
        private char _Sexo;
        private string _Nacionalidad = "";
        private DateTime _FechaNacimiento;
        private string _Direccion = "";
        private string _Localidad = "";
        private int _CodigoProvincia;
        private string _Correo = "";
        private string _Telefono = "";
        private int _CodigoEspecialidad;
        private string _DNI = "";
        private bool _Estado;
        private int _DiaDisponible;

        ///--------------------------------------------------- Constructores ------------------------------------------------------------------------------
        public Medico() //Constructor vacio
        {

        }

        ///------------------------------------------------- Getters y Setters ---------------------------------------------------------------------------------
        public int Legajo
        {
            get {return _Legajo;}
            set {_Legajo = value;}
        }

        public string Nombre
        {
            get {return _Nombre;}
            set {_Nombre = value;}
        }

        public string Apellido
        {
            get {return _Apellido;}
            set {_Apellido = value;}
        }

        public char Sexo
        {
            get {return _Sexo;}
            set {_Sexo = value;}
        }

        public string Nacionalidad
        {
            get {return _Nacionalidad;}
            set {_Nacionalidad = value;}
        }

        public DateTime FechaNacimiento
        {
            get {return _FechaNacimiento;}
            set {_FechaNacimiento = value;}
        }

        public string Direccion
        {
            get {return _Direccion;}
            set {_Direccion = value;}
        }

        public string Localidad
        {
            get {return _Localidad;}
            set {_Localidad = value;}
        }

        public int CodigoProvincia
        {
            get {return _CodigoProvincia;}
            set {_CodigoProvincia = value;}
        }

        public string Correo
        {
            get {return _Correo;}
            set {_Correo = value;}
        }

        public string Telefono
        {
            get {return _Telefono;}
            set {_Telefono = value;}
        }

        public int CodigoEspecialidad
        {
            get {return _CodigoEspecialidad;}
            set {_CodigoEspecialidad = value;}
        }

        public string DNI
        {
            get {return _DNI;}
            set {_DNI = value;}
        }

        public bool Estado
        {
            get {return _Estado;}
            set {_Estado = value;}
        }

        public int DiaDisponible
        {
            get {return _DiaDisponible;}
            set {_DiaDisponible = value;}
        }

        ///------------------------------------------------------ Metodos ---------------------------------------------------------------------------------

    }
}
