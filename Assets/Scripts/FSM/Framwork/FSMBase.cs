using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Voice
{
    public class FSMBase<T> : MonoBehaviour where T:FSMBase<T>
    {
        public string filePath;

        public string defaultStateName;


        private FSMState<T> currentState;
        private FSMState<T> defaultState;

        private string json;
        private List<FSMState<T>> states;
        private void ConfigFSM()
        {
            states = new List<FSMState<T>>();
            json = GetConfigFile(filePath);
            JsonData data = JsonMapper.ToObject(json);
            foreach (var stateName in data.Keys)
            {
                JsonData stateJson = data[stateName];
                //stateName 状态名称
                Type type = Type.GetType("Voice." + stateName+"State");
                FSMState<T> stateObj = Activator.CreateInstance(type) as FSMState<T>;
                stateObj.Init(this as T);
                foreach (var trigger in stateJson.Keys)
                {
                    stateObj.AddMap(trigger,stateJson[trigger].ToString());
                }
                states.Add(stateObj);
            }
         

        }
       
        private void InitDefaultState()
        {
            //在状态列表中,根据默认状态名称查找对象
            //public delegate bool Predicate(FSMState obj);
            defaultState =
                  states.Find(e => e.GetType().Name == defaultStateName + "State");
            //设置当前状态
            currentState = defaultState;
            //执行进入状态逻辑
            currentState.OnStateEnter();
        }


        protected virtual void Awake()
        {
            ConfigFSM();
            InitDefaultState();
        }

        protected void Update()
        {
            //(三)检测当前状态条件
            currentState.CheckTrigger();
            //(六)执行当前状态的停留逻辑
            currentState.OnStateStay();
        }

        public void ChangeState(string stateName,Vector3 value)
        {
            currentState.OnStateExit();

            //如果需要切换的是默认状态 则直接返回默认状态对象
            if (stateName == "Default")
                currentState = defaultState;
            else
                currentState = states.Find(e => e.GetType().Name == stateName + "State");

            currentState.OnStateEnter(value);
        }

        public string GetConfigFile(string fileName)
        {
            //个别手机不识别
            //string path = "file://" + Application.streamingAssetsPath + "/" + fileName;
            //因为在移动端,无法获取项目完整路径,所以不能使用System.IO (File)
            string path;
            //if( Application.platform   == RuntimePlatform.Android )
            //Unity 宏标签:编译时执行.
#if UNITY_EDITOR || UNITY_STANDALONE
            path = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
#elif UNITY_IPHONE
             path = "file://" + Application.dataPath + "/Raw/"+ fileName;
#elif UNITY_ANDROID
             path = "jar:file://" + Application.dataPath + "!/assets/"+ fileName;          
#endif
             
            //using (StreamReader sr=new StreamReader(path))
            //{
            //    return sr.ReadToEnd();
            //}

            WWW www = new WWW(path);
            //yield return www;
            while (true)
            {
                if (www.isDone)
                    return www.text;
            }
        }

    }
}