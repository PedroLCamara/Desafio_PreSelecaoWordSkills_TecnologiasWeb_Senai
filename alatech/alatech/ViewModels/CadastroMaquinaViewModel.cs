using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alatech.ViewModels
{
    public class CadastroMaquinaViewModel
    {
        [Required(ErrorMessage = "Id de placa mãe inválido")]
        public int IdPlacaMae { get; set; }

        [Required(ErrorMessage = "Id de fonte inválido")]
        public int IdFonte { get; set; }

        [Required(ErrorMessage = "Id de processador inválido")]
        public int IdProcessador { get; set; }

        [Required(ErrorMessage = "Id de memória RAM inválido")]
        public int IdMemoriaRam { get; set; }

        [Required(ErrorMessage = "Quantidade de memória RAM inválida")]
        public int QntMemoriaRam { get; set; }

        [Required(ErrorMessage = "Id de placa de vídeo inválido")]
        public int IdPlacaVideo { get; set; }

        [Required(ErrorMessage = "Quantidade de placa de vídeo inválida")]
        public int QntPlacaVideo { get; set; }

        [Required(ErrorMessage = "Nome da máquina inválido")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Descrição da máquina inválido")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Dispositivo(s) de armazenamento inválido(s)")]
        public List<DispositivoArmazenamentoViewModel> DispositivosDeArmazenamento { get; set; }
    }
}
