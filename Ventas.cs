using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialProg
{
    public class Ventas
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public Producto Producto { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
