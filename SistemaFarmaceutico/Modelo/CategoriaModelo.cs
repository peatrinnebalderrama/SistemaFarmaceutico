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
    internal class CategoriaModelo
    {
        public int IdCat { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public CategoriaModelo() { }

        public static int AgregarCategoria(int id, string nombre, string descripcion)
        {
            int retorna = 0;
            SqlConnection con = Conexion.ObtenerConexion();

            string query = string.Format("EXEC GuardarCategoria {0},'{1}','{2}' ", id, nombre, descripcion);
            SqlCommand comando = new SqlCommand(query, con);
            retorna = comando.ExecuteNonQuery();
            con.Close();

            return retorna;
        }

        public DataTable ListarCatg()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null; // Inicializar fuera del try

            try
            {
                conexion = Conexion.ObtenerConexion();

                string consulta = "SELECT IdCatg, NombreCat, Descripcion FROM Categoria";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtCategorias);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al obtener productos: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        public int ModificarCat(CategoriaModelo C)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("modificarCategoria", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdCatg",C.IdCat);
                    comando.Parameters.AddWithValue("@NombreCat", C.nombre);
                    comando.Parameters.AddWithValue("@Descripcion", C.descripcion);
                   
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Modificar Categoria " + e.Message);
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

        public int EliminarCat(CategoriaModelo C)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("eliminarCategoria", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdCatg", C.IdCat);
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Eliminar Categoria " + e.Message);
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
    }
}
