using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mat
{
    /// <summary>
    /// ������ȫ�֣���Ϊ��������
    /// �ڴ˳�ʼ��ȫ�������ݣ��Ա���Ƽ���˳��
    /// </summary>
    public class Entry : MonoSingleTon<Entry> {

        Events events = null;
		Inputs inputs = null;
        ManagerList managerList =null;
        Debuger debuger= null;

		protected override void Awake()
		{
			base.Awake();
            DontDestroyOnLoad(this);//��Ϊȫ�ֱ������ܱ��ƻ�

			events= new Events();
            managerList= new ManagerList();
			inputs= new Inputs();
            debuger= new Debuger();
		}

		private void Start()
		{
			LevelManager.Instance.LoadScene(1);
			/*Events.Instance.AddListener("Move", (e) => {
				var e2 = (ArgsInput)e;
				print(e2.context.ReadValue<Vector2>()); });*/
		}

		private void Update()
		{
			
		}

	}
}

