using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFarmaceutico.Modelo
{
    internal class DetalleCompraModelo
    {
        public int Cantidad { get; set; }
        public float precio { get; set; }
        public float subtotal { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }

        public int registrarCModelo(DetalleCompraModelo c)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarDetCompra", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                   
                    comando.Parameters.AddWithValue("@Cantidad", c.Cantidad);
                    comando.Parameters.AddWithValue("@PrecioU", c.precio);
                    comando.Parameters.AddWithValue("@SubTotal", c.subtotal);
                    comando.Parameters.AddWithValue("@IdCompra", c.IdCompra);
                    comando.Parameters.AddWithValue("@IdProducto", c.IdProducto);
                   
                    resultado = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Detalle de Compra " + e.Message);
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
    }
}
