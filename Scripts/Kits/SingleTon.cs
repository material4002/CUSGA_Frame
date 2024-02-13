using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mat.Kits
{
    /// <summary>
    /// �̳���Mono�ĵ���ģʽ
    /// </summary>
    /// <typeparam name="T">ʹ�õ������������</typeparam>
    public class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance { get { return _instance; } }

        protected virtual void Awake()
        {
            _instance = this as T;
        }

        /// <summary>
        /// Ԥ��һ������������������ʹ��
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
                if (_instance == null) _instance = CreateInstance<T>();//����һ����ֹ����
                return _instance;
            }
        }
    }
    
    
}


