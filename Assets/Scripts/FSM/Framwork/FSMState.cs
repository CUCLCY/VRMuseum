using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    /// <summary>
    /// 状态类
    /// </summary>
    public class FSMState<T> where T:FSMBase<T>
    {
        private List<FSMTrigger<T>> triggers;

        private Dictionary<string, string> map;
        protected T fsm;

        public void Init(T fsm)
        {
            triggers = new List<FSMTrigger<T>>();
            map = new Dictionary<string, string>();
            this.fsm = fsm;
        }

        public void AddMap(string triggerName, string stateName)
        {
            map.Add(triggerName, stateName);
            CreateTriggerObject(triggerName);
        }

        private void CreateTriggerObject(string triggerName)
        {
            //例如:没有血量条件
            //策划配置:NoHealth
            //程序类名:NoHealthTrigger

            //命名空间.类名
            string className = "Voice." + triggerName + "Trigger";
            Type type = Type.GetType(className);
            FSMTrigger<T> obj = Activator.CreateInstance(type) as FSMTrigger<T>;
            //为条件对象提供状态机引用
            obj.Init(fsm);
            triggers.Add(obj);
        }

        /// <summary>
        /// 条件检测
        /// </summary>
        public void CheckTrigger()
        {
            //(四)遍历条件列表
            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i].OnTriggerHandle())
                {
                    //NoHealthTrigger --> NoHealth
                    string triggerName = triggers[i].GetType().Name.Replace("Trigger", "");
                    string stateName = map[triggerName];
                    //(六)调用状态机切换状态方法...
                    fsm.ChangeState(stateName,triggers[i].value);
                    break;
                }
            }
        }

        public virtual void OnStateEnter(Vector3 value) {
            VoiceManager.Instance.nlpData.Clear();
        }
        public virtual void OnStateEnter( ) {
            //每进入一个状态之前清除nlpMap缓存
            VoiceManager.Instance.nlpData.Clear();
        }
        public virtual void OnStateStay() { }
        public virtual void OnStateExit() { }
    }
}