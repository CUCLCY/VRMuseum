using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class CloseTrigger : FSMTrigger<FSMCharacotr>
    {
        public override bool OnTriggerHandle()
        {
            if (VoiceManager.Instance.nlpData.ContainsKey(WordType.v))
            {
                string str = VoiceManager.Instance.nlpData[WordType.v];
                //如果包含角度
                if (str.Contains("再见"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
