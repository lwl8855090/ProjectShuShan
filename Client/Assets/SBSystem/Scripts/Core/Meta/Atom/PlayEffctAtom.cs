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
    [Group("特效相关")]
    [Name("播放特效")]
    public class PlayEffctAtom : MetaAtom
    {
        public int FlagIndex = 0;
        public eTargetType TargetType;
        public bool AutoDestroy = true;
        public eAvatarType DummyPoint;
        public string EffectPrefab = string.Empty;

        public eEffectType EffectType = eEffectType.Attach;

        //公共属性	
        public float Scale = 1.0f;
        public bool ScaleApplyModel = true;
        public Vector3 OffsetPos = Vector3.zero;
        public Vector3 OffsetRotate = Vector3.zero;
        public float DelayDestroyTime = 0f;
        public float Speed = 10.0f;
        public bool RotateToAttacker = false;

        //附着特效属性
        public bool OnlyTranslate = false;

        //位置特效相关

        //子弹特效相关
        public float LifeTime = 3.0f;
        public bool BulletACross = false;
        public float BulletDamageWidth = 0f;

        //追踪特效相关
        public eTraceType TraceType = eTraceType.eTraceType_Targeter;
        public eAvatarType TraceDummyPoint = eAvatarType.damage;
        public float TraceOffsetLen = 0f;
        public float TraceOffsetStandDis = 10f;
        public float TraceOffsetAngle = 0f;
        public float FactorVal = 0.5f;

        //回旋镖特效相关
        public float OnceTime = 1f;
        public bool BackToAttaker = false;
        public float BooDamageWidth = 0f;

        //弹跳特效相关
        public int BounceTimes = 3;
        public float BounceDistance = 10f;

        //闪电特效相关
        //	public int LinkedCount = 1; //最大链接目标
        //	public AvatarType TargetDummyPoint = AvatarType.damage;
        public float Distance = 10f; //最大链接距离
        public float KeepTime = 10f;//链接后的保持时间
        public string ThunderPrefab = string.Empty;

        //激光特效相关
        public float LaserMaxLength = 10.0f;
        public float LaserStartWidth = 0.01f;
        public float LaserEndWidth = 0.05f;
        public float LaserCalcDamageTime = 0.2f;
        public string LaserHeadEffect = string.Empty;
        public string LaserTailEffect = string.Empty;


        public string ArrivedInstance;



        public virtual ActionAtom CreateAction()
        {
            return new PlayEffectAction();
        }

#if UNITY_EDITOR
        public void OnGUI()
        {
            EditorGUILayout.LabelField("标记索引", FlagIndex.ToString());
            TargetType = (eTargetType)EditorGUILayout.EnumPopup("目标类型", TargetType);
            AutoDestroy = EditorGUILayout.Toggle("是否自动销毁", AutoDestroy);
            DummyPoint = (eAvatarType)EditorGUILayout.EnumPopup("骨骼点", DummyPoint);
            EditorGUILayout.BeginHorizontal();
            EffectPrefab = EditorGUILayout.TextField("特效预设路径", EffectPrefab);
            if (GUILayout.Button(".", GUILayout.Width(20)))
            {
                string strPath = "Resources/";
                string strVal = EditorUtility.OpenFilePanel("打开特效预设", Application.dataPath + "/Resources", "prefab");
                int iPos = strVal.LastIndexOf(strPath);
                int toPos = strVal.LastIndexOf(".");
                if (iPos != -1 && toPos != -1)
                    EffectPrefab = strVal.Substring(iPos + strPath.Length, toPos - iPos - strPath.Length);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EffectType = (eEffectType)EditorGUILayout.EnumPopup("特效类型", EffectType);
            Scale = EditorGUILayout.FloatField("特效缩放", Scale);
            ScaleApplyModel = EditorGUILayout.Toggle("特效是否随模型缩放比", ScaleApplyModel);
            OffsetPos = EditorGUILayout.Vector3Field("特效位置偏移", OffsetPos);
            OffsetRotate = EditorGUILayout.Vector3Field("特效旋转偏移", OffsetRotate);
            DelayDestroyTime = EditorGUILayout.FloatField("延迟销毁", DelayDestroyTime);
            if (EffectType == eEffectType.Bullet || EffectType == eEffectType.Trace)
            {
                Speed = EditorGUILayout.FloatField("速度", Speed);
            }

            if (EffectType == eEffectType.Attach)
            {
                OnlyTranslate = EditorGUILayout.Toggle("attach特效是否只位置改变", OnlyTranslate);
            }
            RotateToAttacker = EditorGUILayout.Toggle("是否朝向释放者", RotateToAttacker);
            if (EffectType == eEffectType.Bullet)
            {
                LifeTime = EditorGUILayout.FloatField("子弹生命时间", LifeTime);
            }
            if (EffectType == eEffectType.Bullet || EffectType == eEffectType.Trace)
            {
                EditorGUILayout.BeginHorizontal();
                ArrivedInstance = EditorGUILayout.TextField("击中目标实例预设", ArrivedInstance);
                if (GUILayout.Button(".", GUILayout.Width(20)))
                {
                    string strPath = "Resources/";
                    string strVal = EditorUtility.OpenFilePanel("打开击中目标实例预设", Application.dataPath + "/Resources", "prefab");
                    int iPos = strVal.LastIndexOf(strPath);
                    int toPos = strVal.LastIndexOf(".");
                    if (iPos != -1 && toPos != -1)
                        ArrivedInstance = strVal.Substring(iPos + strPath.Length, toPos - iPos - strPath.Length);
                }
                EditorGUILayout.EndHorizontal();
            }
            if (EffectType == eEffectType.Trace)
            {

                TraceType = (eTraceType)EditorGUILayout.EnumPopup("追踪特效类型", TraceType);
                TraceDummyPoint = (eAvatarType)EditorGUILayout.EnumPopup("追踪特效打击骨骼点", TraceDummyPoint);

                TraceOffsetLen = EditorGUILayout.FloatField("抛物线偏移距离", TraceOffsetLen);
                TraceOffsetAngle = EditorGUILayout.FloatField("抛物线偏移角度", TraceOffsetAngle);
                FactorVal = EditorGUILayout.FloatField("抛物线计算因子", FactorVal);
                TraceOffsetStandDis = EditorGUILayout.FloatField("抛物线参考距离", TraceOffsetStandDis);
            }


            if (EffectType == eEffectType.Bullet)
            {

                BulletACross = EditorGUILayout.Toggle("子弹是否穿越", BulletACross);
                BulletDamageWidth = EditorGUILayout.FloatField("子弹穿越伤害宽度", BulletDamageWidth);
            }

            if (EffectType == eEffectType.Boomerang)
            {
                //			Target = GlobalData.TargetType.TargetType_None;
                Speed = EditorGUILayout.FloatField("速度", Speed);
                OnceTime = EditorGUILayout.FloatField("抵达折返点时间", OnceTime);
                BackToAttaker = EditorGUILayout.Toggle("返回到角色", BackToAttaker);
                BooDamageWidth = EditorGUILayout.FloatField("回旋镖穿越伤害宽度", BooDamageWidth);
            }

            if (EffectType == eEffectType.Bounce)
            {
                Speed = EditorGUILayout.FloatField("速度", Speed);
                BounceTimes = EditorGUILayout.IntField("最大弹跳次数", BounceTimes);
                BounceDistance = EditorGUILayout.FloatField("最大弹跳距离", BounceDistance);
            }

            if (EffectType == eEffectType.Thunder)
            {
                Speed = EditorGUILayout.FloatField("速度", Speed);
                TraceDummyPoint = (eAvatarType)EditorGUILayout.EnumPopup("链接目标骨骼点", TraceDummyPoint);

                EditorGUILayout.BeginHorizontal();
                ThunderPrefab = EditorGUILayout.TextField("链条特效预设路径", ThunderPrefab);
                if (GUILayout.Button(".", GUILayout.Width(20)))
                {
                    string strPath = "Resources/";
                    string strVal = EditorUtility.OpenFilePanel("打开特效预设", Application.dataPath + "/Resources", "prefab");
                    int iPos = strVal.LastIndexOf(strPath);
                    int toPos = strVal.LastIndexOf(".");
                    if (iPos != -1 && toPos != -1)
                        ThunderPrefab = strVal.Substring(iPos + strPath.Length, toPos - iPos - strPath.Length);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            if (EffectType == eEffectType.Laser)
            {
                LaserMaxLength = EditorGUILayout.FloatField("激光束最大长度", LaserMaxLength);
                LaserStartWidth = EditorGUILayout.FloatField("激光束开始宽度", LaserStartWidth);
                LaserEndWidth = EditorGUILayout.FloatField("激光束结束宽度", LaserEndWidth);
                LaserCalcDamageTime = EditorGUILayout.FloatField("激光束计算伤害时间间隔", LaserCalcDamageTime);
                EditorGUILayout.BeginHorizontal();
                LaserHeadEffect = EditorGUILayout.TextField("激光头特效", LaserHeadEffect);
                if (GUILayout.Button(".", GUILayout.Width(20)))
                {
                    string strPath = "Resources/";
                    string strVal = EditorUtility.OpenFilePanel("打开特效预设", Application.dataPath + "/Resources", "prefab");
                    int iPos = strVal.LastIndexOf(strPath);
                    int toPos = strVal.LastIndexOf(".");
                    if (iPos != -1 && toPos != -1)
                        LaserHeadEffect = strVal.Substring(iPos + strPath.Length, toPos - iPos - strPath.Length);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                LaserTailEffect = EditorGUILayout.TextField("激光尾特效", LaserTailEffect);
                if (GUILayout.Button(".", GUILayout.Width(20)))
                {
                    string strPath = "Resources/";
                    string strVal = EditorUtility.OpenFilePanel("打开特效预设", Application.dataPath + "/Resources", "prefab");
                    int iPos = strVal.LastIndexOf(strPath);
                    int toPos = strVal.LastIndexOf(".");
                    if (iPos != -1 && toPos != -1)
                        LaserTailEffect = strVal.Substring(iPos + strPath.Length, toPos - iPos - strPath.Length);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
        }
#endif
    }
}
