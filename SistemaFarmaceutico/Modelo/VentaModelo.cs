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
    internal class VentaModelo
    {
        public int CiE { get; set; }
        public int CiC { get; set; }
        public DateTime fecha { get; set; }
        public float total { get; set; }

       
        public int registrarVtModelo(VentaModelo v)
        {
            int idGenerado = 0; 
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarVentas", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdEmpleado", v.CiE);
                    comando.Parameters.AddWithValue("@IdCliente", v.CiC);
                    comando.Parameters.AddWithValue("@fecha", v.fecha);
                    comando.Parameters.AddWithValue("@Total", v.total);

                    object resultado = comando.ExecuteScalar();

                    if (resultado != null)
                    {
                        idGenerado = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Venta: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) con.Close();
            }

            return idGenerado; 
        }

        public static float CalcularVentasDelDia()
        {
            float total = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("ObtenerTotalVentasHoy", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    object resultado = comando.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        total = Convert.ToSingle(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar el cierre de caja: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) con.Close();
            }
            return total;
        }
        public static DataTable ListarVenta()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null; // Inicializar fuera del try

            try
            {
                conexion = Conexion.ObtenerConexion();
               
                string consulta = "SELECT Id_nro_Venta,IdEmpleado,IdCliente, fecha,Total FROM Ventas";
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
