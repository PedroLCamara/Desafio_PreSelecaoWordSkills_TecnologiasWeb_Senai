using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Machine
    {
        public Machine()
        {
            Machinehasstoragedevices = new HashSet<Machinehasstoragedevice>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição inválida")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Url da imagem inválido")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Id da placa mãe inválido")]
        public int MotherboardId { get; set; }

        [Required(ErrorMessage = "Id do processador inválido")]
        public int ProcessorId { get; set; }

        [Required(ErrorMessage = "Id da memória RAM inválido")]
        public int RamMemoryId { get; set; }

        [Required(ErrorMessage = "Número de memórias RAM inválido")]
        public int RamMemoryAmount { get; set; }

        [Required(ErrorMessage = "Id da placa de vídeo inválido")]
        public int GraphicCardId { get; set; }

        [Required(ErrorMessage = "Número de placas de vídeo inválido")]
        public int GraphicCardAmount { get; set; }

        [Required(ErrorMessage = "Id da fonte inválido")]
        public int PowerSupplyId { get; set; }

        public virtual Graphiccard GraphicCard { get; set; }
        public virtual Motherboard Motherboard { get; set; }
        public virtual Powersupply PowerSupply { get; set; }
        public virtual Processor Processor { get; set; }
        public virtual Rammemory RamMemory { get; set; }
        public virtual ICollection<Machinehasstoragedevice> Machinehasstoragedevices { get; set; }
    }
}
