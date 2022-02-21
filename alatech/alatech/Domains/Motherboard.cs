using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Motherboard
    {
        public Motherboard()
        {
            Machines = new HashSet<Machine>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url de imagem inválido")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Id da marca inválido")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Id do tipo de soquete inválido")]
        public int SocketTypeId { get; set; }

        [Required(ErrorMessage = "Id do tipo de memória ram inválido")]
        public int RamMemoryTypeId { get; set; }

        [Required(ErrorMessage = "Número de slots de memória ram inválido")]
        public int RamMemorySlots { get; set; }

        [Required(ErrorMessage = "Valor máximo de tdp inválido")]
        public int MaxTdp { get; set; }

        [Required(ErrorMessage = "Número de slots SATA inválido")]
        public int SataSlots { get; set; }

        [Required(ErrorMessage = "Número de slots M2 inválido")]
        public int M2Slots { get; set; }

        [Required(ErrorMessage = "Número de slots Pci inválido")]
        public int PciSlots { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Rammemorytype RamMemoryType { get; set; }
        public virtual Sockettype SocketType { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
