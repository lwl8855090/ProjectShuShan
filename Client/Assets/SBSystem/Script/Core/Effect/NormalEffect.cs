using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    class NormalEffect : EffectBase
    {
        override protected void onReset()
        {

        }

        override protected void onInit()
        {

        }

        override protected void onUpdate()
        {
            if (AutoDestroy && _elapseTime >= PlayTime)
            {
                StartDestroy();
            }
            _elapseTime += Time.deltaTime;

            if (_cacheTranform == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 pos = _cacheTranform.TransformPoint(OffsetPos);
            transform.position = pos;

            if (RotateToAttacker && Attacker && Target)
            {
                Vector3 dir = Target.transform.position - Attacker.transform.position;
                dir.y = 0;
                dir.Normalize();

                transform.right = Vector3.Cross(dir, transform.up);
                transform.forward = dir;
            }
            else if (!OnlyTranlate)
            {
                Quaternion quater = Quaternion.Euler(OffsetRotate.x, OffsetRotate.y, OffsetRotate.z);
                Matrix4x4 mat = _cacheTranform.localToWorldMatrix;
                Vector4 vy = mat.GetColumn(1);
                Vector4 vz = mat.GetColumn(2);
                Quaternion wq = Quaternion.LookRotation(new Vector3(vz.x, vz.y, vz.z), new Vector3(vy.x, vy.y, vy.z));
                transform.rotation = wq * quater;
            }
        }
    }
}
