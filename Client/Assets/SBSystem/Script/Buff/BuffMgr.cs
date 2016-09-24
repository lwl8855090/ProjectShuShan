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
    public class BuffMgr : Singleton<BuffMgr>
    {
        private Dictionary<string, MetaBuff> _buffMap = new Dictionary<string, MetaBuff>();
        public const string BuffPath = @"Data/Script/Buff/FileNames.txt";


        private List<ActionBuff> _buffEntityList = new List<ActionBuff>();
        public BuffMgr()
        { 
            
        }

        public override void Initilize()
        {
            _buffMap.Clear();
            LoadBuff(BuffPath);
        }
        public override void Update()
        {
            for (int i = _buffEntityList.Count - 1; i >= 0; --i)
            {
                ActionBuff se = _buffEntityList[i];
                se.Update();
                if (se.WaitDestroy)
                {
                    se.OnDestroy();
                    se = null;
                    _buffEntityList.RemoveAt(i);
                }
            }
        }
        public override void Reset()
        {
            for (int i = _buffEntityList.Count - 1; i >= 0; --i)
            {
                ActionBuff se = _buffEntityList[i];
                se.OnEnd();
            }
            _buffEntityList.Clear();
        }

        void LoadBuff(string folder)
        { 
            
        }


        public void Pause()
        {
            for (int i = _buffEntityList.Count - 1; i >= 0; --i)
            {
                ActionBuff se = _buffEntityList[i];
                se.Pause();
            }
        }

        public void Resume()
        {
            for (int i = _buffEntityList.Count - 1; i >= 0; --i)
            {
                ActionBuff se = _buffEntityList[i];
                se.Resume();
            }
        }

        public MetaBuff GetBuff(string name)
        {
            MetaBuff val = null;
            if (_buffMap.TryGetValue(name, out val))
            {
                return val;
            }
            return null;
        }

        public ActionBuff CastBuff(string name)
        {
            MetaBuff buffData = BuffMgr.Instance.GetBuff(name);
            if (buffData == null)
            {
                buffData = new MetaBuff();
            }
            ActionBuff buffEntity = new ActionBuff();
            buffEntity.BuffData = buffData;
            _buffEntityList.Add(buffEntity);
            return buffEntity;
        }



#if UNITY_EDITOR

        public void AddBuffEntity(ActionBuff be)
        {
            _buffEntityList.Add(be);
        }

#endif
    }
}
