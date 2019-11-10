using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Museun_Gaze 
{
    public interface IVRTKGaze
    {
        void EnterGaze();
        void StayGaze();
        void ExitGaze();
        void CompleteGaze();
    }

}
