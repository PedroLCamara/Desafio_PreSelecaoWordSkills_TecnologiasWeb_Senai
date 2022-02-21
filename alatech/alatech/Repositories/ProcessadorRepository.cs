using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class ProcessadorRepository : IProcessadorRepository
    {
        private alatechContext Ctx = new alatechContext();

        public List<Processor> GetProcessadores()
        {
            return Ctx.Processors.ToList();
        }

        public List<Processor> SearchProcessadores(string parametroBusca)
        {
            return Ctx.Processors.ToList().FindAll(p => p.Name.Contains(parametroBusca));
        }
    }
}
