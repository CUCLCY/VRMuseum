using System;
using System.Collections;
using System.Collections.Generic;
using UI.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRTK;

namespace UI 
{
    public class UIMainPanel: UIWindow
    {
        
        public Transform eyesTF;
        private VRTK_BasicTeleport basicTele;
        private DestinationMarkerEventArgs dmArgs;
        private GameObject[] points;
        private Transform FindTF(GameObject[] gos,string s)
        {
            Transform ts=null;
            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i].name==s)
                {
                    ts = gos[i].transform;
                }
              
               
            }
            return ts;

        }
        private new void Awake()
        {
            base.Awake();
            points = GameObject.FindGameObjectsWithTag("Point");
            basicTele = FindObjectOfType<VRTK_BasicTeleport>();
           
            
            GetUIEventListener("GreekButton").PointerClick += OnGreekClick;
            GetUIEventListener("AsianButton").PointerClick += OnAsianClick;
            GetUIEventListener("EgyptButton").PointerClick += OnEgyptClick;
        }
        private void Start()
        {
            //    if (FindObjectOfType<SteamVR_Camera>()!=null)
            //{
            //    eyesTF = FindObjectOfType<SteamVR_Camera>().transform;
            //}
            //else
            //{
            //    eyesTF = FindObjectOfType<SDK_InputSimulator>().transform;
            //}
        }
        private void OnEgyptClick(PointerEventData eventData)
        {
            
            basicTele.ForceTeleport(FindTF(points, "EgyptPoint").position,Quaternion.Euler(Vector3.zero));
        }

        private void OnAsianClick(PointerEventData eventData)
        {
            basicTele.ForceTeleport(FindTF(points, "AsianPoint").position, Quaternion.Euler(Vector3.zero));
        }

        private void OnGreekClick(PointerEventData eventData)
        {
            
        }
    }

}
