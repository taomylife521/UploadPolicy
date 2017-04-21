using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ND.PolicyReceiveService.Helper
{
   public class LogContext
    {
        public void AddLogInfo(string strPath, string txt)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            var fs = File.AppendText(strPath);
            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
        public void WriteDateTime(string strPath, string txt)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            var fs = File.CreateText(strPath);
            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

     
        public void AddLogInfo(string strPath, string txt, bool isAppend)
        {
            string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            StreamWriter fs;
            if (isAppend) fs = File.AppendText(strPath);
            else fs = File.CreateText(strPath);

            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public void CreateFile(string strPath, string txt)
        {
            string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

           

            FileStream fs = File.Create(strPath);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(txt);
            sw.Flush();
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }


        public string ReadDataLog(string strPath)
        {
            string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(strPath))
            {
                File.CreateText(strPath).Dispose();
            }

            string txt = File.ReadAllText(strPath, Encoding.UTF8);

            return txt;
        }
    }
}
