using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Graphiccard
    {
        public Graphiccard()
        {
            Machines = new HashSet<Machine>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url da imagem inválido")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Id da marca inválido")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Valor de VRAM inválido")]
        public int MemorySize { get; set; }

        [Required(ErrorMessage = "Tipo de memória inválido")]
        public string MemoryType { get; set; }

        [Required(ErrorMessage = "Valor de energia mínimo inválido")]
        public int MinimumPowerSupply { get; set; }

        [Required(ErrorMessage = "Suporte a multi gpu não especificado")]
        public bool SupportMultiGpu { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
