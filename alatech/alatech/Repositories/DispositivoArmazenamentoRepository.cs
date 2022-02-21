using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class DispositivoArmazenamentoRepository : IDispositivoArmazenamentoRepository
    {
        private alatechContext Ctx = new alatechContext();
        public List<Storagedevice> GetDispositivosArmazenamento()
        {
            return Ctx.Storagedevices.ToList();
        }

        public List<Storagedevice> SearchDispositivosArmazenamento(string parametroBusca)
        {
            return Ctx.Storagedevices.ToList().FindAll(sd => sd.Name.Contains(parametroBusca));
        }
    }
}
