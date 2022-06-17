using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFrame.RunningTime
{
    public class ActionQueue
    {

        private List<Action> _queueActions = new List<Action>();

        public void Append(Action action)
        {
            _queueActions.Add(action);
        }

        public void Run()
        {
            foreach (var action in _queueActions)
            {
                action.Invoke();
            }
            _queueActions.Clear();
        }
    }
}