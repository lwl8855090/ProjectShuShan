using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SB
{
    class EffectArtController : MonoBehaviour
    {
        public float PlayTime = 1.0f;

        private float elapseTime = 0.0f;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (elapseTime >= PlayTime)
            {
                Destroy(gameObject);
                return;
            }
            elapseTime += Time.deltaTime;
        }
    }
}
