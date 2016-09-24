using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class PlayAnimAction : ActionAtom
    {
        public override void Excuse()
        {
            PlayAnimAtom data = AtomData as PlayAnimAtom;
            //Actor attacker = ActorMgr.Instance.GetActor(OwnerStageEntity.Attacker);
            //if (data == null || OwnerStageEntity == null || attacker == null)
            //{
            //    return;
            //}
            //if (data.TargetType == eTargetType.TargetType_Self)
            //{
            //    attacker.PlayAnim(ArtConfig.Instance.GetAnimName(data.AnimType), data.AnimMode);
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
            //        act.PlayAnim(ArtConfig.Instance.GetAnimName(data.AnimType), data.AnimMode);
            //    }
            //}
        }
    }
}
