using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class ShowState: FSMState<FSMUI>
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            fsm.Show();
        }
        public override void OnStateEnter(Vector3 value)
        {
            base.OnStateEnter(value);
            fsm.Show();
        }
    }
}
