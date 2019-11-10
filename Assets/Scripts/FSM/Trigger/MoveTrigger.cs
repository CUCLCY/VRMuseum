using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class MoveTrigger : FSMTrigger<FSMCharacotr>
    {
        public override bool OnTriggerHandle()
        {

            if (VoiceManager.Instance.nlpData.ContainsKey(WordType.v))
            {
                string rotation = VoiceManager.Instance.nlpData[WordType.v];
                if (rotation.Contains("前"))
                {
                    value = new Vector3(0, 0, 1);
                    return true;
                }
                else if (rotation.Contains("后"))
                {
                    value = new Vector3(0, 0, -1);
                    return true;

                }
                else if (rotation.Contains("左"))
                {
                    value = new Vector3(-1, 0, 0);
                    return true;

                }
                else if (rotation.Contains("右"))
                {
                    value = new Vector3(1, 0, 0);
                    return true;

                }
                //如果包含角度
                if (VoiceManager.Instance.nlpData.ContainsKey(WordType.m))
                {
                    string str = VoiceManager.Instance.nlpData[WordType.m];
                    //正则表达式提取数字
                    value *= int.Parse(System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", ""));
                    Debug.Log(value);
                    return true;
                }

                
            }

            return false;

        }
    }
}
