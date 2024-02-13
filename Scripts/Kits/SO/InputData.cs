using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mat.Kits
{
	[CreateAssetMenu(fileName ="InputData",menuName ="ScriptableObject/InputData",order = 1)]
	public class InputData : SO_SingleTon<InputData>
	{
		private Vector2 _MousePos= Vector2.zero;
		private Vector2 _MoveOffset = Vector2.zero;

		public Vector2 MousePos { get { return _MousePos; } }
		public Vector2 MoveOffset { get { return _MoveOffset; } }


	}
}

