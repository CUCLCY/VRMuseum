using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 可以被重置
    /// </summary>
    public interface IResetable
    {
        void OnReset();
    }

    /// <summary>
    /// 游戏对象池
    /// </summary>
	public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;

        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 使用对象池"创建"游戏对象
        /// </summary>
        /// <param name="type">类别</param>
        /// <param name="prefab">预制件</param>
        /// <param name="pos">位置</param>
        /// <param name="dir">旋转</param>
        /// <returns></returns>
        public GameObject CreateObject(string type, GameObject prefab, Vector3 pos, Quaternion dir)
        {
            GameObject go = FindUsableObject(type);

            if (go == null) go = AddObject(type, prefab);

            UseObject(pos, dir, go);

            return go;
        }

        private void UseObject(Vector3 pos, Quaternion dir, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = dir;
            go.SetActive(true);
            //...计算目标
            //go.GetComponent<IResetable>().OnReset();
            //遍历当前物体中所有需要被重置的脚本.
            foreach (var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        private GameObject AddObject(string type, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            cache[type].Add(go);
            return go;
        }

        private GameObject FindUsableObject(string type)
        {
            //如果键存在,则查找禁用的物体.
            if (cache.ContainsKey(type)) return cache[type].Find(e => !e.activeInHierarchy);
            //否则添加字典记录
            cache.Add(type, new List<GameObject>());
            //返回空
            return null;
        }

        //立即回收
        private void CollectObject(GameObject go)
        {
            go.SetActive(false);
        }

        //延迟回收
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            CollectObject(go);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="delay"></param>
        public void CollectObject(GameObject go, float delay = 0)
        {
            if (delay == 0)
                CollectObject(go);
            else
                StartCoroutine(CollectObjectDelay(go, delay));
        }
    }
}