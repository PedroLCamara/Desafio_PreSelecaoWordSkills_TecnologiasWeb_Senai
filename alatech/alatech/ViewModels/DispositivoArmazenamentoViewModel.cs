using System.ComponentModel.DataAnnotations;

namespace alatech.ViewModels
{
    /// <summary>
    /// Classe para a definição de um dispositivo de armazenamento no cadastro de uma nova máquina
    /// </summary>
    public class DispositivoArmazenamentoViewModel
    {
        [Required(ErrorMessage = "Id de dispositivo de armazenamento inválido")]
        public int IdDspArmazenamento { get; set; }

        [Required(ErrorMessage = "Quantidade de dispositivo de armazenamento inválida")]
        public int QntDspArmazenamento { get; set; }
    }
}
