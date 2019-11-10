using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
岗位分工:
策划:在模型上附加当前脚本.
       在动画片段中,添加动画事件(攻击/取消动画).
程序:
       播放动画片段
       编写攻击逻辑,注册到AttackHandler事件中.
*/
namespace Common
{
    /// <summary>
    /// 
    /// </summary>
	public class AnimationEventBehaviour : MonoBehaviour 
	{
        private Animator anim;

        public event Action AttackHandler;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        //由Unity动画事件调用
        //当动画播放完毕后执行
        private void OnCancelAnim(string animPara)
        {
            anim.SetBool(animPara, false);
        }

        //由Unity动画事件调用
        //当动画播到某一时刻执行
        private void OnAttack()
        {
            //引发事件
            if (AttackHandler != null)
                AttackHandler();
        }
    }
}