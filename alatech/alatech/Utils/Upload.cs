using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace alatech.Utils
{
    public static class Upload
    {
        public static string UploadFile(IFormFile arquivo)
        {
            try
            {
                var pasta = Path.Combine("StaticFiles", "Images");
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                if (arquivo.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    if (ValidarExtensao(fileName))
                    {
                        int IdImg = 1;
                        bool UniqueId = false;
                        var Diretorio = Directory.EnumerateDirectories(caminho).ToList();
                        while (UniqueId == false)
                        {
                            IdImg++;

                            if (Diretorio.Find(a => a.Split(".")[0] == UniqueId.ToString()) == null)
                            {
                                UniqueId = true;
                            }
                        }
                        var extensao = RetornarExtensao(fileName);
                        var novoNome = $"{IdImg}.{extensao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }

                        return novoNome;
                    }

                    return "Extensão não permitida";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static bool ValidarExtensao(string nomeDoArquivo)
        {
            string[] dados = nomeDoArquivo.Split(".");
            string extensao = dados[dados.Length - 1];

                if (extensao == "png")
                {
                    return true;
                }
            return false;
        }

        public static string RetornarExtensao(string nomeDoArquivo)
        {
            string[] dados = nomeDoArquivo.Split(".");
            return dados[dados.Length - 1];
        }

        public static void RemoverArquivo(string nomeDoArquivo)
        {
            var pasta = Path.Combine("StaticFiles", "Images");
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);
            var caminhoCompleto = Path.Combine(caminho, nomeDoArquivo);

            File.Delete(caminhoCompleto);
        }

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
