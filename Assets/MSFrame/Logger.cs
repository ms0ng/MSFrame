using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MSFrame
{
	public enum LogTags
    {
		UI,
		AudioManager,
		Gameplay,
		Tag0,
		Tag1,
		Tag2,
		Tag3,
    }

	public class Logger
	{
		static HashSet<string> mLogTags = new HashSet<string>();

		static Logger()
		{

		}
		public static string getTimeHead()
		{
			return $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}] ";
		}

		public static void Open(string tag)
		{
			mLogTags.Add(tag);
		}

		public static void D(string tag, object obj)
		{
			if (!mLogTags.Contains(tag))
			{
				return;
			}
			Debug.Log(string.Format("<color=#245A1E>{0}</color>", obj));
		}

		public static void G(string tag, object obj)
		{
			if (!mLogTags.Contains(tag))
			{
				return;
			}
			Debug.Log(string.Format("<color=#00FF00>{0}</color>", obj));
		}

		public static void W(string tag, object obj)
		{
			if (!mLogTags.Contains(tag))
			{
				return;
			}
			Debug.LogWarning(obj);
		}

		public static void E(string tag, object obj)
		{
			if (!mLogTags.Contains(tag))
			{
				return;
			}
			Debug.LogError(obj);
		}
	}
}
