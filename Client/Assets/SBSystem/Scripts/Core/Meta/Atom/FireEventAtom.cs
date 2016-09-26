using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SB
{
    [Group("事件相关")]
    [Name("添加事件")]
    public class FireEventAtom : MetaAtom
    {
        public enum eEventType
        {
            None,
            Damage,
            MoveForward,
            LockActor,
            UnLockActor,
            CameraShake,
            TimeScale,
            SelectDir,
            UnSelectDir,
            GenerateForce,
            UnGenerateForce,
            Pause,
            Resume,
            ChangeScale,
        }
        //基础属性
        public FireEventAtom.eEventType EventType = FireEventAtom.eEventType.None;

        //伤害相关
        public int DamageIndex = 0;
        public bool DamageReCalcTars = true;
        public eTargeterType DamageTargetType = eTargeterType.Enemys;
        public int DamageForceLevel = 0;
        public float DamageAngle = 45.0f;
        public float DamageDistance = 3.0f;
        public bool DamageRect = false;
        public float DamageWidth = 0.0f;
        public float DamageHeight = 0.0f;
        public bool DamageCalcByAttacker = true;

        //MoveForward damage generate 通用
        public float SpeedUpTime = 0.5f;
        public float SpeedConstTime = 0.2f;
        public float SpeedDownTime = 0.2f;
        public float SpeedMax = 0;
        public bool MoveNoPhysics = false;

        //generate
        public int GenerateForceLevel = 1;

        //CameraShake
        public bool ShakeOnlyLeader = true;
        public int NumberOfShakes = 2;
        public Vector3 ShakeAmount = Vector3.one;
        public Vector3 RotationAmount = Vector3.one;
        public float ShakeDistance = 00.10f;
        public float ShakeSpeed = 50.00f;
        public float ShakeDecay = 00.20f;
        public float GuiShakeModifier = 01.00f;
        public bool MultiplyByTimeScale = true;

        //TimeScale
        public float TimeScale = 1.0f;

        //ChangeScale
        public float DestScale = 1.0f;
        public float ChangTime = 1.0f;



        public virtual ActionAtom CreateAction()
        {
            return new FireEventAction();
        }

#if UNITY_EDITOR
        public void OnGUI()
        {
            EventType = (FireEventAtom.eEventType)EditorGUILayout.EnumPopup("事件类型", EventType);
            EditorGUILayout.Space();
            if (EventType == FireEventAtom.eEventType.Damage)
            {
                DamageIndex = EditorGUILayout.IntField("伤害索引", DamageIndex);
                DamageReCalcTars = EditorGUILayout.Toggle("是否重新计算伤害目标", DamageReCalcTars);
                DamageTargetType = (eTargeterType)EditorGUILayout.EnumPopup("伤害目标类型", DamageTargetType);
                DamageForceLevel = EditorGUILayout.IntField("受力级别", DamageForceLevel);
                DamageAngle = EditorGUILayout.FloatField("伤害角度", DamageAngle);
                DamageDistance = EditorGUILayout.FloatField("伤害距离", DamageDistance);
                DamageRect = EditorGUILayout.Toggle("是否矩形计算伤害", DamageRect);
                DamageWidth = EditorGUILayout.FloatField("伤害宽", DamageWidth);
                DamageHeight = EditorGUILayout.FloatField("伤害高", DamageHeight);
                DamageCalcByAttacker = EditorGUILayout.Toggle("伤害计算是否以攻击者为中心", DamageCalcByAttacker);
            }
            if (EventType == FireEventAtom.eEventType.MoveForward || EventType == FireEventAtom.eEventType.Damage || EventType == FireEventAtom.eEventType.GenerateForce)
            {
                SpeedUpTime = EditorGUILayout.FloatField("加速时间", SpeedUpTime);
                SpeedConstTime = EditorGUILayout.FloatField("匀速时间", SpeedConstTime);
                SpeedDownTime = EditorGUILayout.FloatField("减速时间", SpeedDownTime);
                SpeedMax = EditorGUILayout.FloatField("最大速度", SpeedMax);
                if (EventType == FireEventAtom.eEventType.MoveForward)
                {
                    MoveNoPhysics = EditorGUILayout.Toggle("无物理判断", MoveNoPhysics);
                }
            }
            if (EventType == FireEventAtom.eEventType.CameraShake)
            {
                ShakeOnlyLeader = EditorGUILayout.Toggle("摄像机震动是否只队长触发", ShakeOnlyLeader);
                NumberOfShakes = EditorGUILayout.IntField("NumberOfShakes", NumberOfShakes);
                ShakeAmount = EditorGUILayout.Vector3Field("ShakeAmount", ShakeAmount);
                RotationAmount = EditorGUILayout.Vector3Field("RotationAmount", RotationAmount);
                ShakeDistance = EditorGUILayout.FloatField("ShakeDistance", ShakeDistance);
                ShakeSpeed = EditorGUILayout.FloatField("ShakeSpeed", ShakeSpeed);
                ShakeDecay = EditorGUILayout.FloatField("ShakeDecay", ShakeDecay);
                GuiShakeModifier = EditorGUILayout.FloatField("GuiShakeModifier", GuiShakeModifier);
                MultiplyByTimeScale = EditorGUILayout.Toggle("MultiplyByTimeScale", MultiplyByTimeScale);
            }
            if (EventType == FireEventAtom.eEventType.TimeScale)
            {
                TimeScale = EditorGUILayout.FloatField("时间缩放比", TimeScale);
            }

            if (EventType == FireEventAtom.eEventType.GenerateForce)
            {
                GenerateForceLevel = EditorGUILayout.IntField("产生推力的级别", GenerateForceLevel);
            }

            if (EventType == FireEventAtom.eEventType.ChangeScale)
            {
                DestScale = EditorGUILayout.FloatField("缩放比例", DestScale);
                ChangTime = EditorGUILayout.FloatField("缩放时间", ChangTime);
            }
            EditorGUILayout.Space();
        }
#endif
    }
}
