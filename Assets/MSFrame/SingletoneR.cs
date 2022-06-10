using System;
using System.Collections;
using UnityEngine;

namespace MSFrame
{
    /// <summary>
    /// 单例，采用反射。稍微增加内存和性能消耗，但是支持构造方法私有。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletoneR<T> : IDisposable where T : class, new()
    {

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type type = typeof(T);
                    //构造函数是私有或者静态，并且构造函数⽆参
                    var constructorInfos = type.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    foreach (var info in constructorInfos)
                    {
                        var @params = info.GetParameters();
                        if (@params.Length == 0)
                        {
                            //调用无参构造函数
                            instance = (T)info.Invoke(null);
                            break;
                        }
                    }

                    if (instance == null)
                    {
                        throw new NotSupportedException("实例化单例出错：没有非公开的无参构造函数");
                    }
                }
                return instance;
            }
        }
        public virtual void Init() { }
        public virtual void Dispose()
        {
            //GC
        }
    }
}