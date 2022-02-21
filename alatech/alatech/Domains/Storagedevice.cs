using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class Storagedevice
    {
        public Storagedevice()
        {
            Machinehasstoragedevices = new HashSet<Machinehasstoragedevice>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome inválido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url da imagem inválido")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Id da marca inválido")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Tipo de dispositivo de armazenamento inválido")]
        public string StorageDeviceType { get; set; }

        [Required(ErrorMessage = "Tamanho da memória do dispositivo de armazenamento inválido")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Interface do dispositivo de armazenamento inválida")]
        public string StorageDeviceInterface { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Machinehasstoragedevice> Machinehasstoragedevices { get; set; }
    }
}
