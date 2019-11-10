using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class IdleState: FSMState<FSMCharacotr>
    {
        
        public override void OnStateEnter(Vector3 value)
        {
            base.OnStateEnter();
            fsm.Idle();
        }
        public override void OnStateStay()
        {
            base.OnStateStay();
        }
        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}
