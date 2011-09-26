using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Common
{
    public sealed class SerializeDeserialize
    {
        private static readonly SerializeDeserialize Instance = new SerializeDeserialize();

        private SerializeDeserialize()
        {
        }
        public static string HtmlEncode(string strVal)
        {
            if (!string.IsNullOrEmpty(strVal))
            {
                strVal = HttpUtility.HtmlEncode(strVal);
            }
            return strVal;
        }
        /// <summary>
        /// ���л�����������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>����һ����������л����MemoryStream����</returns>
        public static string SerializeObject(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            string str = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                byte[] buffer = new byte[stream.Length];
                str = Convert.ToBase64String(stream.ToArray());
                stream.Flush();
            }
            return str;
        }
        /// <summary>
        /// �����л�����������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object DeserializeObject(string str)
        {
            IFormatter formatter = new BinaryFormatter();
            byte[] buffer = Convert.FromBase64String(str);
            using (Stream stream = new MemoryStream(buffer, 0, buffer.Length))
            {
                return formatter.Deserialize(stream);
            }
        }
    }
}
