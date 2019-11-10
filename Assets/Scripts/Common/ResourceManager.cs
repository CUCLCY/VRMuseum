using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 资源管理器
    /// </summary>
	public class ResourceManager 
	{
        private static Dictionary<string, string> map;

        //静态构造函数
        static ResourceManager()
        {
            map = new Dictionary<string, string>();
            string file = ConfigurationReader.GetConfigFile("Config/ResConfig.txt");
            ConfigurationReader.ReadConfigFile(file, BuildMap);
        }

        private static void BuildMap(string line)
        {
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0], keyValue[1]);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T">需要加载的资源类型</typeparam>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static T Load<T>(string fileName)where T:Object
        {
            if (!map.ContainsKey(fileName)) return null;
            //将文件名 转换为 路径
            string path = map[fileName];
            return Resources.Load<T>(path);
        }
	}
}