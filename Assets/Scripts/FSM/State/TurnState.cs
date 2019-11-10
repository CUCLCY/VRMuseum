using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class TurnState : FSMState<FSMCharacotr>
    {

        public override void OnStateEnter(Vector3 value)
        {
            base.OnStateEnter();
            fsm.transform.localEulerAngles += value;

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
