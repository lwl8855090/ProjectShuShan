using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    class BulletEffect : EffectBase
    {
        public float Speed = 10.0f;
        public float LifeTime = 3.0f;
        public bool BulletACross = false;
        public float BulletDamageWidth = 0f;
        override protected void onReset()
        {

        }

        override protected void onInit()
        {
            if (!BulletACross || (BulletACross && BulletDamageWidth > 0f))
            {
                Rigidbody rd = Utility.AddIfMissing<Rigidbody>(gameObject);
                if (rd)
                {
                    rd.isKinematic = true;
                }
            }
        }
        override protected void onPostInit()
        {

        }

        override protected void onUpdate()
        {
            if (_elapseTime >= LifeTime)
            {
                Target = null;
                StartDestroy();
                return;
            }

            Vector3 dir = transform.forward;
            dir.y = 0;
            dir.Normalize();
            for (; ; )
            {
                if (!BulletACross)
                    break;
                if (BulletDamageWidth <= 0f)
                    break;
                //ActorTemplate ac = Attacker.GetComponent<ActorTemplate>();
                //if (ac == null || ac.OwnerActor == null)
                //    break;

                //List<Actor> tars = GetTargeters(ac.OwnerActor, BulletDamageWidth, Speed * Time.deltaTime);
                //foreach (Actor tar in tars)
                //{
                //    Target = tar.gameObject;
                //    SpawnDamage();
                //}
                break;
            }

            transform.position += dir * Speed * Time.deltaTime;

            _elapseTime += Time.deltaTime;
        }

        override protected void onTriggerEnter(Collider obj)
        {
            if (Attacker == null) return;
            //ActorTemplate acAttacker = Attacker.GetComponent<ActorTemplate>();
            //ActorTemplate ac = obj.gameObject.GetComponent<ActorTemplate>();
            //if (acAttacker == ac)
            //    return;
            //if (acAttacker == null || ac == null || acAttacker.OwnerActor == null || ac.OwnerActor == null)
            //{
            //    return;
            //}
            //if (acAttacker.OwnerActor.CommonData.Camp == ac.OwnerActor.CommonData.Camp)
            //{
            //    return;
            //}
            //if (BulletACross && BulletDamageWidth > 0f)
            //{
            //    return;
            //}
            //Target = ac.gameObject;
            //if (!BulletACross)
            //    StartDestroy();
            //SpawnDamage();
        }
    }
}
