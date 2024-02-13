using Mat.Kits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mat
{
	public class ManagerList : SingleTon<ManagerList>
	{
		public ManagerList():base() {
			_level = new LevelManager();
			_audio= new AudioManager();
		}

		private LevelManager _level = null;
		public LevelManager level { get { return _level; } }
		private AudioManager _audio = null;
		public AudioManager audio { get { return _audio;} }
		
	}
}

