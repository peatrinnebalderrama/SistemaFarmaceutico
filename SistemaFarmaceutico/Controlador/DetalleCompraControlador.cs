using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaFarmaceutico.Modelo;

namespace SistemaFarmaceutico.Controlador
{
    internal class DetalleCompraControlador
    {
        public static int registrarCmCon( int Cant, float p, float sub, int IdC, int IdP)
        {
            DetalleCompraModelo objeto = new DetalleCompraModelo();
           
            objeto.Cantidad = Cant;
            objeto.precio = p;
            objeto.subtotal = sub;
            objeto.IdCompra = IdC;
            objeto.IdProducto = IdP;
            return objeto.registrarCModelo(objeto);
        }
    }
}
