
using UnityEngine;

namespace MSFrame.UI
{
    public interface IWindowEntity
    {
        public GameObject WindowPrefab { get; }
        public string WindowName { get; }

        public void OnWindowOpen(object[] _params);
        public void OnWindowClose();
    }
}
