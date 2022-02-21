using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IProcessadorRepository
    {
        List<Processor> GetProcessadores();

        List<Processor> SearchProcessadores(string parametroBusca);
    }
}
