using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialProg;  

namespace Clases.Repositorios
{
    public class VentaRepository
    {
        public static void RegistrarVenta(Ventas v)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Ventas.Add(v);
            context.SaveChanges();
        }
    }
}
