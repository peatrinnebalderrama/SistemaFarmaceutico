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
    internal class CompraModelo
    {
        public int IdProveedor { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime fecha { get; set; }
        public float Total { get; set; }

        public int registrarCModelo(CompraModelo c)
        {
            int idGenerado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarCompra", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdProveedor", c.IdProveedor);
                    comando.Parameters.AddWithValue("@IdEmpleado", c.IdEmpleado);
                    comando.Parameters.AddWithValue("@fecha", c.fecha);
                    comando.Parameters.AddWithValue("@Total", c.Total);

                    object resultado = comando.ExecuteScalar();

                    if (resultado != null)
                    {
                        idGenerado = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Compra: " + e.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) con.Close();
            }

            return idGenerado;
        }
    }
}
