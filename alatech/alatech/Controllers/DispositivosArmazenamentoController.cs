using alatech.Interfaces;
using alatech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados aos dipositivos de armazenamento
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DispositivosArmazenamentoController : ControllerBase
    {
        private readonly IDispositivoArmazenamentoRepository _dispositivoArmazenamentoRepository;
        public DispositivosArmazenamentoController(IDispositivoArmazenamentoRepository contexto)
        {
            _dispositivoArmazenamentoRepository = contexto;
        }

        /// <summary>
        /// Endpoint de listagem de dispositivos de armazenamento
        /// </summary>
        /// <returns>Lista de dispositivos de armazenamento em formato JSON</returns>
        [HttpGet]
        [Authorize]
        public IActionResult ListarDspArmz()
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
                return Ok(_dispositivoArmazenamentoRepository.GetDispositivosArmazenamento());
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }

        /// <summary>
        /// Endpoint de busca de um ou mais dispositivos de armazenamento por uma query (string)
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Dispositivo(s) de armazenamento em formato JSON</returns>
        [HttpGet("{parametroBusca}")]
        [Authorize]
        public IActionResult BuscarDspArmz(string parametroBusca)
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
                return Ok(_dispositivoArmazenamentoRepository.SearchDispositivosArmazenamento(parametroBusca));
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }
    }
}
