using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class HideState: FSMState<FSMUI>
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            fsm.Hide();
        }
        public override void OnStateEnter(Vector3 value)
        {
            base.OnStateEnter(value);
            fsm.Hide();
        }
    }
}
