using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class MaquinaRepository : IMaquinaRepository
    {
        private alatechContext Ctx = new alatechContext();
        public List<Machine> GetMaquinas()
        {
            return Ctx.Machines.ToList();
        }

        public List<Machine> SearchMaquinas(string parametroBusca)
        {
            return Ctx.Machines.ToList().FindAll(m => m.Name.Contains(parametroBusca));
        }
    }
}