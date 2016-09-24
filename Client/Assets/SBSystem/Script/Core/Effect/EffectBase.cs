using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SB
{
    public class EffectBase : MonoBehaviour
    {
        public static ulong MaxID = 0;
        public ulong ID;
        public GameObject Attacker;
        public GameObject Target;

        public eAvatarType DummyPoint;
        public Vector3 OffsetPos = Vector3.zero;
        public Vector3 OffsetRotate = Vector3.zero;
        public Vector3 SpecialPos = Vector3.zero;

        public float Scale = 1.0f;
        public bool AutoDestroy = true;
        public float DelayDestroyTime = 0f;
        public bool RotateToAttacker = false;
        public float PlayTime = 1.0f;

        public bool OnlyTranlate = false;
        public string InstancePraferb;

        public ActionCommon OwnerEntity;

        protected float _elapseTime = 0.0f;
        protected bool _startDestroy = false;
        protected Vector3 _lastTracePos = Vector3.zero;


        protected bool _paused = false;

        protected Transform _cacheTranform = null;
        void OnEnable()
        {
            SkillMgr.Instance.Effects.Add(this);
        }

        void OnDisable()
        {
            SkillMgr.Instance.Effects.Remove(this);
        }

        virtual protected void OnRealDestroy()
        {

        }

        protected void RealDestroy()
        {
            ParticleSystem[] pars = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem par in pars)
            {
                par.startSize /= Scale;
            }
            OnRealDestroy();
            GameObject.Destroy(this);
            transform.localScale /= Scale;
            Destroy(gameObject);
            if (OwnerEntity != null)
            {
                OwnerEntity.PaddingNum--;
            }
            onRealDestroy();
        }

        protected virtual void onRealDestroy()
        {

        }
        void OnTriggerEnter(Collider obj)
        {
            onTriggerEnter(obj);
        }
        void Update()
        {
            if (_paused)
            {
                return;
            }
            if (_startDestroy)
            {
                if (_elapseTime >= DelayDestroyTime)
                {
                    RealDestroy();
                    return;
                }
                _elapseTime += Time.deltaTime;
                return;
            }
            onUpdate();
        }

        public void Pause()
        {
            _paused = true;
            Animator[] anims = GetComponentsInChildren<Animator>();
            foreach (Animator anim in anims)
            {
                anim.enabled = false;
            }
            ParticleSystem[] pars = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem par in pars)
            {
                par.Pause();
            }
        }

        public void Resume()
        {
            _paused = false;
            Animator[] anims = GetComponentsInChildren<Animator>();
            foreach (Animator anim in anims)
            {
                anim.enabled = true;
            }
            ParticleSystem[] pars = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem par in pars)
            {
                if (par.isPaused)
                    par.Play();
            }
        }
        protected void Reset()
        {
            onReset();
            _elapseTime = 0.0f;
            _startDestroy = false;
            _lastTracePos = Vector3.zero;
            _cacheTranform = null;
        }

        public void Init()
        {
            Reset();
            transform.localScale = new Vector3(Scale * transform.localScale.x, Scale * transform.localScale.y, Scale * transform.localScale.z);
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }
            ParticleSystem[] pars = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem par in pars)
            {
                par.startSize *= Scale;
            }

            _cacheTranform = Target.FindInChildren(DummyPoint.ToString());
            if (_cacheTranform == null)
            {
                _cacheTranform = Target.transform;
            }
            if (_cacheTranform == null)
            {
                Destroy(gameObject);
                return;
            }
            onInit();

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
            else
            {
                Quaternion quater = Quaternion.Euler(OffsetRotate.x, OffsetRotate.y, OffsetRotate.z);
                Matrix4x4 mat = _cacheTranform.localToWorldMatrix;
                Vector4 vy = mat.GetColumn(1);
                Vector4 vz = mat.GetColumn(2);
                Quaternion wq = Quaternion.LookRotation(new Vector3(vz.x, vz.y, vz.z), new Vector3(vy.x, vy.y, vy.z));
                transform.rotation = wq * quater;
            }
            onPostInit();
        }
        protected void StartDestroy()
        {
            _startDestroy = true;
            _elapseTime = 0f;
        }

        protected void SpawnDamage()
        {
            onSpawnDamage();
        }


        //protected bool CheckActorEx(Actor ac, float radius, bool is2D)
        //{
        //    float distance = 0f;
        //    if (!is2D)
        //    {
        //        distance = Vector3.Distance(ac.Position, transform.position);
        //    }
        //    else
        //    {
        //        Vector2 effectPos = new Vector2(transform.position.x, transform.position.z);
        //        Vector2 actorPos = new Vector2(ac.Position.x, ac.Position.z);
        //        distance = Vector2.Distance(effectPos, actorPos);
        //    }
        //    if (distance <= radius) return true;
        //    else return false;
        //}

        //protected List<Actor> GetTargeters(Actor actor, float radius, bool ignoreZ = false)
        //{
        //    ActorTemplate _actorTmp = actor.Template;
        //    List<Actor> res = new List<Actor>();
        //    foreach (Actor ac in ActorMgr.Instance.Actors.Values)
        //    {
        //        if (ac == actor) continue;
        //        ActorTemplate atmp = ac.Template;
        //        if (atmp == null)
        //        {
        //            continue;
        //        }
        //        if (_actorTmp.Type != atmp.Type)
        //        {
        //            continue;
        //        }
        //        if (!CheckActorEx(ac, radius, ignoreZ))
        //            continue;
        //        res.Add(ac);
        //    }
        //    return res;
        //}

        //protected List<Actor> GetTargeters(Actor own, float damageWidth, float damageHeight)
        //{
        //    ActorTemplate _actorTmp = own.Template;
        //    List<Actor> res = new List<Actor>();
        //    foreach (Actor ac in ActorMgr.Instance.RealActors)
        //    {
        //        if (own.CommonData.Camp == ac.CommonData.Camp)
        //        {
        //            continue;
        //        }
        //        if (!CheckActorEx(ac, damageWidth, damageHeight))
        //            continue;
        //        res.Add(ac);
        //    }
        //    return res;
        //}

        //protected virtual bool CheckActorEx(Actor ac, float damageWidth, float damageHeight)
        //{
        //    Vector3 distance = ac.Position - transform.position;
        //    Vector3 dis = distance;
        //    dis.Normalize();
        //    Vector3 forwardDir = transform.forward;
        //    forwardDir.Normalize();
        //    float angle = (float)System.Math.Acos((double)Vector3.Dot(dis, forwardDir));
        //    if (angle > 1.57)
        //    {
        //        return false;
        //    }
        //    float angle2 = 3.14f - angle;
        //    float offY = (float)System.Math.Cos((double)angle2) * Vector3.Distance(ac.Position, transform.position);
        //    float offX = (float)System.Math.Sin((double)angle2) * Vector3.Distance(ac.Position, transform.position);

        //    if (System.Math.Abs(offX) > damageWidth / 2 || System.Math.Abs(offY) > damageHeight)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        virtual protected void onReset()
        {

        }

        virtual protected void onInit()
        {
        }

        virtual protected void onPostInit()
        {
        }
        virtual protected void onUpdate()
        {

        }

        virtual protected void onTriggerEnter(Collider obj)
        {

        }

        virtual protected void onSpawnDamage()
        {
            if (OwnerEntity != null)
            {
                ActionStage stageEntity = OwnerEntity.TriggerPandingStage();
                if (stageEntity == null)
                    return;
                if (Target != null)
                    stageEntity.Targeters.Add((ulong)Target.GetInstanceID());
                stageEntity.SpecailPos = transform.position;
                stageEntity.Play();
            }
        }
    }

}
