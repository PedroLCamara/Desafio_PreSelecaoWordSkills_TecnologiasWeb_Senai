using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Processor
    {
        public Processor()
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

        [Required(ErrorMessage = "Id de tipo de soquete inválido")]
        public int SocketTypeId { get; set; }

        [Required(ErrorMessage = "Número de cores inválido")]
        public int Cores { get; set; }

        [Required(ErrorMessage = "Valor de frequência base inválido")]
        public float BaseFrequency { get; set; }

        [Required(ErrorMessage = "Valor de frequência máxima inválido")]
        public float MaxFrequency { get; set; }

        [Required(ErrorMessage = "Valor de memória cache inválido")]
        public float CacheMemory { get; set; }

        [Required(ErrorMessage = "Valor de tdp inválido")]
        public int Tdp { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Sockettype SocketType { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
