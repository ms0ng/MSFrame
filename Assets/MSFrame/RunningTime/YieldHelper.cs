using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFrame.RunningTime
{
    public class YieldHelper
    {
        public static Dictionary<float, WaitForSeconds> _waitForSecs;
        public static WaitForSeconds WaitFor(float seconds)
        {
            if (_waitForSecs == null) _waitForSecs = new Dictionary<float, WaitForSeconds>();
            if (_waitForSecs.TryGetValue(seconds, out var waitForSeconds))
            {
                return waitForSeconds;
            }
            else
            {
                WaitForSeconds wait = new WaitForSeconds(seconds);
                _waitForSecs[seconds] = wait;
                return wait;
            }

        }
    }
}