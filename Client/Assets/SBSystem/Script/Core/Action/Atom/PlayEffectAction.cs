using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class PlayEffectAction : ActionAtom
    {
        public override void Excuse()
        {
            PlayEffctAtom data = AtomData as PlayEffctAtom;
            //Actor attacker = ActorMgr.Instance.GetActor(OwnerStageEntity.Attacker);
            //if (data == null || OwnerStageEntity == null || attacker == null || OwnerEntity == null)
            //{
            //    return;
            //}

            //if (data.TargetType == eTargetType.TargetType_Self)
            //{
            //    OnPlayEffect(attacker, attacker);
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
            //        OnPlayEffect(attacker, act);
            //    }
            //}

        }

        //void OnPlayEffect(Actor attacker, Actor targeter)
        //{
        //    PlayEffctAtom data = AtomData as PlayEffctAtom;

        //    AssetCacheMgr.Instance.LoadInstance(data.EffectPrefab + ".prefab", (obj) =>
        //    {
        //        if (obj == null)
        //        {
        //            return;
        //        }
        //        GameObject go = obj as GameObject;
        //        if (go == null || attacker == null || attacker.gameObject == null || targeter == null || targeter.gameObject == null)
        //        {
        //            AssetCacheMgr.Instance.ReleaseInstance(obj);
        //            return;
        //        }
        //        if (OwnerEntity.Finished && !data.AutoDestroy)
        //        {//有可能特效加载完之前这个技能或者buff已经执行完成了，那么久不需要吧特效实例化出来了
        //            AssetCacheMgr.Instance.ReleaseInstance(obj);
        //            return;
        //        }
        //        go.transform.position = attacker.Position;
        //        go.transform.rotation = attacker.Rotate;


        //        EffectArgument ea = Utility.AddIfMissing<EffectArgument>(go);
        //        EffectBase effect = null;
        //        if (data.EffectType == eEffectType.Attach)
        //        {
        //            NormalEffect norEft = Utility.AddIfMissing<NormalEffect>(go);

        //            effect = norEft;
        //        }
        //        else if (data.EffectType == eEffectType.Trace)
        //        {
        //            TraceEffect traEft = Utility.AddIfMissing<TraceEffect>(go);

        //            traEft.Speed = data.Speed;
        //            traEft.TraceType = data.TraceType;
        //            traEft.TraceDummyPoint = data.TraceDummyPoint;
        //            traEft.TraceOffsetLen = data.TraceOffsetLen;
        //            traEft.TraceOffsetAngle = data.TraceOffsetAngle;
        //            traEft.TraceStandDis = data.TraceOffsetStandDis;
        //            traEft.FactorVal = data.FactorVal;
        //            effect = traEft;
        //        }
        //        else if (data.EffectType == eEffectType.Position)
        //        {
        //            PositionEffect posEft = Utility.AddIfMissing<PositionEffect>(go);

        //            effect = posEft;
        //        }
        //        else if (data.EffectType == eEffectType.Bullet)
        //        {
        //            BulletEffect bulEft = Utility.AddIfMissing<BulletEffect>(go);

        //            bulEft.Speed = data.Speed;
        //            bulEft.LifeTime = data.LifeTime;
        //            bulEft.BulletACross = data.BulletACross;
        //            bulEft.BulletDamageWidth = data.BulletDamageWidth;
        //            effect = bulEft;
        //        }
        //        else if (data.EffectType == eEffectType.Boomerang)
        //        {
        //            BoomerangEffect boomEft = Utility.AddIfMissing<BoomerangEffect>(go);

        //            boomEft.Speed = data.Speed;
        //            boomEft.OnceTime = data.OnceTime;
        //            boomEft.BackToAttaker = data.BackToAttaker;
        //            boomEft.BooDamageWidth = data.BooDamageWidth;

        //            effect = boomEft;
        //        }
        //        else if (data.EffectType == eEffectType.Bounce)
        //        {
        //            BounceEffect bounceEft = Utility.AddIfMissing<BounceEffect>(go);

        //            bounceEft.Speed = data.Speed;
        //            bounceEft.BounceTimes = data.BounceTimes;
        //            bounceEft.BounceDistance = data.BounceDistance;
        //            bounceEft.TraceType = eTraceType.eTraceType_Targeter;

        //            effect = bounceEft;
        //        }
        //        else if (data.EffectType == eEffectType.Thunder)
        //        {
        //            ThunderEffect linkedEft = Utility.AddIfMissing<ThunderEffect>(go);

        //            linkedEft.Speed = data.Speed;
        //            linkedEft.TraceDummyPoint = data.TraceDummyPoint;
        //            linkedEft.TraceType = eTraceType.eTraceType_Targeter;
        //            linkedEft.DelayDestroyTime = data.DelayDestroyTime;
        //            linkedEft.ThunderLight = GameObject.Instantiate(Resources.Load(data.ThunderPrefab) as GameObject) as GameObject;

        //            effect = linkedEft;
        //        }
        //        else if (data.EffectType == eEffectType.Laser)
        //        {
        //            LaserEffect laserEft = Utility.AddIfMissing<LaserEffect>(go);
        //            laserEft.MaxLaserLength = data.LaserMaxLength;
        //            laserEft.LaserStartWidth = data.LaserStartWidth;
        //            laserEft.LaserEndWidth = data.LaserEndWidth;
        //            laserEft.CalcDamageTime = data.LaserCalcDamageTime;

        //            laserEft.LaserHeadEffect = data.LaserHeadEffect;
        //            laserEft.LaserTailEffect = data.LaserTailEffect;
        //            effect = laserEft;
        //        }
        //        effect.ID = ++EffectBase.MaxID;
        //        effect.OwnerEntity = OwnerEntity;
        //        // EffectEntity effect = Utility.AddIfMissing<EffectEntity>(_effect);//(EffectEntity)go.AddComponent<EffectEntity> ();
        //        effect.Attacker = attacker.gameObject;
        //        effect.Target = targeter.gameObject;
        //        effect.DummyPoint = data.DummyPoint;
        //        effect.OffsetPos = data.OffsetPos;
        //        effect.OffsetRotate = data.OffsetRotate;
        //        effect.Scale = data.ScaleApplyModel ? data.Scale * Mathf.Max(attacker.Scale.x, attacker.Scale.z) : data.Scale; ;
        //        effect.RotateToAttacker = data.RotateToAttacker;
        //        effect.AutoDestroy = data.AutoDestroy;
        //        effect.DelayDestroyTime = data.DelayDestroyTime;
        //        effect.OnlyTranlate = data.OnlyTranslate;
        //        effect.SpecialPos = OwnerStageEntity.SpecailPos;
        //        effect.InstancePraferb = data.ArrivedInstance;

        //        EffectController[] ecs = go.GetComponents<EffectController>();
        //        foreach (EffectController ec in ecs)
        //        {
        //            ea.LiftTime = ec.PlayTime;
        //            GameObject.Destroy(ec);
        //        }

        //        if (!data.AutoDestroy)
        //        {
        //            List<ulong> list;
        //            if (!OwnerEntity.EffectMap.TryGetValue(data.FlagIndex, out list))
        //            {
        //                list = new List<ulong>();
        //                OwnerEntity.EffectMap[data.FlagIndex] = list;
        //            }
        //            list.Add(effect.ID);
        //        }

        //        effect.Init();
        //    });
        //}
    }
}
