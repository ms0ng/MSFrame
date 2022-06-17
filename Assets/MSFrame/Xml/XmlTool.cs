using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace MSFrame.Xml
{
    public static class XmlTool
    {
        public static int GetInt(this XmlElement element, string name)
        {
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return 0;
            int.TryParse(s, out int v);
            return v;
        }
        public static float GetFloat(this XmlElement element, string name)
        {
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return 0;
            float.TryParse(s, out float v);
            return v;
        }
        public static string GetString(this XmlElement element, string name)
        {
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return s;
        }
        public static bool GetBool(this XmlElement element, string name)
        {
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return false;
            return s.Equals("1")
                || s.Equals("true")
                || s.Equals("TRUE")
                || s.Equals("True");
        }

        public static Vector4 GetVector(this XmlElement element, string name)
        {
            //string be like: (2.5,2,34)  or  2.5,2,34
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return Vector4.zero;

            string[] array = s.Split(',');
            if (array.Length == 0) return Vector4.zero;

            int xIndex = 0, yIndex = xIndex + 1, zIndex = yIndex + 1, wIndex = zIndex + 1;

            float x = 0, y = 0, z = 0, w = 0;
            if (array.Length > xIndex) float.TryParse(array[xIndex], out x);
            if (array.Length > yIndex) float.TryParse(array[yIndex], out y);
            if (array.Length > zIndex) float.TryParse(array[zIndex], out z);
            if (array.Length > wIndex) float.TryParse(array[wIndex], out w);
            return new Vector4(x, y, z, w);
        }

        public static List<T> GetList<T>(this XmlElement element, string name, Func<string, T> converter = null)
        {
            //input be like:(1,2,3,4,5,6)
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return null;

            if (s.StartsWith("(")) s = s.Substring(1);
            if (s.EndsWith(")")) s = s.Substring(0, s.Length - 1);
            string[] array = s.Split(',');
            if (array.Length < 1) return null;

            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (converter == null)
                {
                    list.Add((T)Convert.ChangeType(array[i], typeof(T)));
                }
                else
                {
                    list.Add(converter.Invoke(array[i]));
                }
            }
            return list;
        }

        public static T GetEnum<T>(this XmlElement element, string name) where T : Enum
        {
            string s = element.GetAttribute(name);
            if (string.IsNullOrEmpty(s)) return default(T);
            return (T)Enum.Parse(typeof(T), s);
        }
    }
}