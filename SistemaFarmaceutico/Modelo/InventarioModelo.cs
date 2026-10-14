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
    internal class InventarioModelo
    {
        public int IdP { get; set; }
        public int Stock { get; set; }

        public int registrarInModelo(InventarioModelo i)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarInvetario", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", i.IdP);
                    comando.Parameters.AddWithValue("@Stock", i.Stock);
                    object res = comando.ExecuteScalar();
                    if (res != null)
                    {
                        resultado = Convert.ToInt32(res);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar el Item " + e.Message);
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
        public DataTable listarInMo()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null;

            try
            {
                conexion = Conexion.ObtenerConexion();

                string consulta = "SELECT IdProducto AS Item,Stock FROM Inventario";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtCategorias);
               
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al obtener lista del Inventario: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        public int modificarInv(InventarioModelo i)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("modificarInventario", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdProducto", i.IdP);
                    comando.Parameters.AddWithValue("@Stock", i.Stock);

                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Modificar Item del Inventario " + e.Message);
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

        public int eliminarInv(InventarioModelo i)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("eliminarInventario", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdProducto", i.IdP);
                    
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Eliminar Item del Inventario " + e.Message);
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

        public static bool DescontarStock(int idProd, int cantidad)
        {
            bool exito = false;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("descontarStock", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", idProd);
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);

                    object res = comando.ExecuteScalar();
                    if (res != null && Convert.ToInt32(res) == 1)
                    {
                        exito = true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al verificar inventario: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) con.Close();
            }
            return exito;
        }

        public static void RestaurarStock(int idProd, int cantidad)
        {
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("restaurarStock", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", idProd);
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al restaurar stock: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) con.Close();
            }
        }
        public static bool DescontarStock(int idProd, int cantidad, out bool stockBajo)
        {
            bool exito = false;
            stockBajo = false; 
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("RestablecerStock", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", idProd);
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);

                    object res = comando.ExecuteScalar();

                    if (res != null)
                    {
                        int stockRestante = Convert.ToInt32(res);

                        if (stockRestante >= 0) 
                        {
                            exito = true;
                            if (stockRestante <= 2)
                            {
                                stockBajo = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al actualizar inventario: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) con.Close();
            }
            return exito;
        }
    }
}
