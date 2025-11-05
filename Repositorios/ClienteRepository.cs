using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialProg;

namespace Clases.Repositorios
{
    public class ClienteRepository
    {
        public static void RegistrarCliente(Cliente c)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Clientes.Add(c);
            context.SaveChanges();
        }
        public static void BuscarCliente(int dni)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var cliente = context.Clientes.FirstOrDefault(cli => cli.Dni == dni);
            if (cliente != null)
            {
                Console.WriteLine($"Cliente encontrado: {cliente.Nombre} {cliente.Apellido}, DNI: {cliente.Dni}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
    }
}