using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class InventarioControlador
    {
        public static int registrarInCon(int IdP, int stock)
        {
            InventarioModelo objeto = new InventarioModelo();
            objeto.IdP = IdP;
            objeto.Stock = stock;
          
            return objeto.registrarInModelo(objeto);
        }

        public static DataTable listarInCon()
        {
            return new InventarioModelo().listarInMo();
        }

        public static int modificarInCon(int IdP, int stock)
        {
            InventarioModelo objeto = new InventarioModelo();
            objeto.IdP = IdP;
            objeto.Stock = stock;


            return objeto.modificarInv(objeto);
        }

        public static int eliminarInCon(int IdP, int stock)
        {
            InventarioModelo objeto = new InventarioModelo();
            objeto.IdP = IdP;
            objeto.Stock = stock;

            return objeto.eliminarInv(objeto);
        }

    }
}
