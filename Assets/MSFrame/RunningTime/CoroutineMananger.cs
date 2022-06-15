using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFrame.RunningTime
{
    public class CoroutineInfomation
    {
        public int nameHash;
        public IEnumerator routine;
        public Coroutine coroutine;
        public Action<CoroutineInfomation> onComplete;
        public bool isRunning;

        public CoroutineInfomation(string name, IEnumerator routine, Action<CoroutineInfomation> onComplete = null)
        {
            nameHash = name.GetHashCode();
            this.routine = routine;
            this.onComplete = onComplete;
        }
        public CoroutineInfomation Start() => CoroutineMananger.Instance.StartCoroutine(this);
        public CoroutineInfomation Stop(bool removeFromDict = true) => CoroutineMananger.Instance.StopCoroutine(this, removeFromDict);
    }

    public class CoroutineMananger : MonoSingleton<CoroutineMananger>
    {
        private const string WARNNING = "You should not use CoroutineMananger.Instance.StartCoroutine() directly, this corroutine will not be managed by CoroutineManager.";
        private Dictionary<int, CoroutineInfomation> _coroutines = new Dictionary<int, CoroutineInfomation>();

        public CoroutineInfomation ScheduleCoroutine(string identity, IEnumerator routine, Action<CoroutineInfomation> onComplete = null)
        {
            if (_coroutines.ContainsKey(identity.GetHashCode()))
            {
                Debug.LogError("There is a coroutine running with the same name.");
                return null;
            }
            CoroutineInfomation info = new CoroutineInfomation(identity, routine, onComplete);
            _coroutines.Add(identity.GetHashCode(), info);
            return info;
        }

        public CoroutineInfomation GetCoroutineInfomation(string identity)
        {
            CoroutineInfomation res;
            _coroutines.TryGetValue(identity.GetHashCode(), out res);
            return res;
        }

        public CoroutineInfomation StartCoroutine(CoroutineInfomation corInfomation)
        {
            corInfomation.coroutine = base.StartCoroutine(CoroutineHolder(corInfomation));
            return corInfomation;
        }

        public CoroutineInfomation StopCoroutine(CoroutineInfomation corInfomation, bool removeFromDict = true)
        {
            StopCoroutine(corInfomation.coroutine);
            corInfomation.isRunning = false;
            if (removeFromDict) _coroutines.Remove(corInfomation.nameHash);
            return corInfomation;
        }

        private IEnumerator CoroutineHolder(CoroutineInfomation corInfo)
        {
            corInfo.isRunning = true;
            yield return base.StartCoroutine(corInfo.routine);
            corInfo.isRunning = false;
            corInfo.onComplete?.Invoke(corInfo);

            _coroutines.Remove(corInfo.nameHash);
        }

        [Obsolete(WARNNING, false)]
        public new void StartCoroutine(IEnumerator routine)
        {
            Debug.LogWarning(WARNNING);
            base.StartCoroutine(routine);
        }
        [Obsolete(WARNNING, false)]
        public new void StartCoroutine(string methodName)
        {
            Debug.LogWarning(WARNNING);
            base.StartCoroutine(methodName);
        }
        [Obsolete(WARNNING, false)]
        public new void StartCoroutine(string methodName, object value)
        {
            Debug.LogWarning(WARNNING);
            base.StartCoroutine(methodName, value);
        }
    }
}