using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace APIMaisEventos.Utils
{
    // Padrão Singleton -> Static Type
    public static class Upload
    {
        // Upload
        public static string UploadFile(IFormFile file, string[] allowedExtensions, string directory)
        {
            try
            {
                // Criando caminho para salvar o arquivo
                var folder = Path.Combine("StaticFiles", directory);
                var path = Path.Combine(Directory.GetCurrentDirectory(), folder);

                // Verificando a existência do arquivo
                if (file.Length > 0 && file != null)
                {
                    // Pegar o nome do arquivo
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    // Validando extensão
                    if (ExtensionValidation(allowedExtensions, fileName))
                    {
                        var extension = ReturnExtension(fileName);
                        // Gerando nome único para o arquivo
                        var newName = $"{Guid.NewGuid()}.{extension}";
                        var completePath = Path.Combine(path, newName);

                        // Salvando arquivo
                        using (var stream = new FileStream(completePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return newName;
                    }
                }

                return "";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        // Remover Arquivo

        // Validar Extensões de Arquivo
        public static bool ExtensionValidation(string[] allowedExtensions, string fileName)
        {
            // Retorna a extensão do arquivo
            string extension = ReturnExtension(fileName);

            // Laço de repetição nas extensões permitidas
            foreach (string ext in allowedExtensions)
            {
                // Valida a extensão do arquivo
                if (ext == extension)
                {
                    return true;
                }
            }

            return false;
        }

        // Retornar a extensão do arquivo
        public static string ReturnExtension(string fileName)
        {
            string[] data = fileName.Split('.');
            // Retorna a última string da lista
            return data[data.Length - 1];
        }
    }
}
