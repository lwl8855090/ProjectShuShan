using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB
{
    public abstract class ActionAtom
    {
        public ActionCommon OwnerEntity;
        public ActionStage OwnerStageEntity;
        public MetaAtom AtomData;


        public abstract void Excuse();
    }
}
