using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    /// <summary>
    /// 条件类
    /// </summary>
    public abstract class FSMTrigger<T> where T:FSMBase<T>
    {
        protected T fsm;
        /// <summary>
        /// 向state传递的参数
        /// </summary>
        public Vector3 value;
        public void Init(T fsm)
        {
            
            this.fsm = fsm;
        }

        public abstract bool OnTriggerHandle();
      
    }
}

