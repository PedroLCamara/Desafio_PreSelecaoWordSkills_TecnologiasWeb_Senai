using alatech.Interfaces;
using alatech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace alatech.Controllers
{
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
