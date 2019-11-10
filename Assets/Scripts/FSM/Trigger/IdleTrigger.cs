using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class IdleTrigger : FSMTrigger<FSMCharacotr>
    {
        public override bool OnTriggerHandle()
        {
            //return VoiceManager.Instance.nlpData.Count == 0;
            return false;
        }
    }
}
