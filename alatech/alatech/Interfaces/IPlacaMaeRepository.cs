using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IPlacaMaeRepository
    {
        List<Motherboard> GetPlacasMae();

        List<Motherboard> SearchPlacasMae(string parametroBusca);
    }
}
