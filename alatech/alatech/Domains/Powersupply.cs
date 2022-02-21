using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Powersupply
    {
        public Powersupply()
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

        [Required(ErrorMessage = "Valor de potência inválido")]
        public int Potency { get; set; }

        [Required(ErrorMessage = "Valor da badge 80 plus inválido")]
        public string Badge80Plus { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
