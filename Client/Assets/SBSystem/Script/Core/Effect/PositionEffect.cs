using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    class PositionEffect : EffectBase
    {
        override protected void onReset()
        {

        }

        override protected void onInit()
        {

        }
        override protected void onPostInit()
        {

        }

        override protected void onUpdate()
        {
            if (AutoDestroy && _elapseTime >= PlayTime)
            {
                StartDestroy();
            }
            _elapseTime += Time.deltaTime;
        }
    }
}
