using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mat
{
	/// <summary>
	/// 场景加载器，使用异步加载防止卡顿
	/// 使用协程
	/// </summary>
	public class LevelManager : SingleTon<LevelManager>
	{
		public LevelManager():base()
		{
			
			
		}

		//常量
		public readonly int ENTRY = 0;
		public readonly int COUNT = SceneManager.sceneCount;

		//拥有两个事件：Loading加载进程，IsDone加载完成
		public const string Loading = "S_Loading";
		public const string IsDone = "S_IsDone";
		public const string Start  = "S_Start";

		//当前场景
		private int _currentScene = 0;
		public int currentScent { get { return _currentScene; } }

		//检测场景是否符合条件
		private bool IsScene(int scene)
		{
			if(scene >ENTRY && scene < COUNT)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#region 协程方面

		//储存信息
		AsyncOperation operation = null;

		//加载场景，建立协程
		public bool LoadScene(int scene)
		{
			if (IsScene(scene)) return false;
			if (operation != null) return false;
			this.TriggerEvent(Start, new EventArgs());
			Entry.Instance.StartCoroutine(LoadIE(scene));
			return true;
		}

		//协程内核
		IEnumerator LoadIE(int index)
		{
			operation=SceneManager.LoadSceneAsync(index,LoadSceneMode.Single);
			operation.allowSceneActivation = true;
			
			while (operation.progress < 1f)
			{
				this.TriggerEvent(Loading, new ProgressArgs { progress = operation.progress });
				yield return null;
			}
			yield return operation;
			
			this.TriggerEvent(IsDone, new EventArgs());
			operation = null;
			_currentScene = index;
		}

		
		#endregion

	}

	 public class ProgressArgs : EventArgs
	{
		public float progress;
	}

	
}

