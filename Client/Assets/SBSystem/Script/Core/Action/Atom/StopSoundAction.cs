using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class StopSoundAction : ActionAtom
    {
        public override void Excuse()
        {
            StopSoundAtom data = AtomData as StopSoundAtom;
            if (data == null || OwnerEntity == null || OwnerStageEntity == null)
            {
                return;
            }

            List<ulong> list;
            if (!OwnerEntity.SoundMap.TryGetValue(data.FlagIndex, out list))
            {
                return;
            }
            foreach (ulong cp in list)
            {
                //if (cp != null)
                   // SoundEngine.Instance.StopSound(cp);
            }
            OwnerEntity.EffectMap.Remove(data.FlagIndex);
        }
    }
}
