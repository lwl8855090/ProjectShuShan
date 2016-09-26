using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB
{
    public enum eAvatarType
    {
        root,
        left_hand,
        right_hand,
        left_foot,
        right_foot,
        damage,
        titlepoint,
        weapon,
    }

    public enum eAnimType
    {
        run01,
        walk01,
        standnormal01,
        idle01,
        roll01,
        damage01,
        damage02,
        damage03,
        damage04,
        dead01,
        parry01,
        stun01,
        attackstand01,
        a1_1,
        a1_2,
        a1_3,
        a1_4,
        a1_5,
        a1_6,
        skill01,
        skill02,
        skill03,
        skill04,
        skill05,
        skill06,
        skill07,
        skill08,
        skill09,
        skill10,
        skill11,
        skill12,
        skill13,
        skill14,
        skill15,
        skill01_start,
        skill01_loop,
        skill01_end,
        skill02_start,
        skill02_loop,
        skill02_end,
        skill03_start,
        skill03_loop,
        skill03_end,
        skill04_start,
        skill04_loop,
        skill04_end,
        skill05_start,
        skill05_loop,
        skill05_end,
        skill06_start,
        skill06_loop,
        skill06_end,
        a1_7,
        a1_8,
        a1_9,
        a1_10,
        win01,
        appear01,
        lose01,
        operation01,
        upgrade01,
        talk01,
        flaunt01,
        plot01,


        max_cnt,
    }

    class ArtConfig : Singleton<ArtConfig>
    {
        private string[] _animNames = null;
        public void Initialize()
        {
            _animNames = new string[(int)eAnimType.max_cnt];
            for (int i = 0; i < (int)eAnimType.max_cnt; ++i)
            {
                eAnimType type = (eAnimType)i;
                _animNames[i] = type.ToString();
            }
        }

        public string GetAnimName(eAnimType type)
        {
            return _animNames[(int)type];
        }
        public void Reset()
        {

        }

        public void Update()
        {

        }
    }
}
