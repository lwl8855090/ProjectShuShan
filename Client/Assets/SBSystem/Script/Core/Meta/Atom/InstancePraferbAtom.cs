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
    [Group("Fsm相关")]
    [Name("实例化Fsm预设")]
    public class InstancePraferbAtom : MetaAtom
    {
        [VariableDecs("实例预设位置类型")]
        public eInstancePosType InstancePosType;

        [VariableDecs("预设路径")]
        [FileSelect("", "prefab")]
        public string PrefabName = string.Empty;

        [VariableDecs("随机半径")]
        public float RandRadius = 0f;

        [VariableDecs("实例化个数")]
        public int RandNum = 1;


        public virtual ActionAtom CreateAction()
        {
            return new InstancePraferbAction();
        }
    }
}
