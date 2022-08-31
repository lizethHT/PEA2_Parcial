using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial.Data;
using Parcial.Dominio;

namespace Parcial.Logic
{
    public class ProductoBL
    {
        public static List<Producto> Listar()
        {
            var ProductoData = new ProductoData();
            return ProductoData.Listar();
        }

        public static Producto BuscarPolId(int id)
        {
            var productoData = new ProductoData();
            return productoData.BucarPorId(id);
        }

        public static bool Insertar(Producto producto)
        {
            var productoData = new ProductoData();
            return productoData.Insertar(producto);
        }

        public static bool Actualizar(Producto producto)
        {
            var productoData = new ProductoData();
            return productoData.Actualizar(producto);
        }

        public static bool Eliminar(int id)
        {
            var productoData = new ProductoData();
            return productoData.Eliminar(id);
        }
    }
}
