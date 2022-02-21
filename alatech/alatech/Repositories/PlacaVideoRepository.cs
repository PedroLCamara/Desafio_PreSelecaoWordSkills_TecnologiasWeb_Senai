using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    public class PlacaVideoRepository : IPlacaVideoRepository
    {
        private alatechContext Ctx = new alatechContext();
        public List<Graphiccard> GetPlacasVideo()
        {
            return Ctx.Graphiccards.ToList();
        }

        public List<Graphiccard> SearchPlacasVideo(string parametroBusca)
        {
            return Ctx.Graphiccards.ToList().FindAll(gc => gc.Name.Contains(parametroBusca));
        }
    }
}