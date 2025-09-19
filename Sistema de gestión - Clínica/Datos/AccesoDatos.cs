using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Configuration;


namespace Datos
{
    public class AccesoDatos
    {
        ///---------------------------------------------------- Propiedades -------------------------------------------------------------------------------
        private string rutaConexion = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TPI-Grupo10;Integrated Security=True;";
        private bool estadoConexion = false;
        private SqlConnection conexion;
        private SqlDataAdapter dataAdapter;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;
        private DataSet dataSet;
        private int filasAfectadas;

        public AccesoDatos()
        {

        }
        public string GetRutaConexion()
        {
            return rutaConexion;
        }
        public bool GetEstadoConexion()
        {
            return estadoConexion;
        }
        public SqlConnection GetConexion()
        {
            return conexion;
        }
        public SqlDataAdapter GetDataAdapter()
        {
            return dataAdapter;
        }
        public SqlCommand GetSqlCommand()
        {
            return sqlCommand;
        }
        public int GetFilasAfectadas()
        {
            return filasAfectadas;
        }
        public DataSet GetDataSet()
        {
            return dataSet;
        }
        public void SetRutaConexion(string ruta)
        {
            rutaConexion = ruta;
        }
        public void SetEstadoConexion(bool estado)
        {
            estadoConexion = estado;
        }
        public void SetConexion(SqlConnection Conexion)
        {
            conexion = Conexion;
        }
        public void SetDataAdapter(SqlDataAdapter adapter)
        {
            dataAdapter = adapter;
        }
        public void SetSqlCommand(SqlCommand command)
        {
            sqlCommand = command;
        }
        public void SetFilasAfectadas(int filas)
        {
            filasAfectadas = filas;
        }
        public void SetDataSet(DataSet ds)
        {
            dataSet = ds;
        }

        ///---------------------------------------------------- Métodos -------------------------------------------------------------------------------
        public SqlConnection ObtenerConexion()
        {
            conexion = new SqlConnection(rutaConexion);
            try
            {
                conexion.Open();
                estadoConexion = true;
                return conexion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CerrarConexion()
        {
            if (estadoConexion)
            {
                conexion.Close();
                estadoConexion = false;
            }
        }

        public SqlDataAdapter ObtenerDataAdapter(string consulta, SqlConnection sqlConnection)
        {
            try
            {
                dataAdapter = new SqlDataAdapter(consulta, sqlConnection);
                return dataAdapter;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public int EjecutarProcedimientoAlmacenado(string nombreProcedimiento, SqlCommand command)
        {
            filasAfectadas = 0;
            conexion = ObtenerConexion();
            if (conexion == null)
            {
                throw new Exception("No se pudo establecer la conexión a la base de datos.");
            }
            else
            {
                using (conexion)
                {
                    sqlCommand = new SqlCommand();
                    sqlCommand = command;
                    sqlCommand.Connection = conexion;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = nombreProcedimiento;            
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                }

                return filasAfectadas;
            }
        }

        public bool AltaPorParametros(SqlCommand cmd)
        {
            //Obtengo lo necesario usando los metodos anteriores
            SqlConnection connection = ObtenerConexion();

            //Creo el sqlComand y ejecuto
            cmd.Connection = connection;
            int resultado = cmd.ExecuteNonQuery();

            //Cierro
            CerrarConexion();

            if (resultado <= 0)
            {
                return false;
            }

            //Devuelvo true
            return true;
        }

        public DataTable ObtenerTabla(string NombreTabla, string consulta)
        {
            conexion = ObtenerConexion();
            estadoConexion = true;
            if (conexion == null)
            {
                throw new Exception("No se pudo establecer la conexión a la base de datos.");
            }
            using (conexion)
            {
                dataAdapter = ObtenerDataAdapter(consulta, conexion);
                if (dataAdapter != null)
                {
                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, NombreTabla);
                }
                else
                {
                    throw new Exception("No se pudo obtener el DataAdapter para la consulta proporcionada.");
                }
                estadoConexion = false;
            }
            return dataSet.Tables[NombreTabla];
        }

        public DataTable ObtenerTablaFiltrada(string nombreTabla, SqlCommand consulta)
        {
            //Instancio el dataSet
            dataSet = new DataSet();

            //Abro la conexion
            conexion = ObtenerConexion();

            //Establezco la conexion al SqlCommand e instancio el dataAdapter
            consulta.Connection = conexion;
            dataAdapter = new SqlDataAdapter(consulta);

            //Relleno el dataSet
            dataAdapter.Fill(dataSet, nombreTabla);

            //Cierro la conexion
            conexion.Close();
            estadoConexion = false;

            //Devuelvo la tabla
            return dataSet.Tables[nombreTabla]; 
        }

        public SqlDataReader ObtenerLista(string consulta)
        {
            conexion = ObtenerConexion();
            estadoConexion = true;

            if (conexion == null)
            {
                throw new Exception("No se pudo establecer la conexión a la base de datos.");
            }

            SqlCommand comando = new SqlCommand(consulta, conexion);
            sqlDataReader = comando.ExecuteReader(CommandBehavior.CloseConnection);

            estadoConexion = false;

            return sqlDataReader;
        }
    }
}
