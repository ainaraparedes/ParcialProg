using Clases.Repositorios;
using Microsoft.EntityFrameworkCore;
using ParcialProg;
public class Program
{
    public static void Menu()
    {
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("---MENÚ---");
            Console.WriteLine("1. Registrar un nuevo producto.");
            Console.WriteLine("2. Registrar un nuevo cliente.");
            Console.WriteLine("3. Registrar una nueva venta.");
            Console.WriteLine("4. Mostrar un reporte de venta.");
            Console.WriteLine("5. Salir");
            string entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int option))
            {
                switch (option)
                {
                    case 1:
                        RegistrarProducto();
                        Console.WriteLine("Producto registrado con éxito");
                        break;
                    case 2:
                        RegistrarCliente();
                        Console.WriteLine("Cliente registrado con éxito");
                        break;
                    case 3:
                        RegistrarVenta();
                        Console.WriteLine("Venta registrada con éxito");
                        break;
                    case 4:
                        ApplicationDbContext context = new ApplicationDbContext();
                        var ventas = from v in context.Ventas
                                     join c in context.Clientes on v.ClienteId equals c.Id
                                     join p in context.Productos on v.ProductoId equals p.Id
                                     select new
                                     {
                                         ClienteNombre = c.Nombre,
                                         ProductoNombre = p.Nombre,
                                         v.Cantidad,
                                         TotalPrecio = v.Cantidad * p.Precio
                                     };
                        Console.WriteLine("REPORTES DE VENTAS");
                        foreach (var venta in ventas)
                        {
                            Console.WriteLine($"Nombre del cliente: { venta.ClienteNombre}, Nombre del producto: {venta.ProductoNombre}, Cantidad vendida: {venta.Cantidad}, Total de venta: {venta.TotalPrecio}");
                        }

                            break;
                    case 5:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida. Por favor ingrese un número.");
            }
        }
    }
    public static void RegistrarProducto()
    {
        Console.WriteLine("REGISTRAR NUEVO PRODUCTO");
        Console.Write("Ingrese el nombre del producto: ");
        string nombre = Console.ReadLine();
        Console.Write("Ingrese el precio del producto: ");
        decimal precio = decimal.Parse(Console.ReadLine());
        Console.Write("Ingrese el stock del producto: ");
        int stock = int.Parse(Console.ReadLine());
        Producto p = new Producto
        {
            Nombre = nombre,
            Precio = precio,
            Stock = stock
        };
        Clases.Repositorios.ProductoRepository.RegistrarProducto(p);

    }
    public static void RegistrarCliente()
    {
        Console.WriteLine("REGISTRAR NUEVO CLIENTE");
        Console.Write("Ingrese el dni del cliente: ");
        int dni = int.Parse(Console.ReadLine());
        if (dni <= 0)
        {
            Console.WriteLine("El DNI debe ser un número positivo.");
            return;
        }
        Console.Write("Ingrese el nombre del cliente: ");
        string nombre = Console.ReadLine();
        if (string.IsNullOrEmpty(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }
        Console.WriteLine("Ingrese el apellido del cliente: ");
        string apellido = Console.ReadLine();
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
        {
            Console.WriteLine("El nombre y apellido no pueden estar vacíos.");
            return;
        }
        Cliente c = new Cliente
        {
            Dni = dni,
            Nombre = nombre,
            Apellido = apellido
        };
        Clases.Repositorios.ClienteRepository.RegistrarCliente(c);
    }
    public static void RegistrarVenta()
    {
        Console.WriteLine("REGISTRAR NUEVA VENTA");
        ListarClientes();   
        Console.Write("Ingrese el Id del cliente");
        int clienteId = int.Parse(Console.ReadLine());
        Console.Write("Productos dísponibles: ");
        ListarProductos();
        Console.WriteLine("Ingresar id del producto: ");
        int idProducto = int.Parse(Console.ReadLine());
        Console.Write("Ingrese la cantidad a vender: ");
        int cantidad = int.Parse(Console.ReadLine());
        var context = new ApplicationDbContext();

        Ventas venta = new Ventas
        {
            ClienteId = clienteId,
            ProductoId = idProducto,
            Cantidad = cantidad
        };
        VentaRepository.RegistrarVenta(venta);
        ProductoRepository.ActualizarStock(idProducto, cantidad);
    }

    public static void ListarClientes()
    {
        ApplicationDbContext context = new ApplicationDbContext();
        var clientes = context.Clientes.ToList();
        foreach (var c in clientes)
        {
            Console.WriteLine($"{c.Id}  -  {c.Nombre}");
        }
    }
    public static void ListarProductos()
    {
        ApplicationDbContext context = new ApplicationDbContext();
        var productos = context.Productos.ToList();
        foreach (var producto in productos)
        {
            Console.WriteLine($"{producto.Id}  -   {producto.Nombre}");
        }
    }
    public static void Main(string[] args)
    {
        Menu();
    }
}
