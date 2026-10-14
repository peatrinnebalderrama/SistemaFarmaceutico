using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class ProveedorControlador
    {
        public static int registrarProvCon(int ci, string n, string co, string dir, int telf)
        {
            ProveedorModelo objeto = new ProveedorModelo();
            objeto.idProv = ci;
            objeto.nomprov = n;
            objeto.correo = co;
            objeto.dir = dir;
            objeto.tel = telf;
           
            return objeto.registrarProvModelo(objeto);
        }

        public static int modificarProvCon(int ci, string n, string co, string dir, int telf)
        {
            ProveedorModelo objeto = new ProveedorModelo();
            objeto.idProv = ci;
            objeto.nomprov = n;
            objeto.correo = co;
            objeto.dir = dir;
            objeto.tel = telf;

            return objeto.modificarProv(objeto);
        }

        public static int eliminarProvCon(int ci, string n, string co, string dir, int telf)
        {
            ProveedorModelo objeto = new ProveedorModelo();
            objeto.idProv = ci;
            objeto.nomprov = n;
            objeto.correo = co;
            objeto.dir = dir;
            objeto.tel = telf;

            return objeto.eliminarProv(objeto);
        }

        public static DataTable listarProvCon()
        {
            return new ProveedorModelo().listarProvMo();
        }
    }
}
