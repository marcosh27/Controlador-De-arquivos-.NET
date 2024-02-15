using Controlador_de_arquivos.Models;
using Controlador_de_arquivos.Services;

namespace Controlador_de_arquivos
{
    static class Program
    {
        static void Main()
        {
            Operations app = new Operations();
            app.FileControl();
        }
    }
}