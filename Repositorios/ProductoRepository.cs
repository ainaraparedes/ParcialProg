using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialProg;

namespace Clases.Repositorios
{
    public class ProductoRepository
    {
        
        public static void RegistrarProducto(Producto p)
        {
            ApplicationDbContext context = new ApplicationDbContext();
                context.Productos.Add(p);
                context.SaveChanges();

        }
        public static void ActualizarStock(int productoId, int nuevaCantidad)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var producto = context.Productos.FirstOrDefault(p => p.Id == productoId);
            if (producto != null)
            {
                producto.Stock = nuevaCantidad;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }
    }
}
