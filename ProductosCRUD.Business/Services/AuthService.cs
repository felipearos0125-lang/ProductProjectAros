using System.Security.Cryptography;
using System.Text;
using ProductosCRUD.Data.Models;
using ProductosCRUD.Data.Repositories;

namespace ProductosCRUD.Business.Services
{
    public class AuthService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public AuthService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Registrar un nuevo usuario
        public async Task<(bool Success, string Message, Usuario? Usuario)> RegistrarAsync(
            string nombreUsuario, 
            string password)
        {
            // Validar que el nombre de usuario no exista
            if (await _usuarioRepository.ExisteNombreUsuarioAsync(nombreUsuario))
            {
                return (false, "El nombre de usuario ya está en uso", null);
            }

            // Crear el nuevo usuario
            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                PasswordHash = HashPassword(password),
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            var usuarioCreado = await _usuarioRepository.CrearAsync(usuario);
            return (true, "Usuario registrado exitosamente", usuarioCreado);
        }

        // Autenticar usuario (login)
        public async Task<(bool Success, string Message, Usuario? Usuario)> LoginAsync(
            string nombreUsuario, 
            string password)
        {
            var usuario = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nombreUsuario);

            if (usuario == null)
            {
                return (false, "Usuario o contraseña incorrectos", null);
            }

            if (!VerifyPassword(password, usuario.PasswordHash))
            {
                return (false, "Usuario o contraseña incorrectos", null);
            }

            return (true, "Login exitoso", usuario);
        }

        // Obtener usuario por ID
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _usuarioRepository.ObtenerPorIdAsync(id);
        }

        // Hash de contraseña usando SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Verificar contraseña
        private bool VerifyPassword(string password, string passwordHash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == passwordHash;
        }
    }
}
