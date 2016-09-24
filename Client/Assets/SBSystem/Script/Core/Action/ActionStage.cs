using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SB
{
    public class ActionStage
    {
        public enum StageState
	    {
		    Init,
		    Play,
		    Pause,
		    Stop,
	    }

        #region 以下几个变量要和ActionCommon里面的对应起来，因为在技能的不同释放阶段，这些变量会改变

        public ulong Attacker;
        public ulong RealAttacker;
        public List<ulong> Targeters = new List<ulong>();

        public Vector3 SpecailPos = Vector3.zero;

        #endregion

	    public StageState CurrentStage;
	    public MetaStage StageData;
	    public ActionCommon OwnerEntity;


	    private int _curIndex;
	    private float _elapseTime = 0.0f;

        public ActionStage()
	    {
		    Start ();
	    }
	    // Use this for initialization
	    public void Start () {
		    CurrentStage = StageState.Play;
		    _curIndex = 0;
		    _elapseTime = 0.0f;
	    }
	
	    // Update is called once per frame
	    public void Update () {
		    if (CurrentStage != StageState.Play) {
			    return;		
		    }
		    if (_curIndex >= StageData.FrameList.Count) {
			    Stop ();
			    return;
		    }
		    float fRate = 1.0f;
		    MetaFrame frameInfo = StageData.FrameList [_curIndex];
		    if (_elapseTime*fRate >= frameInfo.Index * 0.01f) {
			    foreach(MetaAtom atom in frameInfo.MetaAtomList)
			    {
				    if(atom!=null)
				    {
                        ActionAtom action = atom.CreateAction();
					    if(action == null)
					    {
						    continue;
					    }
                        action.OwnerEntity = OwnerEntity;
                        action.OwnerStageEntity = this;
					    action.AtomData = atom;
					    action.Excuse();
				    }
			    }
			    ++_curIndex;
		    }

		    _elapseTime += Time.deltaTime;
	    }

	    public void Play()
	    {
		    CurrentStage = StageState.Play;
	    }
	    public void Pause()
	    {
		    CurrentStage = StageState.Pause;
	    }
	    public void Stop()
	    {
		    CurrentStage = StageState.Stop;
	    }
    }
}
