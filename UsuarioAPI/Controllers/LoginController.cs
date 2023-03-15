using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Requests;
using UsuarioAPI.Services;

namespace UsuarioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _login;

        public LoginController(LoginService login)
        {
            _login = login;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest request)
        {
            Result resultado = _login.LogaUsuario(request);
            if (resultado.IsFailed)
                return Unauthorized(resultado.Errors);

            return Ok(resultado.Successes);
        }
    }
}
