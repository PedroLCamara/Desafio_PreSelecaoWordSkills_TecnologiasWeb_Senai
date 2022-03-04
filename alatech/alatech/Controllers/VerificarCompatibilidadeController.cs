using alatech.Domains;
using alatech.Interfaces;
using alatech.Utils;
using alatech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace alatech.Controllers
{
    /// <summary>
    /// Controlador responsável por definir os endpoints relacionados a verificacao de compatibilidade 
    /// </summary>
    [Route("alatech/api/[controller]")]
    [ApiController]
    public class VerificarCompatibilidadeController : ControllerBase
    {
        private readonly IMaquinaRepository _maquinaRepository;
        public VerificarCompatibilidadeController(IMaquinaRepository contexto)
        {
            _maquinaRepository = contexto;
        }

        /// <summary>
        /// Endpoint responsável por verificar a compatibilidade entre pecas
        /// </summary>
        /// <param name="Consulta">Objeto que contem as pecas</param>
        /// <returns>Resultados da analise de compatibilidade em formato JSON</returns>
        [HttpPost]
        [Authorize]
        public IActionResult VerificarIncompatibilidade(VerificarIncompatibilidadeViewModel Consulta)
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
                Machine Maquina = _maquinaRepository.RetornarMaquina(Consulta);

                List<string> Incompatibilidades = new List<string>();
                if (Consulta.IdProcessador != 0)
                {
                    Incompatibilidades.Add(VerificarIncompatibilidades.TipoSoqueteProcessador_PlacaMae(Maquina.Motherboard.SocketTypeId, Maquina.Processor.SocketTypeId));
                    Incompatibilidades.Add(VerificarIncompatibilidades.TdpProcessador_PlacaMae(Maquina.Processor.Tdp, Maquina.Motherboard.MaxTdp));
                }
                if (Consulta.IdMemoriaRam != 0)
                {
                    Incompatibilidades.Add(VerificarIncompatibilidades.MemoriaRam_PlacaMae(Maquina.RamMemory.RamMemoryTypeId, Maquina.Motherboard.RamMemoryTypeId));
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdMemoriaRam_PlacaMae(Maquina.RamMemoryAmount, Maquina.Motherboard.RamMemorySlots));
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdMemoriaRam(Maquina.RamMemoryAmount));
                }
                if (Consulta.IdPlacaVideo != 0)
                {
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdPlacaVideo_PlacaMae(Maquina.GraphicCardAmount, Maquina.Motherboard.PciSlots));
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdPlacaVideo(Maquina.GraphicCardAmount));
                    Incompatibilidades.Add(VerificarIncompatibilidades.MultiplasPlacaVideo(Maquina.GraphicCardAmount, Maquina.GraphicCard.SupportMultiGpu));
                    Incompatibilidades.Add(VerificarIncompatibilidades.PotenciaFonte_PlacasVideo(Maquina.GraphicCardAmount, Maquina.GraphicCard.MinimumPowerSupply, Maquina.PowerSupply.Potency));
                }

                if (Consulta.DispositivosDeArmazenamento != null)
                {
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
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmzM2_PlacaMae(QtdDspArmzM2, Maquina.Motherboard.M2Slots));
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmzSATA_PlacaMae(QtdDspArmzSATA, Maquina.Motherboard.SataSlots));
                    Incompatibilidades.Add(VerificarIncompatibilidades.QtdDspArmz(QtdDspArmzSATA, QtdDspArmzM2));
                }

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
                    return Ok(new { message = "Máquina válida" });
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
                throw;
            }
        }
    }
}