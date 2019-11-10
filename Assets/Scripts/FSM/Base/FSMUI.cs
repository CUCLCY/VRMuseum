using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voice
{
    public class FSMUI: FSMBase<FSMUI>
    {
        private CanvasGroup group;
        protected override void Awake()
        {
            base.Awake();
            group = GetComponent<CanvasGroup>();
        }
        public void Show()
        {
            if (group == null)
            {
                group = GetComponent<CanvasGroup>();
            }
            group.alpha = 1;
        }
        public void Hide()
        {
            if (group==null)
            {
                group = GetComponent<CanvasGroup>();
            }
            group.alpha = 0;
        }
    }
}
