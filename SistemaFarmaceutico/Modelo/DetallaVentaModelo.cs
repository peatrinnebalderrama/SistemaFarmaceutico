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
    internal class DetallaVentaModelo
    {
        public int IdProd { get; set; }
        public int cant { get; set; }
        public float precio { get; set; }
        public float subtotal { get; set; }
        public int IdVenta { get; set; }

        public int registrarDVModelo(DetallaVentaModelo v)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarDetVentas", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdProducto", v.IdProd);
                    comando.Parameters.AddWithValue("@Cantidad", v.cant);
                    comando.Parameters.AddWithValue("@PrecioU", v.precio);
                    comando.Parameters.AddWithValue("@SubTotal", v.subtotal);
                    comando.Parameters.AddWithValue("@IdVenta", v.IdVenta);
                  
                    resultado = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Detalle de Venta " + e.Message);
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

        public static DataTable ListarDVenta()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null; // Inicializar fuera del try

            try
            {
                conexion = Conexion.ObtenerConexion();

                //string consulta = "SELECT IdDetalleV,IdProducto,Cantidad,PrecioU, SubTotal FROM DetalleVenta";
                string consulta = @"SELECT d.IdDetalleV, 
                                   p.Nombre AS Producto, 
                                   d.Cantidad, 
                                   d.PrecioU, 
                                   d.SubTotal 
                            FROM DetalleVenta d
                            INNER JOIN Producto p ON d.IdProducto = p.IdProd";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtCategorias);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al obtener ventas: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
    }
}
