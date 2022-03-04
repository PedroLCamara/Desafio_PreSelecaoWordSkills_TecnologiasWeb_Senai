
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alatech.ViewModels
{
    /// <summary>
    /// Classe para definir o objeto JSON da requisicao de consulta de incompatibilidades
    /// </summary>
    public class VerificarIncompatibilidadeViewModel
    {
        [Required(ErrorMessage = "Id de placa mãe inválido")]
        public int IdPlacaMae { get; set; }

        [Required(ErrorMessage = "Id de fonte inválido")]
        public int IdFonte { get; set; }

        public int IdProcessador { get; set; }

        public int IdMemoriaRam { get; set; }

        public int QntMemoriaRam { get; set; }

        public int IdPlacaVideo { get; set; }

        public int QntPlacaVideo { get; set; }

        public List<DispositivoArmazenamentoViewModel> DispositivosDeArmazenamento { get; set; }
    }
}
