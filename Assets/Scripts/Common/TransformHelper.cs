using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 变换组件助手类
    /// </summary>
	public static class TransformHelper
    {
        /// <summary>
        /// 注视目标方向缓动旋转
        /// </summary>
        /// <param name="currentTF">当前变换组件</param>
        /// <param name="direction">目标方向</param>
        /// <param name="rotateSpeed">旋转速度</param>
        public static void LookAtDirection( Transform currentTF, Vector3 direction, float rotateSpeed )
        {
            //如果注视旋转的方向是零向量,则退出.
            if (direction == Vector3.zero) return;
            //当前物体 Z轴 朝向 direction 旋转
            Quaternion targetDir = Quaternion.LookRotation (direction);
            currentTF.rotation = Quaternion.Lerp (currentTF.rotation, targetDir, rotateSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 注视目标位置缓动旋转
        /// </summary>
        /// <param name="currentTF">当前变换组件</param>
        /// <param name="position">目标位置</param>
        /// <param name="rotateSpeed">旋转速度</param>
        public static void LookAtPosition( Transform currentTF, Vector3 position, float rotateSpeed )
        {
            //位置转换为方向
            Vector3 dir = position - currentTF.position;
            LookAtDirection (currentTF, dir, rotateSpeed);
        }

        /// <summary>
        /// 未知层级根据名称查找后代物体
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindChildByName( this Transform currentTF, string childName )
        {
            Transform childTF = currentTF.Find (childName);
            if (childTF != null) return childTF;
            for (int i = 0; i < currentTF.childCount; i++)
            {
                //FindChildByName(currentTF.GetChild(i), childName)
                childTF = currentTF.GetChild (i).FindChildByName (childName);
                if (childTF != null) return childTF;
            }
            return null;

        }
        public static T FindComponent<T>(this Transform currentTF, string childName)
        {
            Transform childTF = currentTF.Find(childName);
            if (childTF != null)
            {
                if (childTF.GetComponent<T>() != null)
                    return childTF.GetComponent<T>();
            }
            for (int i = 0; i < currentTF.childCount; i++)
            {
                //FindChildByName(currentTF.GetChild(i), childName)
                childTF = currentTF.GetChild(i).FindChildByName(childName);
                if (childTF != null)
                {
                    if (childTF.GetComponent<T>() != null)
                        return childTF.GetComponent<T>();
                }
            }
            return default(T);

        }


    }
}