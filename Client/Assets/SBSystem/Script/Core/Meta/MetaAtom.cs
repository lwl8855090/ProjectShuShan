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
    public abstract class MetaAtom
    {
        [HideInInspector]
        [NonSerialized]
        public MetaCommon Owner = null;
        [HideInInspector]
        public int Index = 0;


        public MetaAtom Clone()
        {
            return (MetaAtom)(this.MemberwiseClone());
        }


        public virtual ActionAtom CreateAction()
        {
            return null;
        }
    }
}
