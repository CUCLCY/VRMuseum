using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Framework;
using UnityEngine.UI;

namespace UI 
{
    public class UITopWindow: UIWindow
    {
        private Text timeTxt;
        private new void Awake()
        {
            base.Awake();
            timeTxt = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        }
        private void Update()
        {
            timeTxt.text = string.Format("{0:D2}:{1:D2}", DateTime.Now.Hour, DateTime.Now.Minute);
        }

    }

}
