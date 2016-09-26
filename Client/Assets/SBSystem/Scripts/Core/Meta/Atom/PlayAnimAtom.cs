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
    [Name("播放动作")]
    public class PlayAnimAtom : MetaAtom
    {
        [VariableDecs("动作")]
        public eAnimType AnimType;
        [VariableDecs("目标类型")]
        public eTargetType TargetType;
        [VariableDecs("动作模式")]
        public WrapMode AnimMode = WrapMode.Default;
        [VariableDecs("动作速率")]
        public float AnimSpeed = 1.0f;
        [VariableDecs("淡入时间")]
        public float FadeInTime = 0.0f;


        public virtual ActionAtom CreateAction()
        {
            return new PlayAnimAction();
        }
    }
}
