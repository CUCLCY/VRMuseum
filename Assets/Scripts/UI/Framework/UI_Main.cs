using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Framework
{
    public class UI_Main: UIWindow
    {
        public void ChangeScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
