using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class OpenTrigger : FSMTrigger<FSMCharacotr>
    {
        public override bool OnTriggerHandle()
        {
            if (VoiceManager.Instance.nlpData.ContainsKey(WordType.n)|| VoiceManager.Instance.nlpData.ContainsKey(WordType.vn))
            {
                string str = VoiceManager.Instance.nlpData[WordType.n];
                //如果包含角度
                if (str.Contains("Hello")|| str.Contains("测试"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
