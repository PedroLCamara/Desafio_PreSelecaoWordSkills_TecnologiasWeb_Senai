using alatech.Domains;
using alatech.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alatech.Interfaces
{
    public interface IMaquinaRepository
    {
        List<Machine> GetMaquinas();

        List<Machine> SearchMaquinas(string parametroBusca);

        Machine PostMaquina(Machine maquina);

        Machine RetornarMaquina(CadastroMaquinaViewModel maquina);

        Machine PutMaquina(Machine maquina);

        Machine GetByIdMaquina(int id);

        void DeleteMaquina(int id);
    }
}