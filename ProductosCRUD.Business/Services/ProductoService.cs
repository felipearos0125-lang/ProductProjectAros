using ProductosCRUD.Data.Models;
using ProductosCRUD.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosCRUD.Business.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repository;

        public ProductoService(ProductoRepository repository)
        {
            _repository = repository;
        }

        // Constructor sin parámetros para compatibilidad con Windows Forms
        public ProductoService()
        {
            _repository = new ProductoRepository();
        }

        public async Task<List<Producto>> ObtenerTodosLosProductos()
        {
            return await _repository.ObtenerTodos();
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero");

            return await _repository.ObtenerPorId(id);
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            // Validaciones de negocio
            ValidarProducto(producto);

            return await _repository.Agregar(producto);
        }

        public async Task<Producto> ActualizarProducto(Producto producto)
        {
            if (producto.Id <= 0)
                throw new ArgumentException("El ID del producto es inválido");

            ValidarProducto(producto);

            return await _repository.Actualizar(producto);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero");

            return await _repository.Eliminar(id);
        }

        public async Task<List<Producto>> BuscarProductos(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return await ObtenerTodosLosProductos();

            return await _repository.Buscar(termino);
        }

        private void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio");

            if (producto.Nombre.Length > 100)
                throw new ArgumentException("El nombre no puede exceder 100 caracteres");

            if (producto.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo");

            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo");
        }
    }
}