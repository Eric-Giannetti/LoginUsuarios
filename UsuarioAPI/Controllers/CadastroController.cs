using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Data.Dtos;
using UsuarioAPI.Requests;
using UsuarioAPI.Services;

namespace UsuarioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {

        CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuatio(CreateUsuarioDto createDto) 
        {
            Result result = _cadastroService.CadastraUsuario(createDto);
            if (result.IsFailed) return StatusCode(500, result.Errors);
            return Ok(result.Successes);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result result = _cadastroService.AtivaUsuario(request);

            if (result.IsFailed) 
                return StatusCode(500, result.Errors);
            return Ok(result.Successes);
        }
    }
}
