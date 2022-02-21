using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Brand
    {
        public Brand()
        {
            Graphiccards = new HashSet<Graphiccard>();
            Motherboards = new HashSet<Motherboard>();
            Powersupplies = new HashSet<Powersupply>();
            Processors = new HashSet<Processor>();
            Rammemories = new HashSet<Rammemory>();
            Storagedevices = new HashSet<Storagedevice>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        public virtual ICollection<Graphiccard> Graphiccards { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }

        public virtual ICollection<Powersupply> Powersupplies { get; set; }

        public virtual ICollection<Processor> Processors { get; set; }

        public virtual ICollection<Rammemory> Rammemories { get; set; }

        public virtual ICollection<Storagedevice> Storagedevices { get; set; }
    }
}
