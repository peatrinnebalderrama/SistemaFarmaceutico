using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class EmpleadoControlador
    {
        public static int registrarEmpCon(int ci,string n, string ap, string am,int telf, string dir)
        {
            EmpleadoModelo objeto = new EmpleadoModelo();
            objeto.ci = ci;
            objeto.nombre = n;
            objeto.ap = ap;
            objeto.am = am;
            objeto.telf = telf;
            objeto.dir = dir;
            return objeto.registrarEmpleadoModelo(objeto);
        }

        public static int modificarEmpCon(int ci, string n, string ap, string am, int telf, string dir)
        {
            EmpleadoModelo objeto = new EmpleadoModelo();
            objeto.ci = ci;
            objeto.nombre = n;
            objeto.ap = ap;
            objeto.am = am;
            objeto.telf = telf;
            objeto.dir = dir;
            return objeto.ModificarEmp(objeto);
        }

        public static int eliminarEmpCon(int ci, string n, string ap, string am, int telf, string dir)
        {
            EmpleadoModelo objeto = new EmpleadoModelo();
            objeto.ci = ci;
            objeto.nombre = n;
            objeto.ap = ap;
            objeto.am = am;
            objeto.telf = telf;
            objeto.dir = dir;
            return objeto.EliminarEmp(objeto);
        }

        public static DataTable listarEmpCon()
        {
            return new EmpleadoModelo().listarEmpMo();
        }
    }
}
