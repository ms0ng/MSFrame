using System;
using System.Collections.Generic;
using UnityEngine;

namespace MSFrame.UI
{
    // 窗体的全局管理类
    public class WindowsManager : Singleton<WindowsManager>
    {
        private Dictionary<string, Type> _registeredWindows = new Dictionary<string, Type>();
        private List<IWindowEntity> mOpenedWindows = new List<IWindowEntity>();

        private Transform _SpawnTransform;

        public override void Init() => Init("Window");

        public void Init(string transformName) => Init(GameObject.Find(transformName).transform);

        public void Init(Transform transform)
        {
            RegisterWindows();
            _SpawnTransform = transform;
        }

        public void Hide(string windowName)
        {

            if (!IsOpen(windowName))
            {
                return;
            }
            var obj = Get(windowName);
            mOpenedWindows.Remove(obj);
            GameObject.Destroy(((MonoBehaviour)obj).gameObject);
        }

        public bool IsOpen(string windowName)
        {
            if (Get(windowName) != null) return true;
            return false;
        }

        public IWindowEntity Get()
        {
            if (mOpenedWindows.Count < 1) return null;
            return mOpenedWindows[mOpenedWindows.Count - 1];
        }

        public IWindowEntity Get(string windowName)
        {
            return GetAs<IWindowEntity>(windowName);
        }

        public T GetAs<T>(string windowName) where T : IWindowEntity
        {
            for (int i = 0; i < mOpenedWindows.Count; i++)
            {
                if (mOpenedWindows[i] == null)
                {
                    mOpenedWindows.RemoveAt(i);
                    i--;
                    continue;
                }
                if (windowName.Equals(mOpenedWindows[i].GetType().Name))
                    return (T)mOpenedWindows[i];
            }
            return default(T);
        }

        public IWindowEntity Open(string windowName)
        {
            if (IsOpen(windowName)) return Get(windowName);
            IWindowEntity window = (IWindowEntity)_registeredWindows[windowName];
            window = GameObject.Instantiate(GetWindowPrefab(window), _SpawnTransform).GetComponent<IWindowEntity>();
            mOpenedWindows.Add(window);
            return window;
        }

        public virtual GameObject GetWindowPrefab(IWindowEntity window)
        {
            return window.WindowPrefab;
        }

        private void RegisterWindows()
        {
            var iWindow = typeof(IWindowEntity).FullName;
            var types = typeof(IWindowEntity).Assembly.GetTypes();
            foreach (var t in types)
            {
                if (!t.IsClass || t.IsAbstract) continue;
                if (t.GetInterface(iWindow) != null)
                    _registeredWindows[t.Name] = t;
            }
        }
    }
}

