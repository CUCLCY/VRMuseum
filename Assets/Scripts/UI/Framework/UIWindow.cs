using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Framework
{
    /// <summary>
    /// UI窗口基类:定义所有窗口共性,以层次化方式管理窗口.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
	public abstract class UIWindow : MonoBehaviour
    {
        private CanvasGroup group;

        private Dictionary<string, UIEventListener> eventCache;

        protected void Awake()
        {
            group = GetComponent<CanvasGroup>();
            eventCache = new Dictionary<string, UIEventListener>();
        }

        //1.立即显示/隐藏 
        private void SetVisibleImmediate(bool state)
        {
            group.alpha = state ? 1 : 0;
            group.blocksRaycasts = state;
        }

        //2.延迟显隐
        private IEnumerator SetVisibleDelay(bool state,float delay)
        { 
            yield return new WaitForSeconds(delay);
            SetVisibleImmediate(state);
        }

        /// <summary>
        /// 设置窗口的可见性
        /// </summary>
        /// <param name="state">可见性</param>
        /// <param name="delay">延迟时间</param>
        public void SetVisible(bool state, float delay = 0)
        { 
            StartCoroutine(SetVisibleDelay(state, delay));
        }

        public UIEventListener GetUIEventListener(string name)
        {
            //如果缓存中没有指定名称的事件监听器
            if (!eventCache.ContainsKey(name))
            {//则查找,并加入到缓存中.
                Transform childTF = transform.FindChildByName(name); 
                eventCache.Add(name, childTF.GetComponent<UIEventListener>());
            }
            return eventCache[name];
        }
    }
}