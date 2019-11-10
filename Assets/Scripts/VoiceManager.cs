using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Baidu.Aip.Nlp;
using UnityEngine.Windows.Speech;
using System;
using System.Threading;
using Common;
using LitJson;

namespace Voice
{
    public enum WordType
    {
        /// <summary>
        /// 动词
        /// </summary>
        v,
        /// <summary>
        /// 名动词
        /// </summary>
        vn,
        /// <summary>
        /// 名词
        /// </summary>
        n,
        /// <summary>
        /// 方位词
        /// </summary>
        f,
        /// <summary>
        /// 数量词
        /// </summary>
        m,
        p,
    }

    public class VoiceManager : MonoSingleton<VoiceManager>
    {
        public string APP_ID;
        public string API_KEY;
        public string SECRET_KEY;
        Nlp client;
        DictationRecognizer recognizer;
        /// <summary>
        /// Recognized Result
        /// </summary>
        /// 
        [HideInInspector]
        public string result;
        /// <summary>
        /// Results Processed By Nlp
        /// </summary>
        public Dictionary<WordType, string> nlpData;
        protected override void Init()
        {
            base.Init();
            nlpData = new Dictionary<WordType, string>();
            client = new Nlp(API_KEY, SECRET_KEY)
            {
                Timeout = 60000
            };
            recognizer = new DictationRecognizer
            {
                AutoSilenceTimeoutSeconds = 1
            };

            recognizer.DictationComplete += DictationComplete;
            recognizer.DictationResult += DictationResult;
            recognizer.DictationError += DictationError;
            recognizer.DictationHypothesis += DictationHypothesis;
            recognizer.Start();
        }
   
        private void StartRecognize( )
        {
            recognizer.Start();
        }

        private void DictationHypothesis(string text)
        {
            result = text;
            Debug.Log(text);
        }

        private void DictationError(string error, int hresult)
        {
            
        }

        private void DictationResult(string text, ConfidenceLevel confidence)
        {
            result = text;
            
        }

        private void DictationComplete(DictationCompletionCause cause)
        {

            Debug.Log(cause);
            Debug.Log(NlpProcess(result));
            if (cause==DictationCompletionCause.TimeoutExceeded)
            {
                recognizer.Start();
            }
            else if (cause==DictationCompletionCause.UnknownError)
            {
                recognizer = new DictationRecognizer
                {
                    AutoSilenceTimeoutSeconds = (float)1.0
                };
                //订阅事件  
                recognizer.DictationHypothesis += DictationHypothesis;
                recognizer.DictationResult += DictationResult;
                recognizer.DictationComplete += DictationComplete;
                recognizer.DictationError += DictationError;

                recognizer.Start();
             
            }
        }

        /// <summary>
        /// Return the result processed by Nlp
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string NlpProcess(string text)
        {
            //清除缓存
            nlpData.Clear();
            if (text=="")
            {
                return "";
            }
            string s = "";
            try
            {
                s= client.Lexer(text).ToString();
                Debug.Log(s);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

            JsonData data = JsonMapper.ToObject(s);
            foreach (JsonData item in data["items"])
            {
                string pos = item["pos"].ToString();
                if (pos=="vn")
                {
                    pos = "v";
                }
                try
                {
                    WordType type = (WordType)Enum.Parse(typeof(WordType), pos);
                    nlpData[type] = item["item"].ToString();
                }
                catch (Exception)
                {

                }

                
            }
            return s;
        }

        public void Test(string s)
        {
            Debug.Log(NlpProcess(s));
            foreach (WordType item in nlpData.Keys)
            {
                Debug.Log(item + "   " + nlpData[item]);
            }
        }

        public void GetKeywords()
        {

        }
        private void OnDestroy()
        {
            if (recognizer != null)
            {
                recognizer.Dispose();
            }
        }
    }
}