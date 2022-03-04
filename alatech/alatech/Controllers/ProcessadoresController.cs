using alatech.Interfaces;
using alatech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados aos processadores
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProcessadoresController : ControllerBase
    {
        private readonly IProcessadorRepository _processadorRepository;
        public ProcessadoresController(IProcessadorRepository contexto)
        {
            _processadorRepository = contexto;
        }

        /// <summary>
        /// Endpoint de listagem de processadores
        /// </summary>
        /// <returns>Lista de processadores em formato JSON</returns>
        [HttpGet]
        [Authorize]
        public IActionResult ListarProcessadores()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues headerValues;
                Request.Headers.TryGetValue("Authorization", out headerValues);
                string Token = headerValues.ToString();

                if (ValidacaoToken.ValidarToken(Token) == false)
                {
                    return StatusCode(403, new { message = "token inválido" });
                }
                return Ok(_processadorRepository.GetProcessadores());
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }

        /// <summary>
        /// Endpoint de busca de um ou mais processadores por uma query (string)
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Processador(es) em formato JSON</returns>
        [HttpGet("{parametroBusca}")]
        [Authorize]
        public IActionResult BuscarProcessadores(string parametroBusca)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues headerValues;
                Request.Headers.TryGetValue("Authorization", out headerValues);
                string Token = headerValues.ToString();

                if (ValidacaoToken.ValidarToken(Token) == false)
                {
                    return StatusCode(403, new { message = "token inválido" });
                }
                return Ok(_processadorRepository.SearchProcessadores(parametroBusca));
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }
    }
}
