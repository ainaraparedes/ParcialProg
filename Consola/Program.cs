
using Clases.Repositorios;
using System.Runtime.CompilerServices;

namespace PracticaProgramacion
{
    public class Program
    {
        public static void Menu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("-----MENÜ-----");
                Console.WriteLine("1. Dar de alta un departamento.");
                Console.WriteLine("2. Dar de alta un empleado.");
                Console.WriteLine("3. Salir");
                Console.WriteLine("Por favor seleccione una opción: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            // Alta de departamento
                            Console.WriteLine("---ALTA DEPARTAMENTO---");
                            Console.WriteLine("Ingrese el nombre del departamento: ");
                            string deptoNombre = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(deptoNombre))
                            {
                                Departamento departamento_ = new Departamento
                                {
                                    Nombre = deptoNombre
                                };
                                DepartamentoRepository.AgregarDepartamento(departamento_);
                                Console.WriteLine("Departamento agregado exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("El nombre del departamento no puede estar vacío.");
                            }
                            break;

                        case 2:
                            // Alta de empleado
                            Console.WriteLine("---ALTA EMPLEADO---");
                            Console.WriteLine("Ingrese el nombre del empleado: ");
                            string empNombre = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(empNombre))
                            {
                                Console.WriteLine("El nombre del empleado no puede estar vacío.");
                                break;
                            }
                            Console.WriteLine("Ingrese el email del empleado: ");
                            string empEmail = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(empEmail))
                            {
                                Console.WriteLine("El email del empleado no puede estar vacío.");
                                break;
                            }
                            Console.WriteLine("Ingrese el salario del empleado: ");
                            string empSalario = Console.ReadLine();
                            if (!decimal.TryParse(empSalario, out decimal salario) || salario <= 0)
                            {
                                Console.WriteLine("Salario inválido. Debe ser un número positivo.");
                                break;
                            }
                            using (ApplicationDbConext context = new ApplicationDbConext())
                            {
                                var departamentos = context.Departamentos.ToList();

                                if (departamentos.Count == 0)
                                {
                                    Console.WriteLine("No hay departamentos disponibles. Por favor agregue un departamento primero.");
                                    return;
                                }

                                Console.WriteLine("Departamentos disponibles:");
                                foreach (var dept in departamentos)
                                {
                                    Console.WriteLine($"- {dept.Id}: {dept.Nombre}");
                                }

                                Console.WriteLine("Ingrese el ID del departamento al que pertenece el empleado:");
                                string deptoEmp = Console.ReadLine();

                                if (!int.TryParse(deptoEmp, out int idDepto))
                                {
                                    Console.WriteLine("ID inválido. Debe ser un número.");
                                    return;
                                }

                                var departamentoEmp = context.Departamentos.FirstOrDefault(d => d.Id == idDepto);

                                if (departamentoEmp == null)
                                {
                                    Console.WriteLine("Departamento no encontrado.");
                                    return;
                                }

                                Empleado empleado = new Empleado
                                {
                                    Nombre = empNombre,
                                    Email = empEmail,
                                    Salario = salario,
                                    DepartamentoId = departamentoEmp.Id
                                };

                                EmpleadoRepository.AgregarEmpleado(empleado);
                                Console.WriteLine("Empleado agregado exitosamente.");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Saliendo del programa.");
                            continuar = false; 
                            break;

                        default:
                            Console.WriteLine("Opción inválida. Por favor seleccione una opción válida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor ingrese un número.");
                }

                Console.WriteLine(); 
            }
        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}