namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public class GetEnumBLL
    {
        public static string GetEnumDescription(object enumSubitem)
        {
            enumSubitem = (Enum) enumSubitem;
            string name = enumSubitem.ToString();
            FieldInfo field = enumSubitem.GetType().GetField(name);
            if (field != null)
            {
                object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((customAttributes == null) || (customAttributes.Length == 0))
                {
                    return name;
                }
                DescriptionAttribute attribute = (DescriptionAttribute) customAttributes[0];
                return attribute.Description;
            }
            return "不限";
        }

        public static List<string[]> GetEnumOpt(Type type)
        {
            List<string[]> list = new List<string[]>();
            FieldInfo[] fields = type.GetFields();
            int index = 1;
            int length = fields.Length;
            while (index < length)
            {
                string[] strArray = new string[3];
                FieldInfo info = fields[index];
                strArray[1] = ((int) Enum.Parse(type, info.Name)).ToString();
                strArray[2] = info.Name;
                object[] customAttributes = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((customAttributes == null) || (customAttributes.Length == 0))
                {
                    strArray[0] = info.Name;
                }
                else
                {
                    DescriptionAttribute attribute = (DescriptionAttribute) customAttributes[0];
                    strArray[0] = attribute.Description;
                }
                list.Add(strArray);
                index++;
            }
            return list;
        }
    }
}
