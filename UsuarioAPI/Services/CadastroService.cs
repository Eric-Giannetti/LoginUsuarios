using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Web;
using UsuarioAPI.Data.Dtos;
using UsuarioAPI.Models;
using UsuarioAPI.Requests;

namespace UsuarioAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            if (resultadoIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encodeCode = HttpUtility.UrlEncode(code);
                _emailService.EnviarEmailDeConfirmacao(new[] { usuarioIdentity.Email }, "Confirmação de conta", usuarioIdentity.Id, encodeCode);
                return Result.Ok().WithSuccess(code);
            }

                return Result.Fail("Falha ao cadastrar o usuário");
        }

        public Result AtivaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == request.UsuarioId);

            if (identityUser == null)
                return Result.Fail("Usuário não encontrado");

            var result = _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            if (result.Succeeded)
                return Result.Ok();

            return Result.Fail("Falha ao ativar a conta");
        }
    }
}