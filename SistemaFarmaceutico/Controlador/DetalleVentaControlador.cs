using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class DetalleVentaControlador
    {
        public static int registrarDVCon(int IdP, int Cant, float p,float sub, int IdV)
        {
            DetallaVentaModelo objeto = new DetallaVentaModelo();
            objeto.IdProd = IdP;
            objeto.cant = Cant;
            objeto.precio = p;
            objeto.subtotal = sub;
            objeto.IdVenta = IdV;
            return objeto.registrarDVModelo(objeto);
        }
    }
}
