using System;
using System.IO;

namespace alatech.Utils
{
    public static class Upload
    {
        public static string BuscarEConverterImagem(int IdImg)
        {
            string NomeArquivo = IdImg.ToString() + ".png";
            string Caminho = Path.Combine("StaticFiles/Images", NomeArquivo);
            if (File.Exists(Caminho))
            {
                Byte[] BytesImg = File.ReadAllBytes(Caminho);
                string ImagemB64 = Convert.ToBase64String(BytesImg);
                return ImagemB64;
            }
            return null;
        }
    }
}
