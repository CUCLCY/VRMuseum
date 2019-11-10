using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Framework
{
    /// <summary>
    /// UI管理器:管理(记录/禁用/查找)当前场景中所有UI窗口.
    /// </summary>
	public class UIManager : MonoSingleton<UIManager> 
	{
        private Dictionary<Type,UIWindow> cache;
        protected override void Init()
        {
            base.Init();

            cache = new Dictionary<Type, UIWindow>();
            UIWindow[] allWindow = FindObjectsOfType<UIWindow>();
            for (int i = 0; i < allWindow.Length; i++)
            {
                //禁用窗口
                //allWindow[i].SetVisible(false);
                Type key = allWindow[i].GetType();
                cache.Add(key, allWindow[i]);
            }
        }

        public T GetWindow<T>() where T: UIWindow
        {
            Type key = typeof(T);
            //如果字典中不存在key,则返回空.
            if (!cache.ContainsKey(key)) return null;
            return (T)cache[key];
        }
    }
}