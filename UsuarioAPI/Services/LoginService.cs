using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuarioAPI.Models;
using UsuarioAPI.Requests;

namespace UsuarioAPI.Services
{
    public class LoginService
    {
        private TokenService _tokenService;
        private SignInManager<IdentityUser<int>> _signManager;

        public LoginService(TokenService tokenService, SignInManager<IdentityUser<int>> signManager)
        {
            _tokenService = tokenService;
            _signManager = signManager;
        }
        
        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signManager.PasswordSignInAsync(request.UserName, request.Password, false, false).Result;
            if (resultadoIdentity.Succeeded)
            {
                var identityUser = _signManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.UserName);

                
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.value);
            }
                
            return Result.Fail("Usuário ou senha inválidos");
            
        }
    }
}
