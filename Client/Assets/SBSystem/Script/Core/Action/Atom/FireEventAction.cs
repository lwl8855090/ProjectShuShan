using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SB
{
    class FireEventAction : ActionAtom
    {
        public override void Excuse()
        {
            FireEventAtom data = AtomData as FireEventAtom;
            //Actor attacker = ActorMgr.Instance.GetActor(OwnerStageEntity.Attacker);
            //if (data == null || OwnerEntity == null || attacker == null || OwnerStageEntity == null)
            //{
            //    return;
            //}
            //if (data.EventType == FireEventAtom.eEventType.CameraShake)
            //{
            //    if (!data.ShakeOnlyLeader || (data.ShakeOnlyLeader && attacker == ActorMgr.Instance.GetTeamLeader()))
            //        CameraShake.Shake(data.NumberOfShakes, data.ShakeAmount, data.RotationAmount, data.ShakeDistance, data.ShakeSpeed, data.ShakeDecay, data.GuiShakeModifier, data.MultiplyByTimeScale);
            //}
            //else if (data.EventType == FireEventAtom.eEventType.Damage)
            //{
            //    List<Actor> targets = GetTargeters(attacker);
            //    foreach (Actor targeter in targets)
            //    {
            //        if (data.DamageForceLevel > targeter.Template.ForceLevel)
            //        {
            //            FlageCheckDamageArg arg = new FlageCheckDamageArg();
            //            arg.HitVelicity = targeter.Position - attacker.Position;
            //            arg.HitVelicity.Normalize();
            //            arg.SpeedUpTime = data.SpeedUpTime;
            //            arg.SpeedConstTime = data.SpeedConstTime;
            //            arg.SpeedDownTime = data.SpeedDownTime;
            //            arg.SpeedMax = data.SpeedMax;
            //            if (targeter.FCMgr != null)
            //            {
            //                targeter.FCMgr.DealFlagCheckArg(arg);
            //            }
            //        }
            //        if (targeter.HitEffectController != null)
            //            targeter.HitEffectController.PlayEffect();
            //        int HPvalue = OwnerEntity.OnCalcDamage(data.DamageIndex, attacker, targeter);
            //        //				targeter.Template.ModifyHP(HPvalue);
            //        GlobalFun.ModifyHP(targeter, HPvalue, true);
            //        PropertyModel propModle = attacker.GetActorModle(eActorLogicModle.eActorLogicModle_Property) as PropertyModel;
            //        if (propModle != null)
            //        {
            //            propModle.ModifyProperty(ePropType.Power, eCalcType.ByValue, OwnerEntity.GetProviderPower());
            //        }
            //        float fHPValue = 0;
            //        PropertyModel properties = targeter.GetActorModle(eActorLogicModle.eActorLogicModle_Property) as PropertyModel;
            //        if (properties != null)
            //        {
            //            fHPValue = properties.GetBaseProperty(ePropType.curHP);
            //        }

            //        if (fHPValue <= 0 && !targeter.CheckFlag(eFlagType.Death))
            //        {
            //            if (propModle != null && targeter.ResItem != null)
            //            {
            //                propModle.ModifyProperty(ePropType.Power, eCalcType.ByValue, targeter.ResItem.KillProvidePower);
            //            }
            //            FlageCheckDeathArg arg2 = new FlageCheckDeathArg();
            //            targeter.FCMgr.DealFlagCheckArg(arg2);
            //        }
            //    }
            //}
            //else if (data.EventType == FireEventAtom.eEventType.MoveForward)
            //{
            //    FlageCheckMoveForwardArg arg = new FlageCheckMoveForwardArg();
            //    arg.SpeedUpTime = data.SpeedUpTime;
            //    arg.SpeedConstTime = data.SpeedConstTime;
            //    arg.SpeedDownTime = data.SpeedDownTime;
            //    arg.SpeedMax = data.SpeedMax;
            //    arg.MoveNoPhysics = data.MoveNoPhysics;
            //    if (attacker.FCMgr != null)
            //    {
            //        attacker.FCMgr.DealFlagCheckArg(arg);
            //    }
            //}
            //else if (data.EventType == FireEventAtom.eEventType.GenerateForce)
            //{
            //    FlageForceGenerateArg arg = new FlageForceGenerateArg();
            //    arg.SpeedUpTime = data.SpeedUpTime;
            //    arg.SpeedConstTime = data.SpeedConstTime;
            //    arg.SpeedDownTime = data.SpeedDownTime;
            //    arg.SpeedMax = data.SpeedMax;
            //    arg.ForceLevel = data.GenerateForceLevel;
            //    if (attacker.FCMgr != null)
            //    {
            //        attacker.FCMgr.DealFlagCheckArg(arg);
            //    }
            //}
            //else if (data.EventType == FireEventAtom.eEventType.UnGenerateForce)
            //{
            //    attacker.ReSetFlag(eFlagType.GenerateForce);
            //}
            //else if (data.EventType == FireEventAtom.eEventType.Pause)
            //{
            //    SkillMgr.Instance.Pause();
            //    BuffMgr.Instance.Pause();
            //    SoundEngine.Instance.Pause();
            //    ActorMgr.Instance.Pause(attacker);
            //    OwnerEntity.Resume();
            //}
            //else if (data.EventType == FireEventAtom.eEventType.Resume)
            //{
            //    SkillMgr.Instance.Resume();
            //    BuffMgr.Instance.Resume();
            //    SoundEngine.Instance.Resume();
            //    ActorMgr.Instance.Resume();
            //}
            //else if (data.EventType == FireEventAtom.eEventType.ChangeScale)
            //{
            //    FlageChangeScaleArg arg = new FlageChangeScaleArg();
            //    arg.DestScale = data.DestScale;
            //    arg.ChangTime = data.ChangTime;
            //    if (attacker.FCMgr != null)
            //    {
            //        attacker.FCMgr.DealFlagCheckArg(arg);
            //    }
            //}
        }


        //List<Actor> GetTargeters(Actor own)
        //{
        //    FireEventAtom data = AtomData as FireEventAtom;
        //    ActorTemplate _actorTmp = own.Template;
        //    List<Actor> res = new List<Actor>();
        //    Actor firTar = own;
        //    if (OwnerStageEntity.Targeters.Count > 0 && !data.DamageCalcByAttacker)
        //        firTar = ActorMgr.Instance.GetActor(OwnerStageEntity.Targeters[0]);
        //    if (firTar == null)
        //        firTar = own;
        //    if (!data.DamageReCalcTars)
        //    {
        //        foreach (ulong id in OwnerStageEntity.Targeters)
        //        {
        //            Actor act = ActorMgr.Instance.GetActor(id);
        //            if (act == null)
        //            {
        //                continue;
        //            }
        //            res.Add(act);
        //        }
        //        return res;
        //    }
        //    if (data.DamageTargetType == eTargeterType.Own)
        //    {
        //        res.Add(own);
        //    }
        //    else if (data.DamageTargetType == eTargeterType.TeamLeader)
        //    {
        //        Actor ac = ActorMgr.Instance.GetTeamLeader();
        //        if (ac != null)
        //        {
        //            res.Add(ac);
        //        }
        //    }
        //    else
        //    {
        //        foreach (Actor ac in ActorMgr.Instance.RealActors)
        //        {
        //            ActorTemplate atmp = ac.Template;
        //            if (atmp == null)
        //            {
        //                continue;
        //            }
        //            if (data.DamageTargetType == eTargeterType.Friends)
        //            {
        //                if (own.CommonData.Camp != ac.CommonData.Camp)
        //                {
        //                    continue;
        //                }
        //            }
        //            else if (data.DamageTargetType == eTargeterType.Enemys)
        //            {
        //                if (own.CommonData.Camp == ac.CommonData.Camp)
        //                {
        //                    continue;
        //                }
        //            }
        //            if (!data.DamageRect)
        //            {
        //                if (!CheckActor(firTar, ac))
        //                    continue;
        //            }
        //            else
        //            {
        //                if (!CheckActorEx(firTar, ac))
        //                    continue;
        //            }
        //            res.Add(ac);
        //        }
        //    }
        //    return res;
        //}

        //bool CheckActor(Actor own, Actor ac)
        //{
        //    FireEventAtom data = AtomData as FireEventAtom;
        //    float dis = Vector3.Distance(own.Position, ac.Position);
        //    dis -= ac.ColliderRadus;
        //    if (dis > data.DamageDistance)
        //    {
        //        return false;
        //    }
        //    Vector3 targetDir = ac.Position - own.Position;
        //    Vector3 forward = own.Forward;
        //    float angle = Vector3.Angle(targetDir, forward);
        //    if (angle > data.DamageAngle)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //bool CheckActorEx(Actor own, Actor ac)
        //{
        //    FireEventAtom data = AtomData as FireEventAtom;
        //    Vector3 distance = ac.Position - own.Position;
        //    Vector3 dis = distance;
        //    dis.Normalize();
        //    Vector3 forwardDir = own.Forward;
        //    forwardDir.Normalize();
        //    float angle = (float)System.Math.Acos((double)Vector3.Dot(dis, forwardDir));
        //    if (angle > 1.57)
        //    {
        //        return false;
        //    }
        //    float angle2 = 3.14f - angle;
        //    float offY = (float)System.Math.Cos((double)angle2) * Vector3.Distance(ac.Position, own.Position);
        //    float offX = (float)System.Math.Sin((double)angle2) * Vector3.Distance(ac.Position, own.Position);

        //    if (System.Math.Abs(offX) > data.DamageWidth / 2 || System.Math.Abs(offY) > data.DamageHeight)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
