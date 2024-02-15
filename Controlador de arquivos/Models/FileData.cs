using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador_de_arquivos.Models
{
    public class FileData
    {
        public string FilePath { get; set; }
        public string OriginalMD5 { get; set; }
        public string CurrentMD5 { get; set; }
        public string base64Content { get; set; }

    }
}
