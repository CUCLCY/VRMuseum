using System;
using System.Collections;
using System.Collections.Generic;
using UI.Framework;
using UnityEngine;
using VRTK;
namespace ns 
{
    public class VRInputController: MonoBehaviour
    {
        private VRTK_ControllerEvents events;
        private Canvas menuCanva;
        private UIWindow[] uiWindows;
        private void Awake()
        {
            uiWindows = FindObjectsOfType<UIWindow>();
            menuCanva = GameObject.FindGameObjectWithTag("Menu").GetComponent<Canvas>();
            events = GetComponent<VRTK_ControllerEvents>();
            events.StartMenuPressed += ShowMenu;
        }
        private bool isOpen =false;
        private void ShowMenu(object sender, ControllerInteractionEventArgs e)
        {
            isOpen = !isOpen;
            for (int i = 0; i < uiWindows.Length; i++)
            {
                uiWindows[i].SetVisible(isOpen);
            }
           
        }
    }

}
