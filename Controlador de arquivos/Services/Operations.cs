using Controlador_de_arquivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador_de_arquivos.Services
{
    public class Operations
    {
        FileData fileData = new FileData();
        public void FileControl()
        {

            fileData.FilePath = @"C:\testegit.txt";
            fileData.base64Content = EncodeToBase64(fileData.FilePath);
            fileData.OriginalMD5 = GeradorMD5(fileData.FilePath);
            bool restore = true;
            while (true)
            {
                fileData.CurrentMD5 = GeradorMD5(fileData.FilePath);
                restore = Verificador(fileData.OriginalMD5, fileData.CurrentMD5);
                if (!restore)
                {
                    if (Restaurador(fileData.base64Content, fileData.FilePath))
                    {

                        Console.WriteLine("Arquivo Restaurado!\n");
                        //Console.Clear();
                    }

                }
                Thread.Sleep(2000);
            }
        }
        private static string GeradorMD5(string fileName)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(fileName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }
        private static string EncodeToBase64(string path)
        {
            try
            {
                byte[] pathToBytes = Encoding.UTF8.GetBytes(path);
                string base64 = System.Convert.ToBase64String(pathToBytes);
                return base64;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na codificação\n");
                throw;
            }
        }

        private bool Verificador(string originalMD5, string currentMD5)
        {
            if (currentMD5 != originalMD5)
            {
                return Login();
            }
            else
            {
                Console.WriteLine("Verificando...");
                return true;
            }

        }

        private bool Login()
        {
            string username, password;
            Console.WriteLine("\nPara confirmarmos a sua alteração, pedimos para que insira suas credenciais e assim verificaremos se você tem permissão para realizar a mudança.\n");
            Console.WriteLine("Insira o nome de usuário: ");
            username = Console.ReadLine();
            //Console.WriteLine("\n");
            Console.WriteLine("Insira a senha: ");
            password = Console.ReadLine();
            //Console.WriteLine("\n");

            if (username == "marcosh" && password == "1234")
            {
                Console.WriteLine("Alteração sendo realizada...\n");
                fileData.OriginalMD5 = GeradorMD5(fileData.FilePath);
                fileData.base64Content = EncodeToBase64(fileData.FilePath);
                Thread.Sleep(1000);
                Console.WriteLine("Alteração realizada com sucesso!\n");
                return true;
            }
            else
            {
                Console.WriteLine("Usuario/Senha incorretos. O arquivo está sendo restaurado...\n");
                return false;
            }
        }
        private static bool Restaurador(string base64Content, string filePath)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64Content);
                string decodeString = Encoding.UTF8.GetString(bytes);
                File.WriteAllText(filePath, decodeString);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao restaurar arquivo: " + ex.Message + "\n");
                return false;
            }
        }
    }
}
