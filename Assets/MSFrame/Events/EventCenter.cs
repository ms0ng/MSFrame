using System;
using System.Collections.Generic;

namespace MSFrame.Events
{
    class EventCenter
    {
        /// <summary>
        /// 事件注册列表
        /// </summary>
        private Dictionary<string, Dictionary<int, Action>> mEvents = new Dictionary<string, Dictionary<int, Action>>();


        /// <summary>
        /// 注册监听
        /// </summary>
        public int Register(string eventname, Action handler)
        {
            if (!mEvents.ContainsKey(eventname))
            {
                mEvents.Add(eventname, null);
            }
            int handerHash = handler.GetHashCode();

            if (mEvents.TryGetValue(eventname, out Dictionary<int, Action> actions))
            {
                if (actions.ContainsKey(handerHash))
                {
                    //重复的方法
                    return -1;
                }
                else
                {
                    actions.Add(handerHash, handler);
                    return handerHash;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        public bool RemoveRegister(string eventname, Action handler)
        {
            if (!mEvents.ContainsKey(eventname)) return false;
            if (mEvents.TryGetValue(eventname, out Dictionary<int, Action> actions))
            {
                if (actions.ContainsKey(handler.GetHashCode()))
                {
                    actions.Remove(handler.GetHashCode());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
	    public bool FireEvent(string eventname)
        {
            if (mEvents.TryGetValue(eventname, out Dictionary<int, Action> actions))
            {
                foreach (var kvPair in actions)
                {
                    kvPair.Value?.Invoke();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
