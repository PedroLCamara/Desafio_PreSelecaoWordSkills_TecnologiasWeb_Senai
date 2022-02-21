using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IDispositivoArmazenamentoRepository
    {
        List<Storagedevice> GetDispositivosArmazenamento();

        List<Storagedevice> SearchDispositivosArmazenamento(string parametroBusca);
    }
}
