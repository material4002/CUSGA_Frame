using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mat.Kits
{
    /// <summary>
    /// 继承于Mono的单例模式
    /// </summary>
    /// <typeparam name="T">使用单例的类的类名</typeparam>
    public class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance { get { return _instance; } }

        protected virtual void Awake()
        {
            _instance = this as T;
        }

        /// <summary>
        /// 预留一个函数，当作构造器使用
        /// </summary>
        protected virtual void Init()
        {
			_instance = this as T;
		}
    }

    public class SingleTon<T>:Object  where T:Object
    {
        public static T _instance;
        public static T Instance { get { return _instance;} }

        public SingleTon()
        {
            _instance = this as T;
        }
    }

    public class SO_SingleTon<T> :ScriptableObject where T : ScriptableObject
    {
        private static string _path = "SO/"+typeof(T).Name;
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<T>(_path);
                if (_instance == null) _instance = CreateInstance<T>();//创建一个防止报错
                return _instance;
            }
        }
    }
    
    
}


