using alatech.Domains;
using alatech.Interfaces;
using alatech.Utils;
using alatech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados as maquinas
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MaquinasController : ControllerBase
    {
        private readonly IMaquinaRepository _maquinaRepository;
        public MaquinasController(IMaquinaRepository contexto)
        {
            _maquinaRepository = contexto;
        }

        /// <summary>
        /// Endpoint de listagem de maquinas
        /// </summary>
        /// <returns>Lista de maquinas em formato JSON</returns>
        [HttpGet]
        [Authorize]
        public IActionResult ListarMaquinas()
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
                return Ok(_maquinaRepository.GetMaquinas());
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }

        /// <summary>
        /// Endpoint de busca de uma ou mais maquinas por uma query (string)
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Maquina(s) em formato JSON</returns>
        [HttpGet("{parametroBusca}")]
        [Authorize]
        public IActionResult BuscarMaquinas(string parametroBusca)
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
                return Ok(_maquinaRepository.SearchMaquinas(parametroBusca));
            }
            catch (Exception erro)
            {
                BadRequest(new { Erro = erro });
                throw;
            }
        }

        /// <summary>
        /// Endpoint responsável por cadastrar uma nova maquina
        /// </summary>
        /// <param name="MaquinaRequisicao">Maquina a ser cadastrada</param>
        /// <returns>Resultado do cadastro em formato JSON</returns>
        [HttpPost]
        [Authorize]
        public IActionResult AdicionarMaquina(CadastroMaquinaViewModel MaquinaRequisicao)
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

                Machine Maquina = _maquinaRepository.RetornarMaquina(MaquinaRequisicao);

                List<string> Incompatibilidades = new List<string>();
                Incompatibilidades.Add(VerificarIncompatibilidades.TipoSoqueteProcessador_PlacaMae(Maquina.Motherboard.SocketTypeId, Maquina.Processor.SocketTypeId));
                Incompatibilidades.Add(VerificarIncompatibilidades.TdpProcessador_PlacaMae(Maquina.Processor.Tdp, Maquina.Motherboard.MaxTdp));
                Incompatibilidades.Add(VerificarIncompatibilidades.MemoriaRam_PlacaMae(Maquina.RamMemory.RamMemoryTypeId, Maquina.Motherboard.RamMemoryTypeId));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdMemoriaRam_PlacaMae(Maquina.RamMemoryAmount, Maquina.Motherboard.RamMemorySlots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdPlacaVideo_PlacaMae(Maquina.GraphicCardAmount, Maquina.Motherboard.PciSlots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdMemoriaRam(Maquina.RamMemoryAmount));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdPlacaVideo(Maquina.GraphicCardAmount));
                Incompatibilidades.Add(VerificarIncompatibilidades.MultiplasPlacaVideo(Maquina.GraphicCardAmount, Maquina.GraphicCard.SupportMultiGpu));
                Incompatibilidades.Add(VerificarIncompatibilidades.PotenciaFonte_PlacasVideo(Maquina.GraphicCardAmount, Maquina.GraphicCard.MinimumPowerSupply, Maquina.PowerSupply.Potency));

                int QtdDspArmzSATA = 0;
                int QtdDspArmzM2 = 0;
                foreach (Machinehasstoragedevice item in Maquina.Machinehasstoragedevices)
                {
                    if (item.StorageDevice.StorageDeviceInterface == "m2")
                    {
                        QtdDspArmzM2 = QtdDspArmzM2 + item.Amount;
                    }
                    else if (item.StorageDevice.StorageDeviceInterface == "sata")
                    {
                        QtdDspArmzSATA = QtdDspArmzSATA + item.Amount;
                    }
                }
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmzSATA_PlacaMae(QtdDspArmzSATA, Maquina.Motherboard.SataSlots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmzM2_PlacaMae(QtdDspArmzM2, Maquina.Motherboard.M2Slots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmz(QtdDspArmzSATA, QtdDspArmzM2));

                bool CompatibilidadeTotal = true;
                List<IncompatibilidadeViewModel> IncompatibilidadesRetorno = new List<IncompatibilidadeViewModel>();
                foreach (string item in Incompatibilidades)
                {
                    if (item != null)
                    {
                        CompatibilidadeTotal = false;
                        IncompatibilidadeViewModel NovaIncompatibilidade = new IncompatibilidadeViewModel()
                        {
                            Titulo = item.Split("-")[0],
                            Descricao = item.Split("-")[1],
                        };
                        IncompatibilidadesRetorno.Add(NovaIncompatibilidade);
                    }
                }

                if (CompatibilidadeTotal == false)
                {
                    return BadRequest(IncompatibilidadesRetorno);
                }
                else
                {
                        Maquina.ImageUrl = "0";
                        Machine MaquinaRetorno = _maquinaRepository.PostMaquina(Maquina);
                        return Ok(new { Id = MaquinaRetorno.Id});
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }
        
        /// <summary>
        /// Endpoint responsavel por atualizar uma maquina
        /// </summary>
        /// <param name="MaquinaRequisicao">Maquina atualizada</param>
        /// <param name="IdMaquina">Id da maquina a ser atualizada</param>
        /// <returns>Resultado da atualizacao em formato JSON</returns>
        [HttpPut("{idMaquina}")]
        [Authorize]
        public IActionResult AtualizarMaquina(CadastroMaquinaViewModel MaquinaRequisicao, int IdMaquina)
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

                Machine Maquina = _maquinaRepository.RetornarMaquina(MaquinaRequisicao);

                List<string> Incompatibilidades = new List<string>();
                Incompatibilidades.Add(VerificarIncompatibilidades.TipoSoqueteProcessador_PlacaMae(Maquina.Motherboard.SocketTypeId, Maquina.Processor.SocketTypeId));
                Incompatibilidades.Add(VerificarIncompatibilidades.TdpProcessador_PlacaMae(Maquina.Processor.Tdp, Maquina.Motherboard.MaxTdp));
                Incompatibilidades.Add(VerificarIncompatibilidades.MemoriaRam_PlacaMae(Maquina.RamMemory.RamMemoryTypeId, Maquina.Motherboard.RamMemoryTypeId));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdMemoriaRam_PlacaMae(Maquina.RamMemoryAmount, Maquina.Motherboard.RamMemorySlots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdPlacaVideo_PlacaMae(Maquina.GraphicCardAmount, Maquina.Motherboard.PciSlots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdMemoriaRam(Maquina.RamMemoryAmount));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdPlacaVideo(Maquina.GraphicCardAmount));
                Incompatibilidades.Add(VerificarIncompatibilidades.MultiplasPlacaVideo(Maquina.GraphicCardAmount, Maquina.GraphicCard.SupportMultiGpu));
                Incompatibilidades.Add(VerificarIncompatibilidades.PotenciaFonte_PlacasVideo(Maquina.GraphicCardAmount, Maquina.GraphicCard.MinimumPowerSupply, Maquina.PowerSupply.Potency));

                int QtdDspArmzSATA = 0;
                int QtdDspArmzM2 = 0;
                foreach (Machinehasstoragedevice item in Maquina.Machinehasstoragedevices)
                {
                    if (item.StorageDevice.StorageDeviceInterface == "m2")
                    {
                        QtdDspArmzM2 = QtdDspArmzM2 + item.Amount;
                    }
                    else if (item.StorageDevice.StorageDeviceInterface == "sata")
                    {
                        QtdDspArmzSATA = QtdDspArmzSATA + item.Amount;
                    }
                }
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmzSATA_PlacaMae(QtdDspArmzSATA, Maquina.Motherboard.SataSlots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmzM2_PlacaMae(QtdDspArmzM2, Maquina.Motherboard.M2Slots));
                Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmz(QtdDspArmzSATA, QtdDspArmzM2));

                bool CompatibilidadeTotal = true;
                List<IncompatibilidadeViewModel> IncompatibilidadesRetorno = new List<IncompatibilidadeViewModel>();
                foreach (string item in Incompatibilidades)
                {
                    if (item != null)
                    {
                        CompatibilidadeTotal = false;
                        IncompatibilidadeViewModel NovaIncompatibilidade = new IncompatibilidadeViewModel()
                        {
                            Titulo = item.Split("-")[0],
                            Descricao = item.Split("-")[1],
                        };
                        IncompatibilidadesRetorno.Add(NovaIncompatibilidade);
                    }
                }

                if (CompatibilidadeTotal == false)
                {
                    return BadRequest(IncompatibilidadesRetorno);
                }
                else
                {
                    Machine MaquinaConsulta = _maquinaRepository.GetByIdMaquina(IdMaquina);
                    if (MaquinaConsulta == null)
                    {
                        return NotFound(new { message = "Modelo de máquina não encontrado" });
                    }
                    Maquina.ImageUrl = MaquinaConsulta.ImageUrl;
                    Maquina.Id = IdMaquina;
                    foreach (var item in Maquina.Machinehasstoragedevices)
                    {
                        item.MachineId = IdMaquina;
                    }
                    Machine MaquinaRetorno = _maquinaRepository.PutMaquina(Maquina);
                    return Ok(MaquinaRetorno);
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }
        /// <summary>
        /// Endpoint responsavel por deletar uma maquina
        /// </summary>
        /// <param name="idMaquina">Id da maquina a ser deletada</param>
        /// <returns>Resultado da remocao em formato JSON</returns>
        [HttpDelete("{idMaquina}")]
        [Authorize]
        public IActionResult DeletarMaquina(int idMaquina)
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
                else if (_maquinaRepository.GetByIdMaquina(idMaquina) == null)
                {
                    return NotFound(new { message = "Modelo de máquina não encontrado" });
                }
                else
                {
                    Upload.RemoverArquivo(_maquinaRepository.GetByIdMaquina(idMaquina).ImageUrl + ".png");
                    _maquinaRepository.DeleteMaquina(idMaquina);
                    return NoContent();
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }

        /// <summary>
        /// Endpoint responsavel por atualizar a imagem de uma maquina
        /// </summary>
        /// <param name="idMaquina">Id da maquina</param>
        /// <param name="Img">Imagem</param>
        /// <returns>Resultado da atualizacao de imagem em formato JSON</returns>
        [HttpPatch("{idMaquina}")]
        [Authorize]
        public IActionResult AtualizarImagemMaquina(int idMaquina, IFormFile Img)
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

                Machine MaquinaAttImg = _maquinaRepository.GetByIdMaquina(idMaquina);

                if (MaquinaAttImg == null)
                {
                    return NotFound(new { message = "Id da máquina inválido" });
                }

                if (Img == null)
                {
                    return BadRequest(new { message = "Arquivo não encontrado" });
                }

                string uploadResultado = Upload.UploadFile(Img);

                if (uploadResultado == "")
                {
                    return BadRequest(new { message = "Arquivo não encontrado" });
                }

                if (uploadResultado == "Extensão não permitida")
                {
                    return BadRequest(new { message = "Extensão de arquivo não permitida" });
                }

                MaquinaAttImg.ImageUrl = uploadResultado.Split(".")[0];

                _maquinaRepository.PutMaquina(MaquinaAttImg);

                return Ok(MaquinaAttImg);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }
    }
}
