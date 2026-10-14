using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFarmaceutico
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection("Data Source=PEATRINNE1\\SQLEXPRESS\r\n;Initial Catalog=SistemaFarmaceutico;Integrated Security=True;TrustServerCertificate=True");
            conexion.Open();

            return conexion;
        }
    }
}
