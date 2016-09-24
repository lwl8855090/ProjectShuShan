using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class StopEffectAction : ActionAtom
    {
        public override void Excuse()
        {
            StopEffctAtom data = AtomData as StopEffctAtom;
            if (data == null || OwnerStageEntity == null || OwnerEntity == null)
            {
                return;
            }

            List<ulong> list;
            if (!OwnerEntity.EffectMap.TryGetValue(data.FlagIndex, out list))
            {
                return;
            }
            foreach (ulong cp in list)
            {
                SkillMgr.Instance.RemoveEffectEntity(cp);
            }
            OwnerEntity.EffectMap.Remove(data.FlagIndex);
        }
    }
}
