using JetBrains.Annotations;
using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mat
{
	/// <summary>
	/// Ϊ���ʹ�����϶ȣ�ʹ���¼����������¼����й���
	/// </summary>
	public class Events:SingleTon<Events>
	{
		Dictionary<string, Dictionary<int, Action<EventArgs>>> dic;

		public Events():base()
		{
			dic = new Dictionary<string, Dictionary<int, Action<EventArgs>>>();
		}

		/// <summary>
		/// �����¼�
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
		/// ȡ�������¼�
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
		/// �����Ƴ��¼��еĳ���������
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
		/// ���泡�����ٵļ����������¼�
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
		/// ���泡�����ٵļ�����ȡ������
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
		/// �����¼�
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
		/// ��������¼�
		/// </summary>
		public void Clear()
		{
			dic.Clear();
		}

		/// <summary>
		/// ֻ��������ж��ĵ��¼���HashCode��Key��ͬ��
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
	/// ͨ���̳и��ഢ�����
	/// </summary>
	/*public class EventArgs
	{
		public EventArgs() { }
	}*/

	
}

/// <summary>
/// �������¼�������չ��object���򻯲���
/// </summary>
public static class EventTrigger
{
	public static void TriggerEvent(this object sender, string name, EventArgs args)
	{
		Mat.Events.Instance.TriggerEvent(name, args);
	}
}