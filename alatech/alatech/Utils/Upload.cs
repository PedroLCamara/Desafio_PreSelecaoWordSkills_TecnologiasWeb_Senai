using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace alatech.Utils
{
    public static class Upload
    {
        /// <summary>
        /// Salva um arquivo de imagem .png no diretório StativFiles/Images
        /// </summary>
        /// <param name="arquivo">Arquivo .png</param>
        /// <returns>Caminho completo da imagem em formato de string</returns>
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

        /// <summary>
        /// Valida a extensao do arquivo (.png)
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do arquivo</param>
        /// <returns>True (extensao .png) ou false (extensao != .png)</returns>
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

        /// <summary>
        /// Retorna a extensção do nome do arquivo
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do arquivo</param>
        /// <returns>Extensao (ex: .png, .jpeg ...)</returns>
        public static string RetornarExtensao(string nomeDoArquivo)
        {
            string[] dados = nomeDoArquivo.Split(".");
            return dados[dados.Length - 1];
        }

        /// <summary>
        /// Remove um arquivo com base no nome
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do arquivo</param>
        public static void RemoverArquivo(string nomeDoArquivo)
        {
            var pasta = Path.Combine("StaticFiles", "Images");
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);
            var caminhoCompleto = Path.Combine(caminho, nomeDoArquivo);

            File.Delete(caminhoCompleto);
        }

        /// <summary>
        /// Busca uma imagem e a converte para uma string Base64
        /// </summary>
        /// <param name="IdImg">Nome da imagem salvo no Banco de Dados</param>
        /// <returns>String Base64</returns>
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
