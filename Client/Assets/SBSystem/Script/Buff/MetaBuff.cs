using UnityEngine;
using System.Collections;

namespace SB
{
    public class MetaBuff
    {
        [SerializeField]
        public string BuffName;
        [SerializeField]
        public MetaStage CastStage = null;
        [SerializeField]
        public MetaStage FireStage = null;
        [SerializeField]
        public MetaStage EndStage = null;


        public MetaBuff()
	    {
            CastStage = new MetaStage();
            FireStage = new MetaStage();
            EndStage = new MetaStage();
	    }
    }
}
