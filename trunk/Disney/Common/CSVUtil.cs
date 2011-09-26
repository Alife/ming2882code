using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    /// <summary>
    /// CSV文件操作类
    /// </summary>
    public class CSVUtil
    {
        private CSVUtil()
        {
        }
        /// <summary>
        /// 写一个csv文件
        /// </summary>
        /// <param name="filePathName"></param>
        /// <param name="ls"></param>
        public static void WriteCSV(string filePathName, List<String[]> ls)
        {
            WriteCSV(filePathName, false, ls);
        }
        /// <summary>
        /// 写一个csv文件
        /// </summary>
        /// <param name="filePathName"></param>
        /// <param name="append"></param>
        /// <param name="ls"></param>
        public static void WriteCSV(string filePathName, bool append, List<String[]> ls)
        {
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
            foreach (String[] strArr in ls)
            {
                fileWriter.WriteLine(String.Join(",", strArr));
            }
            fileWriter.Flush();
            fileWriter.Close();
        }
        /// <summary>
        /// 读一个csv文件
        /// </summary>
        /// <param name="filePathName"></param>
        /// <returns></returns>
        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader = new StreamReader(filePathName);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine.Split(','));
                }
            }
            fileReader.Close();
            return ls;
        }
    }
}