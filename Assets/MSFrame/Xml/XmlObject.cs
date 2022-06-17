using System.Collections;
using UnityEngine;

namespace MSFrame.Xml
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">ID</typeparam>
    public abstract class XmlObject<T>
    {
        protected T _identifyKey;

        public virtual T GetID() => _identifyKey;

        public static bool operator !(XmlObject<T> obj)
        {
            return obj == null;
        }

        public static explicit operator bool(XmlObject<T> obj)
        {
            return obj != null;
        }
    }
}