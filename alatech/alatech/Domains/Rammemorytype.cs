using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Rammemorytype
    {
        public Rammemorytype()
        {
            Motherboards = new HashSet<Motherboard>();
            Rammemories = new HashSet<Rammemory>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
        public virtual ICollection<Rammemory> Rammemories { get; set; }
    }
}
