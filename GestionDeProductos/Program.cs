using System;

namespace GestionDeProductos
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            Console.WriteLine("Bienvenido al sistema de gestion de inventario. ");
            Console.WriteLine("¿Cuantos productos desea ingresar?\n");
            int cantidadProducto = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < cantidadProducto; i++)
            {
                Console.WriteLine($"Producto {i + 1}");
                string nombreProducto;
                do
                {
                    Console.Write("Nombre: ");
                    nombreProducto = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nombreProducto))
                    {
                        Console.WriteLine("El nombre no puede estar vacío.");
                    }
                } while (string.IsNullOrWhiteSpace(nombreProducto));

                decimal precioProducto;
                while (true)
                {
                    Console.Write("Precio: ");
                    try
                    {
                        precioProducto = Convert.ToDecimal(Console.ReadLine());
                        if (precioProducto <= 0) throw new FormatException();
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Ingrese un precio válido y positivo.");
                    }
                }


                Producto producto = new Producto(nombreProducto, precioProducto);
                inventario.AgregarProducto(producto);
            }

            Console.Write("\n¿Desea actualizar el precio de un producto? (s/n): ");
            if (Console.ReadLine().Trim().ToLower() == "s")
            {
                Console.Write("Ingrese el nombre del producto a actualizar: ");
                string nombre = Console.ReadLine();
                Console.Write("Ingrese el nuevo precio: ");
                try
                {
                    decimal nuevoPrecio = Convert.ToDecimal(Console.ReadLine());
                    if (nuevoPrecio <= 0) throw new FormatException();

                    if (inventario.ActualizarPrecioProducto(nombre, nuevoPrecio))
                    {
                        Console.WriteLine("Precio actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado.");
                    }
                }
                catch
                {
                    Console.WriteLine("Precio inválido.");
                }
            }

            Console.Write("\n¿Desea eliminar un producto? (s/n): ");
            if (Console.ReadLine().Trim().ToLower() == "s")
            {
                Console.Write("Ingrese el nombre del producto a eliminar: ");
                string nombreEliminar = Console.ReadLine();
                if (inventario.EliminarProductoPorNombre(nombreEliminar))
                {
                    Console.WriteLine("Producto eliminado.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }

            //Contar y agrupar
            inventario.ContarYAgruparProductos();


            decimal precioMinimo;
            while (true)
            {
                Console.Write("\nIngrese el precio minimo para filtrar los productos: ");
                try
                {
                    precioMinimo = Convert.ToDecimal(Console.ReadLine());
                    if (precioMinimo < 0) throw new FormatException();
                    break;
                }
                catch
                {
                    Console.WriteLine("Por favor, ingrese un precio mínimo válido y positivo.");
                }
            }


            var productosFiltrados = inventario.FiltrarYordenarProductos(precioMinimo);
            foreach (var producto in productosFiltrados)
            {
                producto.MostrarInformacion();
            }

            //Reporte
            inventario.GenerarReporteInventario();

        }
    }
}