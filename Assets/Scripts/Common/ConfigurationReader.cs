using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 配置文件读取器
    /// </summary>
	public class ConfigurationReader 
	{ 
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="fileName">文件名称,例如:Config/AI_01.txt</param>
        /// <returns></returns>
        public static string GetConfigFile(string fileName)
        {
            //个别手机不识别
            //string path = "file://" + Application.streamingAssetsPath + "/" + fileName;
            //因为在移动端,无法获取项目完整路径,所以不能使用System.IO (File)
            string path;
            //if( Application.platform   == RuntimePlatform.Android )
            //Unity 宏标签:编译时执行.
#if UNITY_EDITOR || UNITY_STANDALONE
            path = "file://" + Application.dataPath + "/StreamingAssets/"+ fileName;
#elif UNITY_IPHONE
             path = "file://" + Application.dataPath + "/Raw/"+ fileName;
#elif UNITY_ANDROID
             path = "jar:file://" + Application.dataPath + "!/assets/"+ fileName;          
#endif 
            WWW www = new WWW(path);
            //yield return www;
            while (true)
            {
                if (www.isDone)
                    return www.text;
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="file">配置文件</param>
        /// <param name="lineHandle">行的处理逻辑</param>
        public static void ReadConfigFile(string file, Action<string> lineHandle)
        {  
            using (StringReader reader = new StringReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //解析...  
                    lineHandle(line);
                }
            } 
        }
    }
}