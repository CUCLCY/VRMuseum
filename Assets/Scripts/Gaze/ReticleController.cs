using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
namespace Museun_Gaze 
{
    public class ReticleController: MonoBehaviour
    {
        public Canvas gazeCanva;
        
        private Image reticleImg;
        public LayerMask layer;
        private GameObject targetGo;
        public float countTime = 2;
        private float nowTime = 0;

        private bool isComplete;
        private RaycastHit hitInfo;
        private void Awake()
        {
            reticleImg = gazeCanva.transform.FindChildByName("Image").GetComponent<Image>();
            reticleImg.fillAmount = 0;
        }
        private void Update()
        {
            gazeCanva.transform.position = transform.position + transform.forward * 2;
            gazeCanva.transform.forward = transform.forward;
            Ray ray = new Ray(transform.position, transform.forward);
            
            if (Physics.Raycast(ray, out hitInfo, 5, layer))
            {
                
                if (hitInfo.transform.gameObject!=targetGo)
                {
                    nowTime = 0;
                    reticleImg.fillAmount = 0;
                    if (targetGo!=null)
                    {
                        if (hitInfo.transform.GetComponent<IVRTKGaze>()!=null)
                        {
                            hitInfo.transform.GetComponent<IVRTKGaze>().ExitGaze();
                        }
                      
                    }
                    else
                    {
                        if (hitInfo.transform.GetComponent<IVRTKGaze>()!=null)
                        {
                            hitInfo.transform.GetComponent<IVRTKGaze>().EnterGaze();
                        }
                      
                    }
                    targetGo = hitInfo.transform.gameObject;
                }
                else
                {
                   
                   
                    nowTime += Time.deltaTime;
                    
                    if (nowTime<countTime)
                    {
                        if (hitInfo.transform.GetComponent<IVRTKGaze>()!=null)
                        {
                            hitInfo.transform.GetComponent<IVRTKGaze>().StayGaze();
                        }
                        reticleImg.fillAmount = nowTime / countTime;
                        isComplete = true;
                    }
                    
                    else
                    {
                        if (isComplete)
                        {
                            OnComplete();
                        }
                       
                    }

                }
                
            }
            else
            {
                if (targetGo != null)
                {
                    targetGo.transform.GetComponent<IVRTKGaze>().ExitGaze();
                }
                nowTime = 0;
                targetGo = null;
                reticleImg.fillAmount = 0;
                
              
            }
        }

        private void OnComplete()
        {
            if (isComplete)
            {
                hitInfo.transform.GetComponent<IVRTKGaze>().CompleteGaze();
                reticleImg.fillAmount = 0;
            }
            isComplete = false;
        }

        
    }

}
