using alatech.Interfaces;
using alatech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados as fontes
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FontesController : ControllerBase
    {
        private readonly IFonteRepository _fonteRepository;
        public FontesController(IFonteRepository contexto)
        {
            _fonteRepository = contexto;
        }

        /// <summary>
        /// Endpoint de listagem de fontes
        /// </summary>
        /// <returns>Lista de fontes em formato JSON</returns>
        [HttpGet]
        [Authorize]
        public IActionResult ListarFontes()
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
                return Ok(_fonteRepository.GetFontes());
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }

        /// <summary>
        /// Endpoint de busca de uma ou mais fontes por uma query (string)
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Fonte(s) em formato JSON</returns>
        [HttpGet("{parametroBusca}")]
        [Authorize]
        public IActionResult BuscarFontes(string parametroBusca)
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
                return Ok(_fonteRepository.SearchFontes(parametroBusca));
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }
    }
}
