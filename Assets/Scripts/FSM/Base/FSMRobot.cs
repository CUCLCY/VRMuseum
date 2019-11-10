using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class FSMRobot: FSMCharacotr
    {
        public override void Move(Vector3 value)
        {
            base.Move(value);
            
            iTween.MoveTo(gameObject, transform.position + value, 1f);
            anim.SetBool("Walk_Anim", true);

        }
        protected override void Awake()
        {
            base.Awake();
        }
        public override void Idle()
        {
            base.Idle();
            anim.SetBool("Open_Anim", true);
        }

    }
}
