using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace SistemaFarmaceutico.Controlador
{
    internal class UsuarioControlador
    {
        public static int registrarUCon(string u, string con, string cargo, int ci)
        {
            UsuarioModelo objeto = new UsuarioModelo();
            objeto.usuario = u;
            objeto.password = con;
            objeto.cargo= cargo;
            objeto.ci = ci;
            return objeto.registrarUModelo(objeto);
        }
        public static int modificarUCon(string u, string con, string cargo, int ci)
        {
            UsuarioModelo objeto = new UsuarioModelo();
            objeto.usuario = u;
            objeto.password = con;
            objeto.cargo = cargo;
            objeto.ci = ci;

            return objeto.modificarU(objeto);
        }
        public static int eliminarUCon(string u, string con, string cargo, int ci)
        {
            UsuarioModelo objeto = new UsuarioModelo();
            objeto.usuario = u;
            objeto.password = con;
            objeto.cargo = cargo;
            objeto.ci = ci;

            return objeto.eliminarU(objeto);
        }
        public static DataTable listarUCon()
        {
            return new UsuarioModelo().listarUMo();
        }
    }
}
