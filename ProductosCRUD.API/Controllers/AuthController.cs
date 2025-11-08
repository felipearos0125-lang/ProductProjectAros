using Microsoft.AspNetCore.Mvc;
using ProductosCRUD.API.DTOs;
using ProductosCRUD.Business.Services;

namespace ProductosCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registrar un nuevo usuario
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _authService.RegistrarAsync(
                registerDto.NombreUsuario,
                registerDto.Password
            );

            if (!resultado.Success)
            {
                return BadRequest(new { message = resultado.Message });
            }

            var token = _jwtService.GenerarToken(resultado.Usuario!);
            var expiracion = _jwtService.ObtenerExpiracion();

            var response = new AuthResponseDto
            {
                Id = resultado.Usuario!.Id,
                NombreUsuario = resultado.Usuario.NombreUsuario,
                Token = token,
                Expiracion = expiracion
            };

            return Ok(response);
        }

        /// <summary>
        /// Iniciar sesión
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _authService.LoginAsync(
                loginDto.NombreUsuario,
                loginDto.Password
            );

            if (!resultado.Success)
            {
                return Unauthorized(new { message = resultado.Message });
            }

            var token = _jwtService.GenerarToken(resultado.Usuario!);
            var expiracion = _jwtService.ObtenerExpiracion();

            var response = new AuthResponseDto
            {
                Id = resultado.Usuario!.Id,
                NombreUsuario = resultado.Usuario.NombreUsuario,
                Token = token,
                Expiracion = expiracion
            };

            return Ok(response);
        }
    }
}
