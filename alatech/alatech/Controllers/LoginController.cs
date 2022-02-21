using alatech.Domains;
using alatech.Interfaces;
using alatech.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;

namespace alatech.Controllers
{
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository contexto)
        {
            _usuarioRepository = contexto;
        }

        [HttpPost]
        public IActionResult Logar(LoginViewModel UsuarioLogin)
        {
            try
            {
                User UsuarioConsulta = _usuarioRepository.Login(UsuarioLogin.NomeDeUsuario, UsuarioLogin.Senha);
                if (UsuarioConsulta != null)
                {
                    if (UsuarioConsulta.AccessToken != "Não especificado")
                    {
                        return StatusCode(403, new { message = "Usuário já autenticado" });
                    }
                    else
                    {
                        var minhasClaims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.NameId, UsuarioConsulta.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, UsuarioConsulta.Id.ToString())
                        };
                        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("alatech-chave-seguro"));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var meuToken = new JwtSecurityToken(
                                issuer: "alatech.webAPI",
                                audience: "alatech.webAPI",
                                claims: minhasClaims,
                                expires: DateTime.Now.AddHours(1),
                                signingCredentials: creds
                            );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(meuToken);

                        _usuarioRepository.SalvarToken(tokenString, UsuarioConsulta);

                        return Ok(new
                        {
                            token = tokenString
                        });
                    }
                }
                else return BadRequest(new { message = "Credenciais inválidas" });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Erro = erro });
            }
        }

        [HttpDelete]
        public IActionResult Deslogar()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues headerValues;
                Request.Headers.TryGetValue("Authorization", out headerValues);
                string Token = headerValues.ToString();

                if (Token != "")
                {
                    if (_usuarioRepository.Logout(Token))
                    {
                        return Ok(new { message = "Logout com sucesso" });
                    }
                    else return StatusCode(403, new { message = "Token inválido" });
                }
                else return Unauthorized(new { message = "Necessário autenticação" });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Erro = erro });
            }
        }
    }
}
