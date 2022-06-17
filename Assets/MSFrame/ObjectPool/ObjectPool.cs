using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFrame
{
    /// <summary>
    /// 对象池。Unity 2021已内置有对象池<see cref="UnityEngine.Pool.ObjectPool{T}"/>，推荐使用
    /// </summary>
    public class ObjectPool<T>
    {
        public int CountAll => _objActiveList.Count + _objInActiveList.Count;
        public int CountActive => _objActiveList.Count;
        public int CountInactive => _objInActiveList.Count;

        private Func<T> _createFunc;
        private Action<T> _onGet;
        private Action<T> _onRelease;
        private Action<T> _onDestroy;
        private int _capacity;

        private List<T> _objInActiveList;
        private List<T> _objActiveList;
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="createFunc">创建一个<see cref="T"/>的方法</param>
        /// <param name="onGet">获取对象的委托。请注意可能需要对对象再初始化或清除之前遗留的脏数据。</param>
        /// <param name="onRelease">释放对象的委托。</param>
        /// <param name="onDestroy">销毁对象的委托。</param>
        /// <param name="capacity">该对象池的标准容量。</param>
        public ObjectPool(Func<T> createFunc,
            Action<T> onGet = null,
            Action<T> onRelease = null,
            Action<T> onDestroy = null,
            int capacity = 10)
        {
            this._createFunc = createFunc;
            this._onGet = onGet;
            this._onRelease = onRelease;
            this._onDestroy = onDestroy;
            this._capacity = capacity;
            this._objInActiveList = new List<T>();
            this._objActiveList = new List<T>();
        }
        /// <summary>
        /// 获取一个对象，如果没有足够的未激活对象，则创建一个新的
        /// </summary>
        /// <returns><see cref="T"/></returns>
        public T Get()
        {
            T t;
            if (_objInActiveList.Count == 0)
            {
                t = _createFunc.Invoke();
            }
            else
            {
                t = _objInActiveList[0];
                _objInActiveList.RemoveAt(0);
            }
            _objActiveList.Add(t);
            _onGet.Invoke(t);
            return t;
        }
        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="t">欲释放的对象</param>
        public void Release(T t)
        {
            int index = -1;
            for (int i = 0; i < _objActiveList.Count; i++)
            {
                if (_objActiveList[i].Equals(t))
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                Debug.LogWarning($"{GetType().Name}.{nameof(Release)}: 不存在可被Realese的{typeof(T).Name}实例，请检查调用栈");
                return;
            }
            _onRelease.Invoke(t);
            _objActiveList.RemoveAt(index);
            _objInActiveList.Add(t);
            ShrinkOnce();
        }
        /// <summary>
        /// 尽可能销毁一个未激活的<see cref="T"/>直至对象数量降至标准容量
        /// </summary>
        private void ShrinkOnce()
        {
            if (_objInActiveList.Count > 0 && (_objActiveList.Count + _objInActiveList.Count) > _capacity)
            {
                T t = _objInActiveList[0];
                _objInActiveList.RemoveAt(0);
                _onDestroy.Invoke(t);
            }
        }
        /// <summary>
        /// 销毁未激活状态的<see cref="T"/>直至对象数量尽可能的降低至该对象池的标准容量。
        /// </summary>
        public void Shrink()
        {
            for (int i = 0; i < _objInActiveList.Count; i++)
            {
                if (_objInActiveList.Count > 0 && (_objInActiveList.Count + _objActiveList.Count) <= _capacity) break;
                T t = _objInActiveList[0];
                _objInActiveList.RemoveAt(0);
                _onDestroy.Invoke(t);
            }
        }
        /// <summary>
        /// 释放并销毁所有<see cref="T"/>
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _objActiveList.Count; i++)
            {
                T t = _objActiveList[0];
                _objActiveList.RemoveAt(0);
                _onRelease.Invoke(t);
                _objInActiveList.Add(t);
            }

            for (int i = 0; i < _objInActiveList.Count; i++)
            {
                T t = _objInActiveList[0];
                _onDestroy.Invoke(t);
                _objInActiveList.RemoveAt(0);
            }
        }
    }
}