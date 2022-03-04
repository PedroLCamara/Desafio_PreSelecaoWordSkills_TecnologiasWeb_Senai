using alatech.Interfaces;
using alatech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados as placas mae
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PlacasMaeController : ControllerBase
    {
        private readonly IPlacaMaeRepository _placaMaeRepository;
        public PlacasMaeController(IPlacaMaeRepository contexto)
        {
            _placaMaeRepository = contexto;
        }

        /// <summary>
        /// Endpoint de listagem de placas mae
        /// </summary>
        /// <returns>Lista de placas mae em formato JSON</returns>
        [HttpGet]
        [Authorize]
        public IActionResult ListarPlacasMae()
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
                return Ok(_placaMaeRepository.GetPlacasMae());
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }

        /// <summary>
        /// Endpoint de busca de uma ou mais placas mae por uma query (string)
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>-Placa(s) mae em formato JSON</returns>
        [HttpGet("{parametroBusca}")]
        [Authorize]
        public IActionResult BuscarPlacasMae(string parametroBusca)
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
                return Ok(_placaMaeRepository.SearchPlacasMae(parametroBusca));
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }
    }
}