using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistraPontoService
{
    public class Log
    {
        public static void LogRegistraPonto(string descricao)
        {
            StreamWriter vWriter = new StreamWriter(@"C:\PCFCustom\Projetos\"+ DateTime.Now.ToString("ddMMyyyy") +"-RegistraPonto.txt", true);
            vWriter.WriteLine(descricao + " : " + DateTime.Now.ToString());
            vWriter.Flush();
            vWriter.Close();
        }
    }
}
