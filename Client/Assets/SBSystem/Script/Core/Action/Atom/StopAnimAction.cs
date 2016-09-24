using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class StopAnimAction : ActionAtom
    {
        public override void Excuse()
        {
            StopAnimAtom data = AtomData as StopAnimAtom;
            //Actor attacker = ActorMgr.Instance.GetActor(OwnerStageEntity.Attacker);
            //if (data == null || OwnerStageEntity == null || attacker == null)
            //{
            //    return;
            //}
            //if (data.TargetType == eTargetType.TargetType_Self)
            //{
            //    attacker.StopAnim(ArtConfig.Instance.GetAnimName(data.AnimType));
            //}
            //else
            //{
            //    foreach (ulong id in OwnerStageEntity.Targeters)
            //    {
            //        Actor act = ActorMgr.Instance.GetActor(id);
            //        if (act == null)
            //        {
            //            continue;
            //        }
            //        act.StopAnim(ArtConfig.Instance.GetAnimName(data.AnimType));
            //    }
            //}
        }
    }
}
