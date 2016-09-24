using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;

namespace SB
{
    public class SkillMgr : Singleton<SkillMgr>
    {
        private Dictionary<string, MetaSkill> _skillMap = new Dictionary<string, MetaSkill>();
        public const string SkillPath = @"Data/Script/Skill/FileNames.txt";

        private List<ActionSkill> _skillEntityList = new List<ActionSkill>();
        public List<EffectBase> Effects = new List<EffectBase>();
        public SkillMgr()
        { 
            
        }

        public override void Initilize()
        {
            _skillMap.Clear();
            LoadSkill(SkillPath);
        }
        public override void Update()
        {
            for (int i = _skillEntityList.Count - 1; i >= 0; --i)
            {
                ActionSkill se = _skillEntityList[i];
                se.Update();
                if (se.SkillStage == ActionSkill.eSkillStage.RealEnd)
                {
                    se.OnDestroy();
                    se = null;
                    _skillEntityList.RemoveAt(i);
                }
            }
            for (int i = Effects.Count - 1; i >= 0; --i)
            {
                if (Effects[i] == null)
                {
                    Effects.RemoveAt(i);
                }
            }
        }
        public override void Reset()
        {
            for (int i = _skillEntityList.Count - 1; i >= 0; --i)
            {
                ActionSkill se = _skillEntityList[i];
                se.CancelSkill();
            }
            _skillEntityList.Clear();
        }

        void LoadSkill(string folder)
        { 
            
        }


        public void Pause()
        {
            for (int i = _skillEntityList.Count - 1; i >= 0; --i)
            {
                ActionSkill se = _skillEntityList[i];
                se.Pause();
            }
            foreach (EffectBase eff in Effects)
            {
                if (eff != null)
                {
                    eff.Pause();
                }
            }
        }


        public void Resume()
        {
            for (int i = _skillEntityList.Count - 1; i >= 0; --i)
            {
                ActionSkill se = _skillEntityList[i];
                se.Resume();
            }
            foreach (EffectBase eff in Effects)
            {
                if (eff != null)
                {
                    eff.Resume();
                }
            }
        }


        public MetaSkill GetSkill(string name)
        {
            MetaSkill val = null;
            if (_skillMap.TryGetValue(name, out val))
            {
                return val;
            }
            return null;
        }



        public ActionSkill CastSkill(string name)
        {
            MetaSkill skillData = GetSkill(name);
            if (skillData == null)
            {
                skillData = new MetaSkill();
            }
            ActionSkill skillEntity = new ActionSkill();
            skillEntity.SkillData = skillData;
            _skillEntityList.Add(skillEntity);
            return skillEntity;
        }


        public void CancelSkill(ActionSkill se)
        {
            if (se == null)
            {
                return;
            }
            se.CancelSkill();
        }


        public void RemoveEffectEntity(ulong id)
        {
            for (int i = 0; i < Effects.Count; ++i)
            {
                if (Effects[i] != null && Effects[i].ID == id)
                {
                    GameObject.Destroy(Effects[i].gameObject);
                }
            }
        }


#if UNITY_EDITOR

        public void AddSkillEntity(ActionSkill se)
        {
            _skillEntityList.Add(se);
        }

#endif
    }
}
