using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    public class MetaStage
    {
        public List<MetaFrame> FrameList = new List<MetaFrame>();
        public MetaCommon Owner;



        public MetaStage Clone()
        {
            MetaStage skillStage = new MetaStage();
            foreach (MetaFrame sfi in this.FrameList)
            {
                MetaFrame newsfi = new MetaFrame();
                newsfi.Index = sfi.Index;
                newsfi.MetaAtomList = new List<MetaAtom>();
                foreach (MetaAtom sa in sfi.MetaAtomList)
                {
                    MetaAtom newsa = null;
                    if (sa != null) newsa = sa.Clone();
                    newsfi.MetaAtomList.Add(newsa);
                }
                skillStage.FrameList.Add(newsfi);
            }
            return skillStage;
        }
    }
}
