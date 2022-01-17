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
            return -1;
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        public void RemoveRegister(string eventname)
        {

        }

        /// <summary>
        /// 触发事件
        /// </summary>
	    public void FireEvent(string eventname)
        {

        }
    }
}
