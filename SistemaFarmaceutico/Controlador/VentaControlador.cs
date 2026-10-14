using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class VentaControlador
    {
        public static int registrarVCon(int CiE, int CiC, DateTime fecha, float total)
        {
            VentaModelo objeto = new VentaModelo();
            objeto.CiE = CiE;
            objeto.CiC = CiC;
            objeto.fecha = fecha;
            objeto.total = total;
            return objeto.registrarVtModelo(objeto);
        }
    }
}
