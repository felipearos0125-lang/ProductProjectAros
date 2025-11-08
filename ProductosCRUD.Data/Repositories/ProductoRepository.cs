using Microsoft.EntityFrameworkCore;
using ProductosCRUD.Data.Context;
using ProductosCRUD.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductosCRUD.Data.Repositories
{
    public class ProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // Constructor sin parámetros para compatibilidad con Windows Forms
        public ProductoRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=productos.db");
            _context = new AppDbContext(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            return await _context.Productos
                .Where(p => p.Activo)
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> Agregar(Producto producto)
        {
            producto.FechaCreacion = System.DateTime.Now;
            producto.Activo = true;
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> Actualizar(Producto producto)
        {
            // Obtener el producto existente para preservar campos que no deben cambiar
            var productoExistente = await _context.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == producto.Id);
            
            if (productoExistente == null)
            {
                throw new ArgumentException($"Producto con ID {producto.Id} no encontrado");
            }
            
            // Preservar la fecha de creación original
            producto.FechaCreacion = productoExistente.FechaCreacion;
            producto.Activo = productoExistente.Activo;
            
            // Desconectar cualquier instancia rastreada del mismo producto
            var tracked = _context.Productos.Local.FirstOrDefault(p => p.Id == producto.Id);
            if (tracked != null)
            {
                _context.Entry(tracked).State = EntityState.Detached;
            }
            
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> Eliminar(int id)
        {
            var producto = await ObtenerPorId(id);
            if (producto != null)
            {
                producto.Activo = false; // Eliminación lógica
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Producto>> Buscar(string termino)
        {
            return await _context.Productos
                .Where(p => p.Activo &&
                       (p.Nombre.Contains(termino) ||
                        p.Descripcion.Contains(termino)))
                .ToListAsync();
        }
    }
}