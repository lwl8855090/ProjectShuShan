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
    [Group("动作相关")]
    [Name("停止动作")]
    public class StopAnimAtom : MetaAtom
    {
        [VariableDecs("动作类型")]
        public eAnimType AnimType;
        [VariableDecs("目标类型")]
        public eTargetType TargetType;



        public virtual ActionAtom CreateAction()
        {
            return new StopAnimAction();
        }
    }
}
