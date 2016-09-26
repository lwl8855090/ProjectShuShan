using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace SB
{
    public abstract class ActionCommon
    {
        //表现释放者
        private ulong _attacker;
        public ulong Attacker
        {
            get { return _attacker; }
            set
            {
                _attacker = value;
                _realAttacker = _attacker;
            }
        }
        //真实释放者，用于伤害计算。目前貌似不用，留着。
        private ulong _realAttacker;
        public ulong RealAttacker
        {
            get { return _realAttacker; }
            set { _realAttacker = value; }
        }
        //攻击的目标，可以是多个
        private List<ulong> _targeters = new List<ulong>();
        public List<ulong> Targeters
        {
            get { return _targeters; }
        }
        //对地面释放，比如暴风雪技能
        private Vector3 _specailPos = Vector3.zero;
        public Vector3 SpecailPos
        {
            get { return _specailPos; }
            set { _specailPos = value; }
        }
        //技能挂起引用计数。主要用于追踪、射击特效，一旦出发这种特效，引用计数++，这样这个技能不会自然结束，一旦打击到敌人，会触发paddingstage。
        public int PaddingNum = 0;

        public abstract bool Finished { get; }

        public Dictionary<int, List<ulong>> EffectMap = new Dictionary<int, List<ulong>>();
        public Dictionary<int, List<ulong>> SoundMap = new Dictionary<int, List<ulong>>();

        protected bool _paused = false;

        public virtual int OnCalcDamage(int index, ulong attacker, ulong targeter)
        {
            return 0;
        }

        public virtual void OnDestroy()
        {
            //技能结束的时候，需要把策划没有配置结束的特效或者音效强制删除。（避免策划错误）
            foreach (List<ulong> EffList in EffectMap.Values)
            {
                foreach (ulong cp in EffList)
                {
                    if (cp != null)
                    {
                        //EffectResMgr.Instance.RemoveEffect(cp);
                        //SkillMgr.Instance.RemoveEffectEntity(cp);
                    }
                }
            }
            EffectMap.Clear();
            foreach (List<ulong> SndList in SoundMap.Values)
            {
                foreach (ulong cp in SndList)
                {
                    //SoundEngine.Instance.StopSound(cp);
                }
            }
            SoundMap.Clear();
        }


        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }


        public virtual ActionStage TriggerPandingStage()
        {
            return null;
        }

    }
}
