using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IMaquinaRepository
    {
        List<Machine> GetMaquinas();

        List<Machine> SearchMaquinas(string parametroBusca);
    }
}