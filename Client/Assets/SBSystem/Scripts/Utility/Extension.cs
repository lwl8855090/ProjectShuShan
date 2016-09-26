using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SB
{
    public static class Extension
    {
        public static byte ToByte(this string str)
        {
            byte rel = 0;
            try
            {
                rel = byte.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }
        public static sbyte ToSByte(this string str)
        {
            sbyte rel = 0;
            try
            {
                rel = sbyte.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }
        public static float ToFloat(this string str)
        {
            float rel = 0f;
            try
            {
                rel = float.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0f;
            }
            return rel;
        }

        public static double ToDouble(this string str)
        {
            double rel = 0f;
            try
            {
                rel = double.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0.0;
            }
            return rel;
        }
        public static Int16 ToInt16(this string str)
        {
            Int16 rel = 0;
            try
            {
                rel = Int16.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }
        public static UInt16 ToUInt16(this string str)
        {
            UInt16 rel = 0;
            try
            {
                rel = UInt16.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }

        public static Int32 ToInt32(this string str)
        {
            Int32 rel = 0;
            try
            {
                rel = Int32.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }


        public static UInt32 ToUInt32(this string str)
        {
            UInt32 rel = 0;
            try
            {
                rel = UInt32.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }

        public static Int64 ToInt64(this string str)
        {
            Int64 rel = 0;
            try
            {
                rel = Int64.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }


        public static UInt64 ToUInt64(this string str)
        {
            UInt64 rel = 0;
            try
            {
                rel = UInt64.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }

        public static long ToLong(this string str)
        {
            long rel = 0;
            try
            {
                rel = long.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }


        public static ulong ToULong(this string str)
        {
            ulong rel = 0;
            try
            {
                rel = ulong.Parse(str);
            }
            catch (Exception exp)
            {
                rel = 0;
            }
            return rel;
        }

        public static bool ToBool(this string str)
        {
            bool rel = false;
            int iVal = 0;
            try
            {
                rel = bool.Parse(str);
            }
            catch (Exception exp)
            {
                try
                {
                    iVal = int.Parse(str);
                }
                catch (Exception exp2)
                {
                    iVal = 0;
                }
                rel = (iVal > 0);
            }
            return rel;
        }

        public static System.Object ToEnum(this string str, Type tp)
        {
            System.Object rel = null;
            try
            {
                rel = Enum.Parse(tp, str);
            }
            catch (Exception exp)
            { 
                
            }
            return rel;
        }

        public static Vector2 ToVector2(this string str)
        {
            Vector2 rel = Vector2.zero;
            int iStart = str.IndexOf("(");
            int iEnd = str.IndexOf(")");
            if (iStart == -1 || iEnd == -1)
            {
                return rel;
            }
            string subStr = str.Substring(iStart + 1, iEnd - (iStart + 1));
            string[] rels = subStr.Split(',');
            if (rels.Length != 2)
            {
                return rel;
            }
            rel.x = float.Parse(rels[0]);
            rel.y = float.Parse(rels[1]);
            return rel;
        }
        public static Vector3 ToVector3(this string str)
        {
            Vector3 rel = Vector3.zero;
            int iStart = str.IndexOf("(");
            int iEnd = str.IndexOf(")");
            if (iStart == -1 || iEnd == -1)
            {
                return rel;
            }
            string subStr = str.Substring(iStart + 1, iEnd - (iStart + 1));
            string[] rels = subStr.Split(',');
            if (rels.Length != 3)
            {
                return rel;
            }
            rel.x = float.Parse(rels[0]);
            rel.y = float.Parse(rels[1]);
            rel.z = float.Parse(rels[2]);
            return rel;
        }
        public static Color ToColor(this string str)
        {
            Color rel = Color.white;
            int iStart = str.IndexOf("(");
            int iEnd = str.IndexOf(")");
            if (iStart == -1 || iEnd == -1)
            {
                return rel;
            }
            string subStr = str.Substring(iStart + 1, iEnd - (iStart + 1));
            string[] rels = subStr.Split(',');
            if (rels.Length != 4)
            {
                return rel;
            }
            rel.r = float.Parse(rels[0]);
            rel.g = float.Parse(rels[1]);
            rel.b = float.Parse(rels[2]);
            rel.a = float.Parse(rels[3]);
            return rel;
        }
        public static Quaternion ToQuaternion(this string str)
        {
            Quaternion rel = new Quaternion();
            int iStart = str.IndexOf("(");
            int iEnd = str.IndexOf(")");
            if (iStart == -1 || iEnd == -1)
            {
                return rel;
            }
            string subStr = str.Substring(iStart + 1, iEnd - (iStart + 1));
            string[] rels = subStr.Split(',');
            if (rels.Length != 4)
            {
                return rel;
            }
            rel.x = float.Parse(rels[0]);
            rel.y = float.Parse(rels[1]);
            rel.z = float.Parse(rels[2]);
            rel.w = float.Parse(rels[3]);
            return rel;
        }


        public static Rect ToRect(this string str)
        {
            Rect rel = new Rect();
            int iStart = str.IndexOf("(");
            int iEnd = str.IndexOf(")");
            if (iStart == -1 || iEnd == -1)
            {
                return rel;
            }
            string subStr = str.Substring(iStart + 1, iEnd - (iStart + 1));
            string[] rels = subStr.Split(',');
            if (rels.Length != 4)
            {
                return rel;
            }
            string[] item = rels[0].Split(':');
            rel.x = item[1].ToFloat();
            item = rels[1].Split(':');
            rel.y = item[1].ToFloat();
            item = rels[2].Split(':');
            rel.width = item[1].ToFloat();
            item = rels[3].Split(':');
            rel.height = item[1].ToFloat();
            return rel;
        }


        public static Transform FindInChildren(this GameObject go, string name)
        {
            foreach (Transform x in go.GetComponentsInChildren<Transform>())
            {
                if (x.gameObject.name == name)
                {
                    return x;
                }
            }
            return null;
        }
    }
}
