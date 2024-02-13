using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mat
{
	/// <summary>
	/// ������������ʹ���첽���ط�ֹ����
	/// ʹ��Э��
	/// </summary>
	public class LevelManager : SingleTon<LevelManager>
	{
		public LevelManager():base()
		{
			
			
		}

		//����
		public readonly int ENTRY = 0;
		public readonly int COUNT = SceneManager.sceneCount;

		//ӵ�������¼���Loading���ؽ��̣�IsDone�������
		public const string Loading = "S_Loading";
		public const string IsDone = "S_IsDone";
		public const string Start  = "S_Start";

		//��ǰ����
		private int _currentScene = 0;
		public int currentScent { get { return _currentScene; } }

		//��ⳡ���Ƿ��������
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

		#region Э�̷���

		//������Ϣ
		AsyncOperation operation = null;

		//���س���������Э��
		public bool LoadScene(int scene)
		{
			if (IsScene(scene)) return false;
			if (operation != null) return false;
			this.TriggerEvent(Start, new EventArgs());
			Entry.Instance.StartCoroutine(LoadIE(scene));
			return true;
		}

		//Э���ں�
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

