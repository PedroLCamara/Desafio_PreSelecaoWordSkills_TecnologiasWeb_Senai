using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class FonteRepository : IFonteRepository
    {
        private alatechContext Ctx = new alatechContext();
        public List<Powersupply> GetFontes()
        {
            return Ctx.Powersupplies.ToList();
        }

        public List<Powersupply> SearchFontes(string parametroBusca)
        {
            return Ctx.Powersupplies.ToList().FindAll(ps => ps.Name.Contains(parametroBusca));
        }
    }
}