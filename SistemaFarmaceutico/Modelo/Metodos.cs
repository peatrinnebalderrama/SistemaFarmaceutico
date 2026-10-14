using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaFarmaceutico.Modelo;
using SistemaFarmaceutico.Vista;


namespace SistemaFarmaceutico
{
    internal class Metodos
    {

        public static int AgregarProducto(Producto p)
        {
            int retorna = 0;

            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("guardarProducto", con))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdPro", p.Id);
                comando.Parameters.AddWithValue("@IdCat", p.Idcategoria);
                comando.Parameters.AddWithValue("@nombre", p.nombre);
                comando.Parameters.AddWithValue("@precio", p.precio);
                comando.Parameters.AddWithValue("@fecha", p.fechaVenc);

                // FOTO → convertir a byte[]
                byte[] imagenBytes = FrmLogin.ImageToByteArray(p.foto);
                comando.Parameters.Add("@foto", SqlDbType.VarBinary).Value = imagenBytes;

                retorna = comando.ExecuteNonQuery();
            }

            return retorna;
        }

        public static int ModificarProducto(Producto p)
        {
            int retorna = 0;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                // Asegúrate que el nombre coincida EXACTAMENTE con tu script SQL
                using (SqlCommand comando = new SqlCommand("modificarProducto", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // PARÁMETROS
                    // El error más común es olvidar este, que es el WHERE del update
                    comando.Parameters.AddWithValue("@IdProd", p.Id);

                    comando.Parameters.AddWithValue("@IdCat", p.Idcategoria);
                    comando.Parameters.AddWithValue("@nombre", p.nombre);
                    comando.Parameters.AddWithValue("@precio", p.precio);
                    comando.Parameters.AddWithValue("@fecha", p.fechaVenc);

                    // Manejo de la FOTO
                    if (p.foto != null)
                    {
                        byte[] imagenBytes = FrmLogin.ImageToByteArray(p.foto);
                        comando.Parameters.Add("@foto", SqlDbType.VarBinary).Value = imagenBytes;
                    }
                    else
                    {
                        // Si la foto es nula o vacía, enviamos DBNull
                        comando.Parameters.Add("@foto", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // Ejecutamos
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retorna;
        }


        public static List<CategoriaModelo> CargaComboBox()
        {
            List<CategoriaModelo> lista = new List<CategoriaModelo>();
            SqlConnection con = Conexion.ObtenerConexion();
            string sql = "select IdCatg, NombreCat  From Categoria";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CategoriaModelo x = new CategoriaModelo();
                x.IdCat = reader.GetInt32(0);
                x.nombre = reader.GetString(1);

                lista.Add(x);
            }
            return lista;
        }


        public static DataTable ObtenerProductos()
        {
            DataTable dtProductos = new DataTable();
            SqlConnection conexion = null; // Inicializar fuera del try

            try
            {
                 conexion = Conexion.ObtenerConexion();

                string consulta = "SELECT IdProd,IdCategoria, Nombre, precio, fechaV FROM Producto";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtProductos);
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
            return dtProductos;
        }

        public static Image ObtenerImagenProductoSP(int idProducto)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("obtenerFotoProducto", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProd", idProducto);

                object result = cmd.ExecuteScalar(); // devuelve la primera fila, primera columna
                if (result != DBNull.Value && result != null)
                {
                    byte[] imagenBytes = (byte[])result;
                    using (MemoryStream ms = new MemoryStream(imagenBytes))
                    {
                        return Image.FromStream(ms);
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public static int EliminarProducto(Producto p)
        {
            int retorna = 0;

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("eliminarProducto", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    
                    comando.Parameters.AddWithValue("@nombre", p.nombre);

                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }

            return retorna;
        }

        public static DataTable listarPC1(int idCategoria)
        {
            DataTable dtProductos = new DataTable(); 
            SqlConnection conexion = null;

            try
            {
                conexion = Conexion.ObtenerConexion();
                string consulta = "SELECT IdProd, IdCategoria, Nombre, Precio, FechaV FROM Producto WHERE IdCategoria = @IdCategoria";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dtProductos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener lista de Productos: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return dtProductos;
        }
    }
}
    
        






    

