using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    public interface IPlacaVideoRepository
    {
        List<Graphiccard> GetPlacasVideo();

        List<Graphiccard> SearchPlacasVideo(string parametroBusca);
    }
}
