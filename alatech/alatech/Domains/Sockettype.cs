using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Sockettype
    {
        public Sockettype()
        {
            Motherboards = new HashSet<Motherboard>();
            Processors = new HashSet<Processor>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
        public virtual ICollection<Processor> Processors { get; set; }
    }
}
