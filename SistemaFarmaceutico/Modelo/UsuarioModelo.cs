using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace SistemaFarmaceutico.Modelo
{
    internal class UsuarioModelo
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public string cargo { get; set; }
        public int ci { get; set; }

        public int registrarUModelo(UsuarioModelo u)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarUsuario", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    
                    comando.Parameters.AddWithValue("@usuario", u.usuario);
                    comando.Parameters.AddWithValue("@pasword", u.password);
                    comando.Parameters.AddWithValue("@cargo", u.cargo);
                    comando.Parameters.AddWithValue("@IdEmpleado", u.ci);
                    
                    resultado = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Usuario " + e.Message);
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

        public DataTable listarUMo()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null;

            try
            {
                conexion = Conexion.ObtenerConexion();

                string consulta = "SELECT IdEmpleado AS CI,usuario,pasword,cargo FROM Usuario";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtCategorias);

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al obtener lista de usuarios: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return dtCategorias;
        }
        public int modificarU(UsuarioModelo u)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("modificarUsuario", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@usuario", u.usuario);
                    comando.Parameters.AddWithValue("@pasword", u.password);
                    comando.Parameters.AddWithValue("@cargo", u.cargo);
                    comando.Parameters.AddWithValue("@IdEmpleado", u.ci);

                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Modificar Usuario " + e.Message);
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
        public int eliminarU(UsuarioModelo u)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("eliminarUsuario", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdEmpleado", u.ci);
                    retorna = comando.ExecuteNonQuery();
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Eliminar Usuario " + e.Message);
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
       

        public static string VerificarUsuario(string usuario, string password)
        {
            string cargoUsuario = "";
            SqlConnection con = Conexion.ObtenerConexion();
            string query = "SELECT cargo FROM Usuario WHERE usuario = @user AND pasword = @pass";

            try
            {
                using (SqlCommand comando = new SqlCommand(query, con))
                {
                    comando.Parameters.AddWithValue("@user", usuario);
                    comando.Parameters.AddWithValue("@pass", password);
                    object resultadoScalar = comando.ExecuteScalar();

                    if (resultadoScalar != null && resultadoScalar != DBNull.Value)
                    {
                        cargoUsuario = resultadoScalar.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al verificar el usuario: " + e.Message);
                cargoUsuario = ""; 
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return cargoUsuario;
        }
        public static int ObtenerCiUsuario(string usuario)
        {
            int ciUsuario = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            string query = "SELECT IdEmpleado FROM Usuario WHERE usuario = @user";
            try
            {
                using (SqlCommand comando = new SqlCommand(query, con))
                {
                    comando.Parameters.AddWithValue("@user", usuario);
                    object resultadoScalar = comando.ExecuteScalar();

                    if (resultadoScalar != null && resultadoScalar != DBNull.Value)
                    {
                        ciUsuario = Convert.ToInt32(resultadoScalar);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al obtener el CI del usuario: " + e.Message);
                ciUsuario = 0;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ciUsuario;
        }
    }
}
