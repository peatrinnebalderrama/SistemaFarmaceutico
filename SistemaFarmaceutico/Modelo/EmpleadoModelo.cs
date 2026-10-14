using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaFarmaceutico.Vista;

namespace SistemaFarmaceutico.Modelo
{
    internal class EmpleadoModelo
    {
        public int ci { get; set; }
        public string nombre { get; set; }
        public string ap { get; set; }
        public string am { get; set; }
        public int telf { get; set; }
        public string dir { get; set; }

        public int registrarEmpleadoModelo(EmpleadoModelo E)
        {
            int resultado = 0;
            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                using (SqlCommand comando = new SqlCommand("GuardarEmpleado", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                   // @IdEmpleado int , @NombreEmp varchar(30),@Ap varchar(30),@Am varchar(30),@Telf int, @Dir varchar(40)
                    comando.Parameters.AddWithValue("@IdEmpleado", E.ci);
                    comando.Parameters.AddWithValue("@NombreEmp", E.nombre);
                    comando.Parameters.AddWithValue("@Ap", E.ap);
                    comando.Parameters.AddWithValue("@Am", E.am);
                    comando.Parameters.AddWithValue("@Telf", E.telf);
                    comando.Parameters.AddWithValue("@Dir", E.dir);
                    
                    resultado = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Registrar Empleado " + e.Message);
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

        public DataTable listarEmpMo()
        {
            DataTable dtCategorias = new DataTable();
            SqlConnection conexion = null; 

            try
            {
                conexion = Conexion.ObtenerConexion();

                string consulta = "SELECT IdEmpleado AS Ci,NombreEmp,Ap,Am,Telf,Dir FROM Empleado";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(dtCategorias);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al obtener lista de Empleados: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        public int ModificarEmp(EmpleadoModelo E)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {
                
                using (SqlCommand comando = new SqlCommand("modificarEmpleado", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdEmpleado", E.ci);
                    comando.Parameters.AddWithValue("@NombreEmp", E.nombre);
                    comando.Parameters.AddWithValue("@Ap", E.ap);
                    comando.Parameters.AddWithValue("@Am", E.am);
                    comando.Parameters.AddWithValue("@Telf", E.telf);
                    comando.Parameters.AddWithValue("@Dir", E.dir);


                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Modificar Empleado " + e.Message);
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

        public int EliminarEmp(EmpleadoModelo E)
        {
            int retorna = 0;

            SqlConnection con = Conexion.ObtenerConexion();
            try
            {

                using (SqlCommand comando = new SqlCommand("eliminarEmpleado", con))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdEmpleado", E.ci);
                    retorna = comando.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Eliminar Empleado " + e.Message);
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
            string query = "SELECT NombreEmp FROM Empleado WHERE IdEmpleado = @ci";
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
                MessageBox.Show("Error al obtener el nombre del usuario: " + e.Message);
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
