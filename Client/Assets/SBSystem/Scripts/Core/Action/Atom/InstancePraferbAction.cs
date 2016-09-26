using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class InstancePraferbAction : ActionAtom
    {
        public override void Excuse()
        {
            InstancePraferbAtom data = AtomData as InstancePraferbAtom;
        //    Actor attacker = ActorMgr.Instance.GetActor(OwnerStageEntity.Attacker);
        //    if (data == null || OwnerStageEntity == null || attacker == null)
        //    {
        //        return;
        //    }
        //    if (data.InstancePosType == eInstancePosType.eInstancePosType_Caster)
        //    {
        //        for (int i = 0; i < data.RandNum; ++i)
        //        {
        //            ActorTemplate ac = attacker.Template;
        //            if (ac == null)
        //                return;
        //            Actor act = ActorMgr.Instance.AddFsmObj();
        //            act.ResItem = ac.OwnerActor.ResItem;
        //            act.ModelFileName = data.PrefabName;

        //            float fX = UnityEngine.Random.Range(-data.RandRadius, data.RandRadius);
        //            float fZ = UnityEngine.Random.Range(-data.RandRadius, data.RandRadius);
        //            Vector3 pos = attacker.Position + new Vector3(fX, 0, fZ);

        //            act.Position = Utility.GetTerrianPos(pos);
        //            act.CommonData.AttackTargetID = ac.ID;
        //            act.CommonData.CasterID = ac.ID;
        //            act.CommonData.Camp = ac.OwnerActor.CommonData.Camp;

        //            act.InitData();
        //        }
        //    }
        //    else if (data.InstancePosType == eInstancePosType.eInstancePosType_Targeter)
        //    {
        //        foreach (ulong id in OwnerStageEntity.Targeters)
        //        {
        //            Actor act = ActorMgr.Instance.GetActor(id);
        //            if (act == null)
        //            {
        //                continue;
        //            }

        //            for (int i = 0; i < data.RandNum; ++i)
        //            {
        //                ActorTemplate ac = attacker.Template;
        //                if (ac == null)
        //                    return;
        //                Actor act2 = ActorMgr.Instance.AddFsmObj();
        //                act2.ResItem = attacker.ResItem;
        //                act2.ModelFileName = data.PrefabName;

        //                float fX = UnityEngine.Random.Range(-data.RandRadius, data.RandRadius);
        //                float fZ = UnityEngine.Random.Range(-data.RandRadius, data.RandRadius);
        //                Vector3 pos = act.Position + new Vector3(fX, 0, fZ);

        //                act2.Position = Utility.GetTerrianPos(pos);
        //                act2.CommonData.AttackTargetID = ac.ID;
        //                act2.CommonData.CasterID = ac.ID;
        //                act2.CommonData.Camp = attacker.CommonData.Camp;

        //                act2.InitData();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < data.RandNum; ++i)
        //        {
        //            ActorTemplate ac = attacker.Template;
        //            if (ac == null)
        //                return;
        //            Actor act2 = ActorMgr.Instance.AddFsmObj();
        //            act2.ResItem = attacker.ResItem;
        //            act2.ModelFileName = data.PrefabName;

        //            float fX = UnityEngine.Random.Range(-data.RandRadius, data.RandRadius);
        //            float fZ = UnityEngine.Random.Range(-data.RandRadius, data.RandRadius);
        //            Vector3 pos = OwnerStageEntity.SpecailPos + new Vector3(fX, 0, fZ);

        //            act2.Position = Utility.GetTerrianPos(pos);
        //            act2.CommonData.AttackTargetID = ac.ID;
        //            act2.CommonData.CasterID = ac.ID;
        //            act2.CommonData.Camp = attacker.CommonData.Camp;

        //            act2.InitData();
        //        }
        //    }
        }
    }
}
