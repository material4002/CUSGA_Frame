using JetBrains.Annotations;
using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mat
{
	/// <summary>
	/// 为降低代码耦合度，使用事件管理器对事件进行管理
	/// </summary>
	public class Events:SingleTon<Events>
	{
		Dictionary<string, Dictionary<int, Action<EventArgs>>> dic;

		public Events():base()
		{
			dic = new Dictionary<string, Dictionary<int, Action<EventArgs>>>();
		}

		/// <summary>
		/// 订阅事件
		/// </summary>
		/// <param name="name"></param>
		/// <param name="listener"></param>
		/// <returns></returns>
		public bool AddListener(string name, Action<EventArgs> listener)
		{
			if (dic.ContainsKey(name))
			{
				int code = listener.GetHashCode();
				if (dic[name].ContainsKey(code))
				{
					return false;
				}
				else
				{
					dic[name].Add(code, listener);
					return true;
				}
			}
			else
			{
				int code = listener.GetHashCode();
				dic[name] = new Dictionary<int, Action<EventArgs>>();
				dic[name].Add(code, listener);
				return true;
			}
		}

		/// <summary>
		/// 取消订阅事件
		/// </summary>
		/// <param name="name"></param>
		/// <param name="listener"></param>
		/// <returns></returns>
		public bool RemoveListener(string name,Action<EventArgs> listener)
		{
			if(dic.ContainsKey(name))
			{
				int code = listener.GetHashCode();
				if (dic[name].ContainsKey(code))
				{
					dic[name].Remove(code);
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
		/// 批量移除事件中的场景监听器
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool RemoveListeners(string name)
		{
			if (dic.ContainsKey(name))
			{
				foreach(var i in dic[name].Keys)
				{
					if (dic[name][i].GetHashCode() == i)
					{
						dic[name].Remove(i);
					}
				}
				if (dic[name].Count== 0)
				{
					dic.Remove(name);
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 不随场景销毁的监听器订阅事件
		/// </summary>
		/// <param name="name"></param>
		/// <param name="listener"></param>
		/// <returns></returns>
		public bool AddGlobalListener(string name, Action<EventArgs> listener)
		{
			if (dic.ContainsKey(name))
			{
				int code = -listener.GetHashCode();
				if (dic[name].ContainsKey(code) )
				{
					return false;
				}
				else
				{
					dic[name].Add(code, listener);
					return true;
				}
			}
			else
			{
				int code = -listener.GetHashCode();
				dic.Add(name,new Dictionary<int, Action<EventArgs>>());
				dic[name].Add(code, listener);
				return true;
			}
		}

		/// <summary>
		/// 不随场景销毁的监听器取消订阅
		/// </summary>
		/// <param name="name"></param>
		/// <param name="listener"></param>
		/// <returns></returns>
		public bool RemoveGlobalListener(string name, Action<EventArgs> listener)
		{
			if (dic.ContainsKey(name))
			{
				int code = -listener.GetHashCode();
				if (dic[name].ContainsKey(code))
				{
					dic[name].Remove(code);
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
		/// 激活事件
		/// </summary>
		/// <param name="name"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public bool TriggerEvent(string name,EventArgs args)
		{
			if (!dic.ContainsKey(name)) return false;
			foreach(var i in dic[name].Values)
			{
				i(args);
			}
			return true;
		}

		/// <summary>
		/// 清除所有事件
		/// </summary>
		public void Clear()
		{
			dic.Clear();
		}

		/// <summary>
		/// 只清除场景中订阅的事件（HashCode与Key相同）
		/// </summary>
		public void ClearSceneEvent()
		{
			foreach(var i in dic.Values)
			{
				foreach(var j in i.Keys)
				{
					if (i[j].GetHashCode()== j)
					{
						i.Remove(j);
					}
				}
			}
		}
	}

	/// <summary>
	/// 通过继承该类储存参数
	/// </summary>
	/*public class EventArgs
	{
		public EventArgs() { }
	}*/

	
}

/// <summary>
/// 将激活事件函数拓展至object，简化操作
/// </summary>
public static class EventTrigger
{
	public static void TriggerEvent(this object sender, string name, EventArgs args)
	{
		Mat.Events.Instance.TriggerEvent(name, args);
	}
}