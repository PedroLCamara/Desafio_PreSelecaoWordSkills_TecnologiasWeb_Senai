using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IMemoriaRamRepository
    {
        List<Rammemory> GetMemoriasRam();

        List<Rammemory> SearchMemoriasRam(string parametroBusca);
    }
}
