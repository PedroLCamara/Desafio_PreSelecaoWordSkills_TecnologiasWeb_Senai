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
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados ao login 
    /// </summary>
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

        /// <summary>
        /// Endpoint responsável por realizar o login de um usuário e retornar seu token
        /// </summary>
        /// <param name="UsuarioLogin">Usuário a fazer login</param>
        /// <returns>Token em formato JSON</returns>
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

        /// <summary>
        /// Endpoint responsável pelo logout de um usuário
        /// </summary>
        /// <returns>Mensagem informando o decorrer do logout em formato JSON</returns>
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
