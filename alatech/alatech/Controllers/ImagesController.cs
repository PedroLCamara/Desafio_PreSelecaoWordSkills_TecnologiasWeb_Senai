using alatech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados as imagens 
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ImagesController : ControllerBase
    {

        /// <summary>
        /// Endpoint responsável por buscar uma imagem pelo nome de seu arquivo (desconsiderando extensao)
        /// </summary>
        /// <param name="IdImg"></param>
        /// <returns></returns>
        [HttpGet("{idImg}")]
        [Authorize]
        public IActionResult BuscarPorId(int IdImg)
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

                if (Upload.BuscarEConverterImagem(IdImg) != null)
                {
                    return Ok(new { ImgB64 = Upload.BuscarEConverterImagem(IdImg) });
                }
                else return BadRequest(new { message = "Imagem não encontrada" });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Erro = erro });
                throw;
            }
        }
    }
}
