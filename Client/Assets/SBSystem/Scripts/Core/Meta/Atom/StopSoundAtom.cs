using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SB
{
    [Group("声音相关")]
    [Name("删除声音")]
    public class StopSoundAtom : MetaAtom
    {
        [VariableDecs("标记索引")]
        public int FlagIndex = 0;


        public virtual ActionAtom CreateAction()
        {
            return new StopSoundAction();
        }
    }
}
