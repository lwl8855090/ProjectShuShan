using UnityEngine;
using System.Collections;

namespace SB
{
    public class MetaCommon
    {
        protected int _maxFlagIndex = 0;

        public int GetFlagIndex()
        {
            return ++_maxFlagIndex;
        }

        public void UpdateFlagIndex(int index)
        {
            if (_maxFlagIndex < index)
                _maxFlagIndex = index;
        }
    } 
}
