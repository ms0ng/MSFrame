using UnityEngine;

namespace MSFrame
{
    /// <summary>
    /// MonoBehaviour单例
    /// </summary>
    /// <typeparam name="T">单例类型</typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
                return instance;
            }
        }

        public virtual void Awake()
        {
            instance = this as T;
        }

        public virtual void Init() { }
    }

}
