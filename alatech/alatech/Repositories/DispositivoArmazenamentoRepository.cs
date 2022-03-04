using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de StorageDevice dentro da aplicação
    /// </summary>
    public class DispositivoArmazenamentoRepository : IDispositivoArmazenamentoRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Lista todos os dispositivos de armazenamento
        /// </summary>
        /// <returns>Lista de dispositivos de armazenamento</returns>
        public List<Storagedevice> GetDispositivosArmazenamento()
        {
            return Ctx.Storagedevices.ToList();
        }

        /// <summary>
        /// Pesquisa um ou mais dispositivo de armazenamento por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Dispositivo(s) encontrado(s)</returns>
        public List<Storagedevice> SearchDispositivosArmazenamento(string parametroBusca)
        {
            return Ctx.Storagedevices.ToList().FindAll(sd => sd.Name.Contains(parametroBusca));
        }
    }
}
