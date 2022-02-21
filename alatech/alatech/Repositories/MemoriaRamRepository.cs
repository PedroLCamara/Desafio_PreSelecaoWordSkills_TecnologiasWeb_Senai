using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class MemoriaRamRepository : IMemoriaRamRepository
    {
        private alatechContext Ctx = new alatechContext();
        public List<Rammemory> GetMemoriasRam()
        {
            return Ctx.Rammemories.ToList();
        }

        public List<Rammemory> SearchMemoriasRam(string parametroBusca)
        {
            return Ctx.Rammemories.ToList().FindAll(rm => rm.Name.Contains(parametroBusca));
        }
    }
}
