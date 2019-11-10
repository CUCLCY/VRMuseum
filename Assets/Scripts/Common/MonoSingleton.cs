using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 单例模板类
    /// </summary>
    //Where T: 类   指的是约束T只能是该类类型或者该类的派生类
	public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
    {
        //public static T Instance { get; private set; }

        //private void Awake()
        //{
        //    Instance = this as T;
        //}
        
        //按需加载
        private static T instance;
        public static T Instance
        {
            get
            {
                //为instance赋值
                //instance = this;
                if (instance == null)
                {
                    //在场景中查找T类型引用
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        //立即执行Awake
                        new GameObject("Singleton of  " + typeof(T).Name).AddComponent<T>();
                    }
                    else
                    {
                        //通知子类进行初始化
                        instance.Init();
                    }
                }
                return instance;
            }
        }

        protected virtual void Init() { }
         
        protected void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        }

        /*
         目标: 让客户端代码在何时何地都可以正确的访问Instance属性
        
         使用方式:
         管理类:  继承MonoSingleton<管理类>
                     重写 Init 方法,进行初始化.
         客户端:管理类.Instance.成员; 

         需求:
         如果管理类自行附加到场景中(即使没有代码访问Instance属性),
         //也可以在Awake中进行初始化.
         */
    }
}