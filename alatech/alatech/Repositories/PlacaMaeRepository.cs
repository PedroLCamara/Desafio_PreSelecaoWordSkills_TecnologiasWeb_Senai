using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class PlacaMaeRepository : IPlacaMaeRepository
    {
        private alatechContext Ctx = new alatechContext();
        public List<Motherboard> GetPlacasMae()
        {
            return Ctx.Motherboards.ToList();
        }

        public List<Motherboard> SearchPlacasMae(string parametroBusca)
        {
            return Ctx.Motherboards.ToList().FindAll(m => m.Name.Contains(parametroBusca));
        }
    }
}