using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class ShowTrigger : FSMTrigger<FSMUI>
    {
        public override bool OnTriggerHandle()
        {
            if (VoiceManager.Instance.nlpData.ContainsKey(WordType.v))
            {
                string str = VoiceManager.Instance.nlpData[WordType.v];
                //如果包含角度
                if (str.Contains("开"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
