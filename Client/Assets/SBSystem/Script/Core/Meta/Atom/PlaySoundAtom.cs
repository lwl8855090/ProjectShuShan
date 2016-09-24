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
    [Name("播放声音")]
    public class PlaySoundAtom : MetaAtom
    {
        [VariableDecs("标记索引")]
        public int FlagIndex = 0;
        [VariableDecs("目标类型")]
        public eTargetType TargetType;
        [VariableDecs("是否自动销毁")]
        public bool AutoDestroy = true;
        [VariableDecs("骨骼点")]
        public eAvatarType DummyPoint;
        [VariableDecs("声音预设路径")]
        [FileSelect("", "mp3;ogg")]
        public string SoundPrefab;
        [VariableDecs("是否跟随目标")]
        public bool FollowTarget = false;
        [VariableDecs("淡入时间")]
        public float FadeInTime = 0f;
        [VariableDecs("淡出时间")]
        public float FadeOutTime = 0f;



        public virtual ActionAtom CreateAction()
        {
            return new PlaySoundAction();
        }
    }
}
