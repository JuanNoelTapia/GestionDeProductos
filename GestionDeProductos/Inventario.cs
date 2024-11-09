using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeProductos
{
    public class Inventario
    {
        private List<Producto> productos;
        public Inventario()
        {
            productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }
        public IEnumerable<Producto> FiltrarYordenarProductos(decimal precioMinimo)
        {
            return productos.Where(p => p.Precio > precioMinimo).OrderBy(p => p.Precio);
        }
        public bool ActualizarPrecioProducto(string nombre, decimal nuevoPrecio)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                return true;
            }
            return false;
        }
        public bool EliminarProductoPorNombre(string nombre)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                productos.Remove(producto);
                return true;
            }
            return false;
        }
        public void ContarYAgruparProductos()
        {
            int menor500 = productos.Count(p => p.Precio < 500);
            int entre500Y1000 = productos.Count(p => p.Precio >= 500 && p.Precio <= 1000);
            int mayor1000 = productos.Count(p => p.Precio > 1000);

            Console.WriteLine("\nProductos por rango de precios:");
            Console.WriteLine($"Menores a 500 C$: {menor500}");
            Console.WriteLine($"Entre 500 y 1000 C$: {entre500Y1000}");
            Console.WriteLine($"Mayores a 1000 C$: {mayor1000}");
        }

        public void GenerarReporteInventario()
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos en el inventario.");
                return;
            }

            var totalProductos = productos.Count;
            var precioPromedio = productos.Average(p => p.Precio);
            var productoMasCaro = productos.OrderByDescending(p => p.Precio).First();
            var productoMasBarato = productos.OrderBy(p => p.Precio).First();

            Console.WriteLine("\nReporte del Inventario:");
            Console.WriteLine($"Número total de productos: {totalProductos}");
            Console.WriteLine($"Precio promedio: {precioPromedio} C$");
            Console.WriteLine($"Producto más caro: {productoMasCaro.Nombre}, Precio: {productoMasCaro.Precio} C$");
            Console.WriteLine($"Producto más barato: {productoMasBarato.Nombre}, Precio: {productoMasBarato.Precio} C$");
        }

    }
}
