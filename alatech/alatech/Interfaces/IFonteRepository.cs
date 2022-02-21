using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IFonteRepository
    {
        List<Powersupply> GetFontes();

        List<Powersupply> SearchFontes(string parametroBusca);
    }
}