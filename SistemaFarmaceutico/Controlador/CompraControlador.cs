using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class CompraControlador
    {
        public static int registrarCCon(int CiP, int CiE, DateTime fecha, float total)
        {
            CompraModelo objeto = new CompraModelo();
            objeto.IdProveedor = CiP;
            objeto.IdEmpleado= CiE;
            objeto.fecha = fecha;
            objeto.Total = total;
            return objeto.registrarCModelo(objeto);
        }
    }
}
