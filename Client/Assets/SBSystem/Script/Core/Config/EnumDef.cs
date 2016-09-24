using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB
{
    public enum eTargetType
    {
        TargetType_Self,
        TargetType_Target,
    }


    public enum eEffectType
    {
        Attach,
        Position,
        Bullet,
        Trace,
        Boomerang,
        Bounce,
        Thunder,
        Laser,  //激光
    }


    public enum eTraceType
    {
        eTraceType_Targeter,
        eTraceType_TargeterPos,
        eTraceType_SpecialPos,
    }




    public enum eTargeterType
    {
        //对于玩家
        Own,	//自己
        TeamLeader,	//队长

        Friends,	//友方
        Enemys,		//敌人
        Any,		//任何对象
    }


    public enum eInstancePosType
    {
        eInstancePosType_Caster,
        eInstancePosType_Targeter,
        eInstancePosType_Pos,
    }
}
