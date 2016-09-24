using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    class TraceEffect : EffectBase
    {
        public eTraceType TraceType;
        public eAvatarType TraceDummyPoint;
        public float TraceOffsetLen = 0f;
        public float TraceOffsetAngle = 0f;
        public float TraceStandDis = 0f;
        public float Speed = 10.0f;
        public float FactorVal = 0.5f;

        protected Vector3 _TraceStartPos = Vector3.zero;
        protected float _curTraceOffsetRate = 0f;
        private float _traceMovedDis = 0f;
        override protected void onReset()
        {
            _TraceStartPos = Vector3.zero;
            _curTraceOffsetRate = 0f;
            _traceMovedDis = 0f;
        }

        override protected void onInit()
        {
            if (Attacker != null)
            {
                _cacheTranform = Attacker.FindInChildren(DummyPoint.ToString());
                if (_cacheTranform == null)
                {
                    _cacheTranform = Attacker.transform;
                }
                if (TraceType == eTraceType.eTraceType_SpecialPos)
                {
                    if (SpecialPos == Vector3.zero)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    _lastTracePos = SpecialPos;
                }
                else
                {
                    _lastTracePos = Target.transform.position;
                }
            }
        }
        override protected void onPostInit()
        {
            _TraceStartPos = transform.position;
        }

        override protected void onUpdate()
        {
            if (TraceType == eTraceType.eTraceType_Targeter)
            {
                if (Target != null)
                {
                    Transform targetObj = Target.FindInChildren(TraceDummyPoint.ToString());
                    if (targetObj == null)
                    {
                        targetObj = Target.transform;
                    }
                    _lastTracePos = targetObj.position;
                }
            }
            float fRestDis = Vector3.Distance(_lastTracePos, _TraceStartPos);
            Vector3 dir = _lastTracePos - _TraceStartPos;
            dir.Normalize();




            _traceMovedDis += Speed * Time.deltaTime;

            _curTraceOffsetRate = _traceMovedDis / (_traceMovedDis + fRestDis);

            if (Speed * Time.deltaTime >= Vector3.Distance(_TraceStartPos, _lastTracePos))
            {
                OnTracedTarget();
                return;
            }
            _TraceStartPos += dir * Speed * Time.deltaTime;
            float fYOffset = 0f;
            float fRate = _curTraceOffsetRate <= 0f ? 0f : (_curTraceOffsetRate <= 0.5f ? (_curTraceOffsetRate / 0.5f) : (1f - _curTraceOffsetRate) / 0.5f);
            float fOffRate = TraceStandDis <= 0 ? 1f : (_traceMovedDis + fRestDis) / TraceStandDis;
            fYOffset = (TraceOffsetLen * fOffRate * fRate - TraceOffsetLen * fOffRate * fRate * fRate / 2) * 2;


            //fYOffset = fRate * TraceOffsetLen;

            Vector3 vecUp = Vector3.Cross(dir, Vector3.up);
            vecUp = Vector3.Cross(vecUp, dir);
            Quaternion quater = Quaternion.Euler(0, 0, TraceOffsetAngle);
            vecUp = quater * vecUp;

            vecUp.Normalize();

            vecUp *= fYOffset;
            Vector3 curPos = _TraceStartPos;
            curPos += vecUp;
            transform.position = curPos;



            Vector3 tmpVecUp = Vector3.up * TraceOffsetLen * fOffRate;
            tmpVecUp = tmpVecUp - tmpVecUp * 2f * _curTraceOffsetRate;
            tmpVecUp = quater * tmpVecUp;
            Vector3 tmpVecFor = _lastTracePos - _TraceStartPos;
            tmpVecFor.Normalize();
            if (TraceOffsetLen > 0f)
                tmpVecFor *= TraceOffsetLen * fOffRate * FactorVal;

            transform.forward = tmpVecUp + tmpVecFor;
        }

        public virtual void OnTracedTarget()
        {
            transform.position = _lastTracePos;
            StartDestroy();
            _curTraceOffsetRate = 1f;
            SpawnDamage();
        }
    }
}
