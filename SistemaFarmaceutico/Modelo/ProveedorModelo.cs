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
    internal class ProveedorModelo
    {
        public int idProv { get; set; }
        public string nomprov { get; set; }
        public string correo { get; set; }
        public string dir { get; set; }
        public int tel { get; set; }

        public int registrarProvModelo( ProveedorModelo p)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("guardarProveedor", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    //@IdProvd int , @NombreProv varchar(40),@Correo varchar(30),@Area varchar(40), @telefono INT
                    comando.Parameters.AddWithValue("@IdProvd", p.idProv);
                    comando.Parameters.AddWithValue("@NombreProv", p.nomprov);
                    comando.Parameters.AddWithValue("@Correo", p.correo);
                    comando.Parameters.AddWithValue("@Area", p.dir);
                    comando.Parameters.AddWithValue("@telefono",p.tel);
                    resultado = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Proveedor " + e.Message);
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

        public DataTable listarProvMo()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null;

            try
            {
                conexion = Conexion.ObtenerConexion();

                string consulta = "SELECT IdProvd AS Id,NombreProv,Correo,Area,Telf FROM Proveedor";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtCategorias);

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al obtener lista de Proveedores: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        public int modificarProv(ProveedorModelo p)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("modificarProveedor", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdProvd", p.idProv);
                    comando.Parameters.AddWithValue("@NombreProv", p.nomprov);
                    comando.Parameters.AddWithValue("@Correo", p.correo);
                    comando.Parameters.AddWithValue("@Area", p.dir);
                    comando.Parameters.AddWithValue("@telefono", p.tel);
                    
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Modificar Proveedor " + e.Message);
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

        public int eliminarProv(ProveedorModelo p)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("eliminarProveedor", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdProvd", p.idProv);
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Eliminar Proveedor " + e.Message);
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
