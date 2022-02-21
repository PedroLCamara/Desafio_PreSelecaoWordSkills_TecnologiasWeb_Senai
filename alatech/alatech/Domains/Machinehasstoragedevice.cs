using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Machinehasstoragedevice
    {
        [Key]
        [Required(ErrorMessage = "Id da máquina inválido")]
        public int MachineId { get; set; }
        
        [Key]
        [Required(ErrorMessage = "Id do dispositivo de armazenamento inválido")]
        public int StorageDeviceId { get; set; }

        [Required(ErrorMessage = "Quantidade inválida")]
        public int Amount { get; set; }

        public virtual Machine Machine { get; set; }
        public virtual Storagedevice StorageDevice { get; set; }
    }
}
