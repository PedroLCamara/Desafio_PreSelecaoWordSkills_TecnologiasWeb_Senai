using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using alatech.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alatech.Repositories
{
    public class MaquinaRepository : IMaquinaRepository
    {
        private alatechContext Ctx = new alatechContext();

        public void DeleteMaquina(int id)
        {
            List<Machinehasstoragedevice> ListaConexaoMaquinaDspArmz = Ctx.Machinehasstoragedevices.ToList().FindAll(mhsd => mhsd.MachineId == id);
            foreach (Machinehasstoragedevice item in ListaConexaoMaquinaDspArmz)
            {
                Ctx.Machinehasstoragedevices.Remove(item);
            }
            Machine MaquinaRemover = GetByIdMaquina(id);
            Ctx.Machines.Remove(MaquinaRemover);
            Ctx.SaveChanges();
        }

        public Machine GetByIdMaquina(int id)
        {
            Machine MaquinaRetorno = Ctx.Machines.AsNoTracking().FirstOrDefault(m => m.Id == id);
            return MaquinaRetorno;
        }

        public List<Machine> GetMaquinas()
        {
            return Ctx.Machines.ToList();
        }

        public Machine PostMaquina(Machine maquina)
        {
            Ctx.Machines.Add(maquina);
            Ctx.SaveChanges();
            return maquina;
        }

        public Machine PutMaquina(Machine maquina)
        {
            Ctx.Machines.Update(maquina);
            foreach (var item in maquina.Machinehasstoragedevices)
            {
                if (item.Amount == 0)
                {
                    Ctx.Machinehasstoragedevices.Remove(item);
                }
            }
            Ctx.SaveChanges();
            return maquina;
        }

        public Machine RetornarMaquina(CadastroMaquinaViewModel maquina)
        {
            ICollection<Machinehasstoragedevice> ListaDspArmazenamentoMaquina = new List<Machinehasstoragedevice>();

            foreach (DispositivoArmazenamentoViewModel item in maquina.DispositivosDeArmazenamento)
            {
                Storagedevice testee = Ctx.Storagedevices.ToList().Find(sd => sd.Id == item.IdDspArmazenamento);
                Machinehasstoragedevice MaquinaDspArmazenamento = new Machinehasstoragedevice()
                {
                    StorageDeviceId = item.IdDspArmazenamento,
                    StorageDevice = Ctx.Storagedevices.ToList().Find(sd => sd.Id == item.IdDspArmazenamento),
                    Amount = item.QntDspArmazenamento
                };
                ListaDspArmazenamentoMaquina.Add(MaquinaDspArmazenamento);
            }

            Machine retorno = new Machine()
            {
                MotherboardId = maquina.IdPlacaMae,
                Motherboard = Ctx.Motherboards.ToList().Find(m => m.Id == maquina.IdPlacaMae),
                PowerSupplyId = maquina.IdFonte,
                PowerSupply = Ctx.Powersupplies.ToList().Find(p => p.Id == maquina.IdFonte),
                ProcessorId = maquina.IdProcessador,
                Processor = Ctx.Processors.ToList().Find(p => p.Id == maquina.IdProcessador),
                RamMemoryId = maquina.IdMemoriaRam,
                RamMemory = Ctx.Rammemories.ToList().Find(rm => rm.Id == maquina.IdMemoriaRam),
                RamMemoryAmount = maquina.QntMemoriaRam,
                GraphicCardId = maquina.IdPlacaVideo,
                GraphicCard = Ctx.Graphiccards.ToList().Find(gc => gc.Id == maquina.IdPlacaVideo),
                GraphicCardAmount = maquina.QntPlacaVideo,
                Name = maquina.Nome,
                Description = maquina.Descricao,
                Machinehasstoragedevices = ListaDspArmazenamentoMaquina
            };

            return retorno;
        }

        public List<Machine> SearchMaquinas(string parametroBusca)
        {
            return Ctx.Machines.ToList().FindAll(m => m.Name.Contains(parametroBusca));
        }
    }
}