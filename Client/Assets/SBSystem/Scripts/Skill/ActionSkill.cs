using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    public class ActionSkill : ActionCommon
    {
        public enum eSkillStage
        {
            Init,
            Start,
            Sing,
            Channel,
            Cast,
            End,
            RealEnd,
        }

        public MetaSkill SkillData;
        public float SingTime = 0.0f;
        public float ChannelTime = 0.0f;
        public float CastTime = 0.0f;

        //public LuaTable ProcessInterface;


        public override bool Finished
        {
            get
            {
                return SkillStage > eSkillStage.Cast;
            }
        }

        public eSkillStage SkillStage = eSkillStage.Init;

        private List<ActionStage> _stageList = new List<ActionStage>();

        private float _elapseTime = 0.0f;

        // Use this for initialization
        public void Start()
        {

            StageForward();
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
            if (SkillStage == eSkillStage.RealEnd)
            {
                return;
            }
            if (SkillData == null)
            {
                SkillStage = eSkillStage.RealEnd;
                return;
            }
            //if (SkillStage == eSkillStage.Sing && SingTime > 0)
            //{
            //    //这里判断打断技能
            //    Actor ac = ActorMgr.Instance.GetActor(Attacker);
            //    if (ac != null && ac.CheckFlag(eFlagType.MoveTarget))
            //    {
            //        SkillMgr.Instance.CancelSkill(this);
            //        return;
            //    }
            //}
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

            if (_elapseTime <= 0.0f)
            {
                if (SkillStage < eSkillStage.End)
                    StageForward();
                else if (_stageList.Count == 0 && PaddingNum <= 0)
                {
                    StageForward();
                }
            }
            _elapseTime -= Time.deltaTime;
        }

        void StageForward()
        {
            if (SkillData == null)
            {
                return;
            }
            SkillStage = SkillStage + 1;
            if (SkillStage == eSkillStage.Start)
            {
                //if (ProcessInterface != null && ProcessInterface["OnSkillStart"] != null)
                //{
                //    ((LuaFunction)ProcessInterface["OnSkillStart"]).call(ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
                //}
                StageForward();
            }
            else if (SkillStage == eSkillStage.Sing)
            {
                //if (ProcessInterface != null && ProcessInterface["OnSkillSing"] != null)
                //{
                //    ((LuaFunction)ProcessInterface["OnSkillSing"]).call(ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
                //}
                _elapseTime = SingTime;
                ActionStage _curStage = new ActionStage();
                _curStage.StageData = SkillData.SingStage;
                _curStage.OwnerEntity = this;
                _curStage.Attacker = this.Attacker;
                _curStage.RealAttacker = this.RealAttacker;
                _curStage.Targeters = this.Targeters;
                _curStage.SpecailPos = this.SpecailPos;
                _curStage.Play();
                _stageList.Add(_curStage);
            }
            else if (SkillStage == eSkillStage.Channel)
            {

                //if (ProcessInterface != null && ProcessInterface["OnSkillChannel"] != null)
                //{
                //    ((LuaFunction)ProcessInterface["OnSkillChannel"]).call(ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
                //}
                _elapseTime = ChannelTime;
                ActionStage _curStage = new ActionStage();
                _curStage.StageData = SkillData.ChannelStage;
                _curStage.OwnerEntity = this;
                _curStage.Attacker = this.Attacker;
                _curStage.RealAttacker = this.RealAttacker;
                _curStage.Targeters = this.Targeters;
                _curStage.SpecailPos = this.SpecailPos;
                _curStage.Play();
                _stageList.Add(_curStage);
            }
            else if (SkillStage == eSkillStage.Cast)
            {

                //if (ProcessInterface != null && ProcessInterface["OnSkillCast"] != null)
                //{
                //    ((LuaFunction)ProcessInterface["OnSkillCast"]).call(ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
                //}
                _elapseTime = CastTime;
                ActionStage _curStage = new ActionStage();
                _curStage.StageData = SkillData.CastStage;
                _curStage.OwnerEntity = this;
                _curStage.Attacker = this.Attacker;
                _curStage.RealAttacker = this.RealAttacker;
                _curStage.Targeters = this.Targeters;
                _curStage.SpecailPos = this.SpecailPos;
                _curStage.Play();
                _stageList.Add(_curStage);
            }
            else if (SkillStage == eSkillStage.End)
            {
                //Actor ac = ActorMgr.Instance.GetActor(Attacker);
                //if (ac != null)
                //{
                //    ac.ReSetFlag(eFlagType.CastSkill);
                //}
                //if (ProcessInterface != null && ProcessInterface["OnSkillEnd"] != null)
                //{
                //    ((LuaFunction)ProcessInterface["OnSkillEnd"]).call(ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
                //}
                _elapseTime = 0;
                ActionStage _curStage = new ActionStage();
                _curStage.StageData = SkillData.EndStage;
                _curStage.OwnerEntity = this;
                _curStage.Attacker = this.Attacker;
                _curStage.RealAttacker = this.RealAttacker;
                _curStage.Targeters = this.Targeters;
                _curStage.SpecailPos = this.SpecailPos;
                _curStage.Play();
                _stageList.Add(_curStage);
            }
        }

        public void CancelSkill()
        {
            //Actor ac = ActorMgr.Instance.GetActor(Attacker);
            //if (ac != null)
            //{
            //    ac.ReSetFlag(eFlagType.CastSkill);
            //}
            SkillStage = eSkillStage.End;
            //if (ProcessInterface != null && ProcessInterface["OnSkillEnd"] != null)
            //{
            //    ((LuaFunction)ProcessInterface["OnSkillEnd"]).call(ActorMgr.Instance.GetActor(GetAttacker()), ProcessInterface);
            //}
            _elapseTime = 0;
            ActionStage _curStage = new ActionStage();
            _curStage.StageData = SkillData.EndStage;
            _curStage.OwnerEntity = this;
            _curStage.Attacker = this.Attacker;
            _curStage.RealAttacker = this.RealAttacker;
            _curStage.Targeters = this.Targeters;
            _curStage.SpecailPos = this.SpecailPos;
            _curStage.Play();
            _stageList.Add(_curStage);
        }

        public override ActionStage TriggerPandingStage()
        {
            ActionStage _curStage = new ActionStage();
            _curStage.StageData = SkillData.PandingStage;
            _curStage.OwnerEntity = this;
            _curStage.Attacker = this.Attacker;
            _curStage.RealAttacker = this.RealAttacker;
            _curStage.Pause();
            _stageList.Add(_curStage);
            return _curStage;
        }
    }
}
