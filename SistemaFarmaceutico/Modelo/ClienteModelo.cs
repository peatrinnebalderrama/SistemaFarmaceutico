using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFarmaceutico.Modelo
{
    internal class ClienteModelo
    {
        public int ci { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }

        public int registrarClienteModelo(ClienteModelo C)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarCliente", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Ci", C.ci);
                    comando.Parameters.AddWithValue("@nombre", C.nombre);
                    comando.Parameters.AddWithValue("@telefono", C.telefono);
                    resultado = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Cliente " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return resultado;
        }
        public DataTable listarCliMo()
        {
            DataTable dtClientes = new DataTable();
            SqlConnection conexion = null;
            try
            {
                conexion = Conexion.ObtenerConexion();
                string consulta = "SELECT  Ci,nombre,telefono FROM Cliente";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtClientes);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener lista de Clientes: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return dtClientes;
        }

        public int ModificarCli(ClienteModelo C)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("modificarCliente", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Ci", C.ci);
                    comando.Parameters.AddWithValue("@nombre", C.nombre);
                    comando.Parameters.AddWithValue("@telefono", C.telefono);
                    retorna = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Modificar Cliente " + e.Message);
            }
            finally
            {

                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return retorna;
        }
        public int EliminarCli(ClienteModelo C)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("eliminarCli", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Ci", C.ci);
                    retorna = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al eliminar Cliente " + e.Message);
            }
            finally
            {

                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return retorna;
        }
        public static string ObtenerNombreUsuario(int ci)
        {
            string nombre = "";
            SqlConnection con = Conexion.ObtenerConexion();
            string query = "SELECT nombre FROM Cliente WHERE Ci= @ci";
            try
            {
                using (SqlCommand comando = new SqlCommand(query, con))
                {
                    comando.Parameters.AddWithValue("@ci", ci);
                    object resultadoScalar = comando.ExecuteScalar();

                    if (resultadoScalar != null && resultadoScalar != DBNull.Value)
                    {
                        nombre = resultadoScalar.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al obtener el nombre del cliente: " + e.Message);
                nombre = "";
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return nombre;
        }
    }
}
