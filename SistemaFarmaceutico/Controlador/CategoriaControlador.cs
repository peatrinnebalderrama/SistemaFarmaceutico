using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;
using SistemaFarmaceutico.Vista;

namespace SistemaFarmaceutico.Controlador
{
    internal class CategoriaControlador
    {
        public static DataTable Refresh()
        {

            return new CategoriaModelo().ListarCatg();

        }

        public static int modificarCatCon(int id, string n, string d)
        {
            CategoriaModelo objeto = new CategoriaModelo();
            objeto.IdCat = id;
            objeto.nombre = n;
            objeto.descripcion = d;
            
            return objeto.ModificarCat(objeto);
        }
        public static int eliminarCatgCon(int id, string n, string d)
        {
            CategoriaModelo objeto = new CategoriaModelo();
            objeto.IdCat = id;
            objeto.nombre = n;
            objeto.descripcion = d;

            return objeto.EliminarCat(objeto);
        }
    }
}
