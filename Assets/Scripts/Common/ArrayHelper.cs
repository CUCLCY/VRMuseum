
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
扩展方法:允许在不修改原有类代码情况下,在外部为其添加新方法.
作用:
调用者更方法
1. 通过引用调用(不使用类名)
2. 不用传递被扩展类型引用.

语法:
定义者:
1. 使用this关键字,修饰第一个参数.
2. 必须在非泛型的静态类中定义.
调用者:
1. 使用调用实例方法的方式调用扩展方法.
    静态方式的方式 --- 类名.方法名(数据);
    实例方式的方式 --- 数据.方法名();

适用性:
1. 希望调用方式可以像实例方法一样方便.

是什么?
干嘛的?
怎么用?
什么时候用? 
*/
namespace Common
{ 
    /// <summary>
    /// 数组助手类
    /// </summary>
	public static class ArrayHelper
    {
        //public static T GetMax<T>(T[] array,Func<T, float> handler)
        //{
        //    T max = array[0];
        //    for (int i = 1; i < array.Length; i++)
        //    {
        //        //if(max.HP <  array[i].HP)
        //        //if(YYY(max) < YYY(array[i]))
        //        if(handler(max) < handler(array[i]))
        //            max = array[i];
        //    }
        //    return max;
        //} 
        //static float XXX(EnemyStatus e)
        //{
        //    return e.HP;
        //}
        //static float YYY(EnemyStatus e)
        //{
        //    return e.baseATK;
        //}

        /// <summary>
        /// 获取对象数组的最大值,Enemy[]
        /// </summary>
        /// <typeparam name="T">数组中的元素类型</typeparam>
        /// <typeparam name="R">委托的返回值</typeparam>
        /// <param name="array">对象数组</param>
        /// <param name="handler">对象数组中元素的属性,比如:HP</param>
        /// <returns></returns>
        public static T GetMax<T, R>(this T[] array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Length == 0) return default(T);

            T max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                //if(max.HP <  array[i].HP)
                //if(YYY(max) < YYY(array[i]))
                //if (handler(max) < handler(array[i]))
                //CompareTo 返回负数表示 小于
                if (handler(max).CompareTo(handler(array[i])) < 0)
                    max = array[i];
            }
            return max;
        }

        //作业:
        //1. 获取对象数组的最小值 
        public static T GetMin<T, R>(this T[] array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Length == 0) return default(T);

            T min = array[0];
            for (int i = 1; i < array.Length; i++)
            { 
                if (handler(min).CompareTo(handler(array[i])) > 0)
                    min = array[i];
            }
            return min;
        }

        //2. 筛选对象数组
        public static R[] Select<T, R>(this T[] array, Func<T, R> handler)
        {
            R[] result = new R[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                //result[i] = array[i].GetComponent<EnemyStatus>();
                //result[i] = array[i].transform;
                result[i] = handler(array[i]);
            }
            return result;
        }
        //static   Animator AAA(GameObject o)
        //{ 
        //    return o.GetComponentInChildren<Animator>();
        //}
        //static Transform BBB(GameObject o)
        //{
        //    return o.transform;
        //}

        //   参数                              返回值
        // GameObject[]   --->  Transform[]
        // GameObject[]   --->  Animator[]
        // GameObject[]   --->  脚本[]

        //1. 对象数组升序排列.
        //Enemy[]              HP
        //Transform[]        距离

        //2.根据条件获取所有对象
        //查找死的敌人Enemy[]       HP == 0 
        //查找攻击力强的敌人Enemy[]      ATK > 80

        //解决步骤:
        //1. 实现具体的需求.
        //2. 识别变化点.
        //3. 抽象变化(找代表.接口/委托) 

        public static void OrderBy<T,R>(this T[] array, Func<T, R> handler) where R : IComparable
        {
            for (int r = 0; r < array.Length-1; r++)
            {
                for (int c = r+1; c < array.Length; c++)
                { 
                    if (handler(array[r]).CompareTo(handler(array[c]))>0  ) 
                    {
                        var temp = array[r];
                        array[r] = array[c];
                        array[c] = temp;
                    }
                }
            }
        }

        public static T[] FindAll<T>(this T[] array,Func< T  ,bool> handler)
        {
            List<T> result = new List<T>(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                //if (array[i].HP == 0)
                if(handler(array[i]))
                    result.Add(array[i]);
            }
            return result.ToArray();
        }

        //作业
        //2.根据条件获取单个对象
        //查找死的敌人Enemy[]       HP == 0 
        //查找攻击力强的敌人Enemy[]      ATK > 80

        //3.降序排列
        //查找死的敌人Enemy[]       HP == 0 
        //查找攻击力强的敌人Enemy[]      ATK > 80
    }
}