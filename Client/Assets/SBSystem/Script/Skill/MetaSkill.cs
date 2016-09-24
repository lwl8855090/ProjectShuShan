using UnityEngine;
using System.Collections;

namespace SB
{
    public class MetaSkill : MetaCommon
    {
        [SerializeField]
        public string SkillName;
        [SerializeField]
        public MetaStage SingStage = null;
        [SerializeField]
        public MetaStage ChannelStage = null;
        [SerializeField]
        public MetaStage CastStage = null;
        [SerializeField]
        public MetaStage EndStage = null;
        [SerializeField]
        public MetaStage PandingStage = null;


        public MetaSkill()
	    {
            SingStage = new MetaStage();
            ChannelStage = new MetaStage();
            CastStage = new MetaStage();
            EndStage = new MetaStage();
            PandingStage = new MetaStage();
	    }
    }
}
