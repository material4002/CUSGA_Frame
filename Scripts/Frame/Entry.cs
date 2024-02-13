using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mat
{
    /// <summary>
    /// 放置在全局，作为程序的入口
    /// 在此初始化全部的内容，以便控制加载顺序
    /// </summary>
    public class Entry : MonoSingleTon<Entry> {

        Events events = null;
		Inputs inputs = null;
        ManagerList managerList =null;
        Debuger debuger= null;

		protected override void Awake()
		{
			base.Awake();
            DontDestroyOnLoad(this);//作为全局变量不能被破坏

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

