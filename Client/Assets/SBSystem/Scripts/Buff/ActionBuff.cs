using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace SB
{
    public class ActionBuff : ActionCommon
    {
        public MetaBuff BuffData;

        public float LifeTime = 0f;
        public float FireTime = 0f;

        //public LuaTable ProcessInterface;

        public bool WaitDestroy = false;


        public override bool Finished
        {
            get
            {
                return _end;
            }
        }

        private List<ActionStage> _stageList = new List<ActionStage>();

        private float _elapseTime = 0.0f;
        private float _curLifeTime = 0.0f;

        private bool _end = false;

        public void Start()
        {
            //if (ProcessInterface != null && ProcessInterface["OnBuffStart"] != null)
            //{
            //    ((LuaFunction)ProcessInterface["OnBuffStart"]).call(ActorMgr.Instance.GetActor(GetRealAttacker()), ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
            //}
            OnCast();
            Update();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override int OnCalcDamage(int index, ulong attacker, ulong targeter)
        {
            //if (ProcessInterface != null && ProcessInterface["OnCalcDamage"] != null)
            //{
            //    object obj = ((LuaFunction)ProcessInterface["OnCalcDamage"]).call(index, attacker, targeter, ProcessInterface);
            //    int val = 0;
            //    try
            //    {
            //        val = Convert.ToInt32(obj);
            //    }
            //    catch
            //    {
            //        val = 0;
            //    }
            //    return val;
            //}
            return 0;
        }

        // Update is called once per frame
        public void Update()
        {
            if (_paused)
            {
                return;
            }
            if (WaitDestroy)
            {
                return;
            }
            if (BuffData == null)
            {
                WaitDestroy = true;
                return;
            }

            if (_end && _stageList.Count == 0)
            {
                WaitDestroy = true;
                return;
            }
            for (int i = _stageList.Count - 1; i >= 0; --i)
            {
                ActionStage _curStage = _stageList[i];
                _curStage.Update();

                if (_curStage.CurrentStage == ActionStage.StageState.Stop)
                {
                    _curStage = null;
                    _stageList.RemoveAt(i);
                }
            }

            if (_end)
            {
                return;
            }

            //if (ProcessInterface != null && ProcessInterface["OnUpdate"] != null)
            //{
            //    ((LuaFunction)ProcessInterface["OnUpdate"]).call(ActorMgr.Instance.GetActor(GetRealAttacker()), ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
            //}

            if (LifeTime > 0 && _curLifeTime >= LifeTime)
            {
                OnEnd();
                return;
            }

            _curLifeTime += Time.deltaTime;

            if (FireTime <= 0f)
            {
                return;
            }
            if (_elapseTime <= 0f)
            {
                OnFire();
            }
            _elapseTime -= Time.deltaTime;
        }

        void OnCast()
        {
            //if (ProcessInterface != null && ProcessInterface["OnBuffCast"] != null)
            //{
            //    ((LuaFunction)ProcessInterface["OnBuffCast"]).call(ActorMgr.Instance.GetActor(GetRealAttacker()), ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
            //}
            ActionStage _curStage = new ActionStage();
            _curStage.StageData = BuffData.CastStage;
            _curStage.OwnerEntity = this;
            _curStage.Attacker = this.Attacker;
            _curStage.RealAttacker = this.RealAttacker;
            _curStage.Targeters = this.Targeters;
            _curStage.SpecailPos = this.SpecailPos;
            _curStage.Play();
            _stageList.Add(_curStage);
            _elapseTime = FireTime;
        }

        void OnFire()
        {
            //if (ProcessInterface != null && ProcessInterface["OnBuffFire"] != null)
            //{
            //    ((LuaFunction)ProcessInterface["OnBuffFire"]).call(ActorMgr.Instance.GetActor(GetRealAttacker()), ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
            //}
            ActionStage _curStage = new ActionStage();
            _curStage.StageData = BuffData.FireStage;
            _curStage.OwnerEntity = this;
            _curStage.Attacker = this.Attacker;
            _curStage.RealAttacker = this.RealAttacker;
            _curStage.Targeters = this.Targeters;
            _curStage.SpecailPos = this.SpecailPos;
            _curStage.Play();
            _stageList.Add(_curStage);
            _elapseTime = FireTime;
        }

        public void OnEnd()
        {
            //if (ProcessInterface != null && ProcessInterface["OnBuffEnd"] != null)
            //{
            //    ((LuaFunction)ProcessInterface["OnBuffEnd"]).call(ActorMgr.Instance.GetActor(GetRealAttacker()), ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
            //}
            ActionStage _curStage = new ActionStage();
            _curStage.StageData = BuffData.EndStage;
            _curStage.OwnerEntity = this;
            _curStage.Attacker = this.Attacker;
            _curStage.RealAttacker = this.RealAttacker;
            _curStage.Targeters = this.Targeters;
            _curStage.SpecailPos = this.SpecailPos;
            _curStage.Play();
            _stageList.Add(_curStage);
            _end = true;
        }
    }
}
