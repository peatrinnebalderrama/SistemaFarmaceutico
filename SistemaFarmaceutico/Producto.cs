using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFarmaceutico
{
    public class Producto
    {
        public int Id { get; set; }
       
        public int Idcategoria { get; set; }
        public string nombre { get; set; }

        public float precio { get; set; }
        public DateTime fechaVenc { get; set; }
       public Image foto { get; set; }
        public Producto()
        {
           
        }

        
    }
}
