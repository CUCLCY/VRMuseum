using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class FSMCharacotr: FSMBase<FSMCharacotr>
    {
        protected Animator anim;
        public GameObject player;
        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
        }
        public virtual void Move(Vector3 value)
        { }
        public virtual void Idle()
        { }
        public void Open()
        {
            anim.SetBool("Open_Anim", true);
            iTween.LookTo(gameObject, player.transform.position, 1f);
        }
        public void Close()
        {
            anim.SetBool("Open_Anim", false);
        }

    }
}
