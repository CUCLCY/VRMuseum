using Museun_Gaze;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace ns 
{
    public class ShowTags : MonoBehaviour, IVRTKGaze
    {
        public Transform headTF;
        private GameObject go;
        public float distance = 0.5f;

        private void Awake()
        {
           
            //headTF = FindObjectOfType<ReticleController>().transform;
         
        }
        public void CompleteGaze()
        {
            distance = Vector3.Distance(transform.position, headTF.position) -0.38f;
            Vector3 dir = (headTF.position - transform.position).normalized;
            go = Instantiate(gameObject);
            go.layer = 0;
            go.transform.forward = dir;
           go.transform.position=transform.position;
            go.transform.DOMove(transform.position + dir * distance, 0.5f);
            //go.transform.position = transform.position + dir * distance;

        }

        public void EnterGaze()
        {
            
        }

        public void ExitGaze()
        {
            if (go!=null)
            {
                go.transform.DOMove(transform.position, 0.5f);
                Destroy(go,0.5f);
            }
          
            print("Exit");
        }

        public void StayGaze()
        {
           
        }
    }

}
