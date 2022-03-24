using System;
using System.Collections.Generic;
using UnityEngine;

namespace MSFrame.UI
{
    public class WindowsManager : Singleton<WindowsManager>
    {
        private List<Type> _registeredWindows = new List<Type>();
        private Dictionary<int, IWindowEntity> _openedWindows = new Dictionary<int, IWindowEntity>();

        private Transform _SpawnTransform;

        public override void Init() => Init("Window");

        public void Init(string transformName) => Init(GameObject.Find(transformName).transform);

        public void Init(Transform transform)
        {
            RegisterWindows();
            _SpawnTransform = transform;
        }

        private List<Type> RegisterWindows()
        {
            Type[] types = typeof(IWindowEntity).Assembly.GetTypes();
            string iWindow = typeof(IWindowEntity).FullName;
            foreach (var t in types)
            {
                if (!t.IsClass || t.IsAbstract) continue;
                if (t.GetInterface(iWindow) == null) continue;

                _registeredWindows.Add(t);
            }
            return _registeredWindows;
        }

        public IWindowEntity Open(string windowName, object[] @params = null)
        {
            int key = windowName.GetHashCode();
            if (_openedWindows.TryGetValue(key, out IWindowEntity entity)) return entity;

            _registeredWindows.ForEach((type) =>
            {
                IWindowEntity e = (IWindowEntity)type;
                if (e.WindowName.Equals(windowName)) entity = (IWindowEntity)type;
            });
            if (entity == null) return null;

            entity = GameObject.Instantiate(entity.WindowPrefab, _SpawnTransform).GetComponent<IWindowEntity>();
            entity.OnWindowOpen(@params);
            _openedWindows.Add(key, entity);
            return entity;
        }

        public void Close(string windowName)
        {
            IWindowEntity entity = Get(windowName);
            if (entity == null) return;
            _openedWindows.Remove(windowName.GetHashCode());
            entity.OnWindowClose();
            GameObject.Destroy(((MonoBehaviour)entity).gameObject);
        }

        public IWindowEntity Get(string windowName) => GetAs<IWindowEntity>(windowName);

        public T GetAs<T>(string windowName) where T : IWindowEntity
        {
            if (_openedWindows.TryGetValue(windowName.GetHashCode(), out IWindowEntity entity)) return (T)entity;
            return default(T);
        }
    }
}

