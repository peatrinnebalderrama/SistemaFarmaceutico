using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class ClienteControlador
    {
        public static int registrarCliCon(int ci, string n, string tel)
        {
            ClienteModelo objeto = new ClienteModelo();
            objeto.ci = ci;
            objeto.nombre = n;
            objeto.telefono = tel;
            
            return objeto.registrarClienteModelo(objeto);
        }

        public static DataTable listarCliCon()
        {
            return new ClienteModelo().listarCliMo();
        }
        public static int modificarCliCon(int ci, string n, string tel)
        {
            ClienteModelo objeto = new ClienteModelo();
            objeto.ci = ci;
            objeto.nombre = n;
            objeto.telefono = tel;

            return objeto.ModificarCli(objeto);
        }
        public static int eliminarCliCon(int ci, string n, string tel)
        {
            ClienteModelo objeto = new ClienteModelo();
            objeto.ci = ci;
            objeto.nombre = n;
            objeto.telefono = tel;

            return objeto.EliminarCli(objeto);
        }
    }
}
