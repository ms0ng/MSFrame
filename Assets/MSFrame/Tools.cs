using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MSFrame
{
    public static class Tools
    {
        public static string SecondsToTimer(int seconds)
        {
            if (seconds < 0)
            {
                return "00:00:00";
            }
            int h = seconds / 3600;
            int m = (seconds - h * 3600) / 60;
            int s = seconds % 60;
            return string.Format($"{h.ToString("00")}:{m.ToString("00")}:{s.ToString("00")}");
        }

        public static string SecondsToMinuteTimer(int seconds)
        {
            if (seconds < 0)
            {
                return "00:00";
            }
            int h = seconds / 3600;
            int m = (seconds - h * 3600) / 60;
            int s = seconds % 60;
            return string.Format($"{m.ToString("00")}:{s.ToString("00")}");
        }

        public static string ToPercent(this float value, int num = 0)
        {
            string format = $"{{0:P{num}}}";
            return string.Format(format, value);
        }

        public static string ToPercent(this int value, int num = 0)
        {
            string format = $"{{0:P{num}}}";
            return string.Format(format, value);
        }

        public static string GetMd5Hash(string str)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        #region Parse String to Int
        public static Int16 ToInt16(this string value)
        {
            Int16 v = 0;
            if (Int16.TryParse(value, out v) == false)
            {
                Debug.LogError($"{value} can not convert to int16");
            }
            return v;
        }

        public static Int32 ToInt32(this string value)
        {
            Int32 v = 0;
            if (int.TryParse(value, out v) == false)
            {
                Debug.LogError($"{value} can not convert to int32");
            }
            return v;
        }

        public static Int64 ToInt64(this string value)
        {
            Int64 v = 0;
            if (Int64.TryParse(value, out v) == false)
            {
                Debug.LogError($"{value} can not convert to int64");
            }
            return v;
        }

        public static UInt16 ToUInt16(this string value)
        {
            UInt16 v = 0;
            if (UInt16.TryParse(value, out v) == false)
            {
                Debug.LogError($"{value} can not convert to uint16");
            }
            return v;
        }

        public static UInt32 ToUInt32(this string value)
        {
            UInt32 v = 0;
            if (UInt32.TryParse(value, out v) == false)
            {
                Debug.LogError($"{value} can not convert to uint32");
            }
            return v;
        }

        public static UInt64 ToUInt64(this string value)
        {
            UInt64 v = 0;
            if (UInt64.TryParse(value, out v) == false)
            {
                Debug.LogError($"{value} can not convert to uint64");
            }
            return v;
        }
        #endregion

        #region Vector
        public static Vector3 ClearX(this Vector3 v3)
        {
            v3.x = 0;
            return v3;
        }
        public static Vector3 ClearY(this Vector3 v3)
        {
            v3.y = 0;
            return v3;
        }
        public static Vector3 ClearZ(this Vector3 v3)
        {
            v3.z = 0;
            return v3;
        }
        public static Vector3 SetX(this Vector3 v3, float x)
        {
            v3.x = x;
            return v3;
        }
        public static Vector3 SetY(this Vector3 v3, float y)
        {
            v3.y = y;
            return v3;
        }
        public static Vector3 SetZ(this Vector3 v3, float z)
        {
            v3.z = z;
            return v3;
        }

        public static Vector2 ClearX(this Vector2 v2)
        {
            v2.x = 0;
            return v2;
        }
        public static Vector2 ClearY(this Vector2 v2)
        {
            v2.y = 0;
            return v2;
        }
        public static Vector2 SetX(this Vector2 v2, float x)
        {
            v2.x = x;
            return v2;
        }
        public static Vector2 SetY(this Vector2 v2, float y)
        {
            v2.y = y;
            return v2;
        }
        #endregion

        public static float GetXZDistance(this Vector3 start, Vector3 end)
        {
            return Vector3.Distance(start.ClearY(), end.ClearY());
        }
        public static float GetXZDistance(this Transform start, Transform target)
        {
            return Vector3.Distance(start.position.ClearY(), target.position.ClearY());
        }

        public static bool IsEqual(this float lhs, float rhs)
        {
            float num = lhs - rhs;
            return num < 1E-06f && num > -1E-06f;
        }

        public static void FindAllChildren(this Transform trans, List<Transform> children)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                children.Add(trans.GetChild(i));
                FindAllChildren(trans.GetChild(i), children);
            }
        }

        public static void ForeachChild(this Transform trans, Action<Transform> callback)
        {
            if (trans == null || callback == null) return;
            for (int i = 0; i < trans.childCount; i++)
            {
                callback(trans.GetChild(i));
                callback?.Invoke(trans.GetChild(i));
            }
        }

        public static int Sum(this IEnumerable<int> collection)
        {
            int num = 0;
            foreach (int current in collection)
            {
                unchecked
                {
                    num += (int)current;
                }
            }
            return num;
        }
        public static long Sum(this IEnumerable<long> collection)
        {
            long num = 0L;
            foreach (long current in collection)
            {
                unchecked
                {
                    num += current;
                }
            }
            return num;
        }

        public static GameObject Clone(this GameObject prefab)
        {
            if (prefab == null) return null;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
            Transform transform = prefab.transform;
            gameObject.transform.SetParent(transform.parent, false);
            gameObject.transform.localScale = transform.localScale;
            return gameObject;
        }

        public static List<T> Clone<T>(this List<T> list)
        {
            List<T> l = new List<T>();
            foreach (var item in list)
            {
                l.Add(item);
            }
            return l;
        }

        public static T GetLast<T>(this List<T> list)
        {
            if (list.Count < 1) return default(T);
            return list[list.Count - 1];
        }

        public static T GetFirst<T>(this List<T> list)
        {
            if (list.Count < 1) return default(T);
            return list[0];
        }

        public static bool IsSubClassOf(this Type type, Type targetType)
        {
            for (Type t = type; !t.Equals(typeof(object)); t = t.BaseType)
            {
                if (t.Equals(targetType)) return true;
            }
            return false;
        }

        public static T GetInstance<T>(this Type type, string propertyName = "Instance")
        {
            for (Type t = type; t != typeof(object); t = t.BaseType)
            {
                var property = t.GetProperty(propertyName);
                if (property == null) continue;
                var i = property.GetMethod.Invoke(null, null);
                if (i != null) return (T)i;
                break;
            }
            return default;
        }

        public static void UpwardAction(this Type type, Action<Type> action)
        {
            for (Type t = type; t != typeof(object); t = t.BaseType)
            {
                action.Invoke(t);
            }
        }
    }
}
