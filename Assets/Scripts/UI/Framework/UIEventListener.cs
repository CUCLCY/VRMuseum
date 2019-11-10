using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Framework
{
    public delegate void PointerEventHandler(PointerEventData eventData);
    public delegate void CancelEventHandler( BaseEventData baseEventData );

    /// <summary>
    /// UI事件监听器:附加需要检测的UI.
    /// </summary>
	public class UIEventListener : MonoBehaviour,ICancelHandler, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler//...
    { 
        /*
         定义事件:
         1. 定义事件参数类
         2. 定义委托
         3. 声明委托类型变量(事件)
         4. 在某个方法内引发事件 
         */
        
        public event PointerEventHandler PointerClick;
        public event PointerEventHandler PointerEnter;
        public event PointerEventHandler PointerExit;

        public event CancelEventHandler cancel;
        public void OnCancel( BaseEventData eventData )
        {
            if (cancel != null) cancel (eventData);
           
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PointerClick != null) PointerClick(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (PointerEnter != null) PointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (PointerExit != null) PointerExit(eventData);
        } 
    }
}