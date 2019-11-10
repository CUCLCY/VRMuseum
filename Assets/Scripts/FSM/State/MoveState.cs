using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class MoveState: FSMState<FSMCharacotr>
    {
        
        public override void OnStateEnter(Vector3 value)
        {
            base.OnStateEnter();
            fsm.Move(value);
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
