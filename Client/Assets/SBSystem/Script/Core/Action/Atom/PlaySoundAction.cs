using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class PlaySoundAction : ActionAtom
    {
        public override void Excuse()
        {
            PlaySoundAtom data = AtomData as PlaySoundAtom;
            //Actor attacker = ActorMgr.Instance.GetActor(OwnerStageEntity.Attacker);
            //if (data == null || OwnerStageEntity == null || attacker == null || OwnerEntity == null)
            //{
            //    return;
            //}
            //if (data.TargetType == eTargetType.TargetType_Self)
            //{
            //    SoundEntity sndEntity = SoundEngine.Instance.PlaySound(data.SoundPrefab + ".ogg", eSoundType.eSoundType_Fight, data.FadeInTime, data.FadeOutTime, data.AutoDestroy ? false : true);//sndObj.AddComponent<SoundEntity> ();
            //    sndEntity.FollowTarget = true;
            //    sndEntity.Targeter = attacker.gameObject;
            //    sndEntity.Postion = attacker.Position;
            //    if (!data.AutoDestroy)
            //    {
            //        List<ulong> list;
            //        if (!OwnerEntity.SoundMap.TryGetValue(data.FlagIndex, out list))
            //        {
            //            list = new List<ulong>();
            //            OwnerEntity.SoundMap[data.FlagIndex] = list;
            //        }
            //        list.Add(sndEntity.ID);
            //    }
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
            //        SoundEntity sndEntity = SoundEngine.Instance.PlaySound(data.SoundPrefab, eSoundType.eSoundType_Fight, data.FadeInTime, data.FadeOutTime, data.AutoDestroy ? false : true);//sndObj.AddComponent<SoundEntity> ();
            //        sndEntity.FollowTarget = true;
            //        sndEntity.Targeter = act.gameObject;
            //        sndEntity.Postion = act.Position;
            //        if (!data.AutoDestroy)
            //        {
            //            List<ulong> list;
            //            if (!OwnerEntity.SoundMap.TryGetValue(data.FlagIndex, out list))
            //            {
            //                list = new List<ulong>();
            //                OwnerEntity.SoundMap[data.FlagIndex] = list;
            //            }
            //            list.Add(sndEntity.ID);
            //        }
            //    }
            //}

        }
    }
}
