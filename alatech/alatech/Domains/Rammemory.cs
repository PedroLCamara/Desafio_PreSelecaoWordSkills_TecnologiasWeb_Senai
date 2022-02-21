using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Rammemory
    {
        public Rammemory()
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

        [Required(ErrorMessage = "Tamanho da memória inválido")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Id do tipo de memória inválido")]
        public int RamMemoryTypeId { get; set; }

        [Required(ErrorMessage = "Valor de frequência inválido")]
        public float Frequency { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Rammemorytype RamMemoryType { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
