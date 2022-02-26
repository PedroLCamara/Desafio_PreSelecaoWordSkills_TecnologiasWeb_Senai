using System.ComponentModel.DataAnnotations;

namespace alatech.ViewModels
{
    public class DispositivoArmazenamentoViewModel
    {
        [Required(ErrorMessage = "Id de dispositivo de armazenamento inválido")]
        public int IdDspArmazenamento { get; set; }

        [Required(ErrorMessage = "Quantidade de dispositivo de armazenamento inválida")]
        public int QntDspArmazenamento { get; set; }
    }
}
