using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pantallas
{
    public class clsConsultas
    {
        public string CommandText, resultado;
        public bool NumFila;
        public SqlDataAdapter adapter;
        public DataSet dataSet;
        public SqlConnection conexion = new SqlConnection(@"Data Source = JUAICSM1; Initial Catalog = Maintenance; Persist Security Info = True; User ID = pdatech; password = control");
        public SqlCommand cmd = new SqlCommand();
        SqlDataReader mireader;

        public clsConsultas()
        {
            CerrarConexion();
            CommandText = "";
            resultado = "";
            NumFila = false;
        }

        public string Obtener(string campo)
        {
            CerrarConexion();
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;

            mireader = cmd.ExecuteReader();
            while (mireader.Read())
            {
                resultado = mireader[campo].ToString();
            }
            mireader.Close();
            conexion.Close();
            CerrarConexion();

            return resultado;
        }


        public string[] Obtener2campos(string campo1, string campo2)
        {
            CerrarConexion();
            string[] arreglo = new string[2];
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;

            mireader = cmd.ExecuteReader();
            while (mireader.Read())
            {
                arreglo[0] = mireader[campo1].ToString();
                arreglo[1] = mireader[campo2].ToString();
            }
            mireader.Close();
            conexion.Close();
            CerrarConexion();
            return arreglo;
        }



        public string[] Obtener5campos(string campo1, string campo2, string campo3, string campo4, string campo5)
        {
            CerrarConexion();
            string[] arreglo = new string[5];
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;

            mireader = cmd.ExecuteReader();
            while (mireader.Read())
            {
                arreglo[0] = mireader[campo1].ToString();
                arreglo[1] = mireader[campo2].ToString();
                arreglo[2] = mireader[campo3].ToString();
                arreglo[3] = mireader[campo4].ToString();
                arreglo[4] = mireader[campo5].ToString();
            }
            mireader.Close();
            conexion.Close();
            CerrarConexion();
            return arreglo;
        }



        public string[] Obtener6campos(string campo1, string campo2, string campo3, string campo4, string campo5, string campo6)
        {
            CerrarConexion();
            string[] arreglo = new string[6];
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;

            mireader = cmd.ExecuteReader();
            while (mireader.Read())
            {
                arreglo[0] = mireader[campo1].ToString();
                arreglo[1] = mireader[campo2].ToString();
                arreglo[2] = mireader[campo3].ToString();
                arreglo[3] = mireader[campo4].ToString();
                arreglo[4] = mireader[campo5].ToString();
                arreglo[5] = mireader[campo6].ToString();
            }
            mireader.Close();
            conexion.Close();
            CerrarConexion();
            return arreglo;
        }


        public DataTable SeleccionarTabla()
        {
            CerrarConexion();
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;
            cmd.ExecuteNonQuery();
            adapter = new SqlDataAdapter(cmd);
            DataTable dataSet = new DataTable();
            adapter.Fill(dataSet);
            conexion.Close();
            CerrarConexion();

            return dataSet;
        }


        public bool VerificarSiExiste()
        {
            CerrarConexion();
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;

            mireader = cmd.ExecuteReader();
            if (mireader.HasRows == false)
            {
                NumFila = false;
            }
            else
            {
                NumFila = true;
            }
            mireader.Close();
            conexion.Close();
            CerrarConexion();

            return NumFila;
        }

        public void Ejecutar()
        {
            CerrarConexion();
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;
            cmd.ExecuteNonQuery();
            conexion.Close();
            CerrarConexion();
        }

        public void Borrar(string parametro1, int parametro2)
        {
            CerrarConexion();
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandText = CommandText;
            cmd.Parameters.AddWithValue("@" + parametro1 + "", SqlDbType.Int).Value = parametro2;
            cmd.ExecuteNonQuery();
            conexion.Close();
            CerrarConexion();
        }

        private void CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}