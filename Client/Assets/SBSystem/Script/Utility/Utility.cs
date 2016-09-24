using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SB
{
    public static class Utility
    {

        public static T AddIfMissing<T>(GameObject target) where T : Component
        {
            T component;
            return AddIfMissing(target, out component);
        }
        public static T AddIfMissing<T>(GameObject target, out T component) where T : Component
        {
            component = target.GetComponent<T>();

            if (component == null)
            {
                component = target.AddComponent<T>();
                return component;
            }

            return component;
        }

        public static void Serilize(System.Object obj, string path)
        {
            if (obj == null)
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            XmlElement data = doc.CreateElement(obj.GetType().Name);
            Serilize(doc, data, obj);
            doc.AppendChild(data);
            doc.Save(path);
        }

        
        public static void Serilize(XmlDocument doc, XmlElement ele, System.Object obj)
        {
            if (obj == null)
            {
                return;
            }
            if (obj.GetType() == typeof(byte)||
                obj.GetType() == typeof(sbyte) ||
                obj.GetType() == typeof(float) ||
                obj.GetType() == typeof(double) ||
                obj.GetType() == typeof(Int16) ||
                obj.GetType() == typeof(UInt16) ||
                obj.GetType() == typeof(Int32) ||
                obj.GetType() == typeof(UInt32) ||
                obj.GetType() == typeof(Int64) ||
                obj.GetType() == typeof(UInt64)||
                obj.GetType() == typeof(long)||
                obj.GetType() == typeof(ulong)||
                obj.GetType() == typeof(bool) ||
                obj.GetType() == typeof(String)||
                typeof(Enum).IsAssignableFrom(obj.GetType())||
                obj.GetType() == typeof(Vector2)||
                obj.GetType() == typeof(Vector3)||
                obj.GetType() == typeof(Color)||
                obj.GetType() == typeof(Quaternion)||
                obj.GetType() == typeof(Rect) )
            {
                ele.InnerText = obj.ToString();
                return;
            }
            if (typeof(IDictionary).IsAssignableFrom(obj.GetType()))
            {
                IDictionaryEnumerator enumer = (obj as IDictionary).GetEnumerator();
                while (enumer.MoveNext())
                {
                    XmlElement child = doc.CreateElement("item");
                    Serilize(doc, child, enumer.Current);
                    ele.AppendChild(child);

                    XmlElement key = doc.CreateElement("key");
                    Serilize(doc, key, enumer.Key);
                    child.AppendChild(key);

                    XmlElement val = doc.CreateElement("val");
                    Serilize(doc, val, enumer.Value);
                    child.AppendChild(val);
                }
                return;
            }
            if (typeof(IEnumerable).IsAssignableFrom(obj.GetType()))
            {
                IEnumerator enumer = (obj as IEnumerable).GetEnumerator();
                while (enumer.MoveNext())
                {
                    XmlElement child = doc.CreateElement("item");
                    Serilize(doc, child, enumer.Current);
                    ele.AppendChild(child);
                }
                return;
            }
            ele.SetAttribute("type", obj.GetType().FullName);
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttributes(typeof(NonSerializedAttribute), true).FirstOrDefault() as NonSerializedAttribute != null)
                    continue;
                XmlElement child = doc.CreateElement(field.Name);
                Serilize(doc, child, field.GetValue(obj));
                ele.AppendChild(child);
            }
        }

        public static System.Object DeSerilize(Type type, string path)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlElement ele = doc.SelectSingleNode(type.Name) as XmlElement;
            System.Object obj = DeSerilize(ele, type);
            return obj;
        }

        public static System.Object DeSerilize(XmlElement ele, Type type)
        {
            if (type == null)
            {
                return null;
            }

            if (type == typeof(byte))
            {
                return ele.InnerText.ToByte();
            }
            else if(type == typeof(sbyte))
            {
                return ele.InnerText.ToSByte();
            }
            else if (type == typeof(float))
            {
                return ele.InnerText.ToFloat();
            }
            else if (type == typeof(double))
            {
                return ele.InnerText.ToDouble();
            }
            else if (type == typeof(Int16))
            {
                return ele.InnerText.ToInt16();
            }
            else if (type == typeof(UInt16))
            {
                return ele.InnerText.ToUInt16();
            }
            else if (type == typeof(Int32))
            {
                return ele.InnerText.ToInt32();
            }
            else if (type == typeof(UInt32))
            {
                return ele.InnerText.ToUInt32();
            }
            else if (type == typeof(Int64))
            {
                return ele.InnerText.ToInt64();
            }
            else if (type == typeof(UInt64))
            {
                return ele.InnerText.ToUInt64();
            }
            else if (type == typeof(long))
            {
                return ele.InnerText.ToLong();
            }
            else if (type == typeof(ulong))
            {
                return ele.InnerText.ToULong();
            }
            else if (type == typeof(bool))
            {
                return ele.InnerText.ToBool();
            }
            else if (type == typeof(String))
            {
                return ele.InnerText;
            }
            else if (typeof(Enum).IsAssignableFrom(type))
            {
                return ele.InnerText.ToEnum(type);
            }
            else if (type == typeof(Vector2))
            {
                return ele.InnerText.ToVector2();
            }
            else if (type == typeof(Vector3))
            {
                return ele.InnerText.ToVector3();
            }
            else if (type == typeof(Color))
            {
                return ele.InnerText.ToColor();
            }
            else if (type == typeof(Quaternion))
            {
                return ele.InnerText.ToQuaternion();
            }
            else if (type == typeof(Rect))
            {
                return ele.InnerText.ToRect();
            }
            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                var result = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                XmlNodeList items = ele.ChildNodes;
                foreach (XmlNode item in items)
                {
                    XmlElement keyNode = item.SelectSingleNode("key") as XmlElement;
                    XmlElement valueNode = item.SelectSingleNode("val") as XmlElement;
                    var key = DeSerilize(keyNode as XmlElement, Type.GetType(keyNode.Attributes["type"].Value));
                    var val = DeSerilize(valueNode as XmlElement, Type.GetType(valueNode.Attributes["type"].Value));
                    if (key == null || val == null)
                    {
                        continue;
                    }
                    type.GetMethod("Add").Invoke(result, new object[] { key, val });
                }
                return result;
            }
            if (typeof(IList).IsAssignableFrom(type))
            {
                var result = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                XmlNodeList items = ele.ChildNodes;
                foreach (XmlNode item in items)
                {
                    XmlElement valNode = item as XmlElement;
                    var v = DeSerilize(item as XmlElement, Type.GetType(valNode.Attributes["type"].Value));
                    if(v == null)
                    {
                        continue;
                    }
                    type.GetMethod("Add").Invoke(result, new object[] { v });
                }

                return result;
            }
            System.Object rel = null;
            if (ele.Attributes["type"] != null)
            {
                Type tp = Type.GetType(ele.Attributes["type"].Value);
                if (tp != null)
                    rel = Activator.CreateInstance(tp);
            }
            if (rel == null)
            {
                return null;
            }
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttributes(typeof(NonSerializedAttribute), true).FirstOrDefault() as NonSerializedAttribute != null || ele.SelectSingleNode(field.Name) == null)
                    continue;
                XmlElement child = ele.SelectSingleNode(field.Name) as XmlElement;
                System.Object value = DeSerilize(child, field.FieldType);
                if (value == null)
                {
                    continue;
                }
                field.SetValue(rel, value);
            }
            return rel;
        }


        
#if UNITY_EDITOR
        public class ScriptInfo
        {

            public Type type;
            public string name;
            public string category;

            public ScriptInfo(Type type, string name, string category)
            {
                this.type = type;
                this.name = name;
                this.category = category;
            }
        }

        public static List<ScriptInfo> GetAllScriptsOfTypeCategorized(Type baseType)
        {
            List<MonoScript> allScripts = new List<MonoScript>();
            List<ScriptInfo> allRequestedScripts = new List<ScriptInfo>();

            foreach (string path in AssetDatabase.GetAllAssetPaths())
            {

                if (path.EndsWith(".js") || path.EndsWith(".cs") || path.EndsWith(".boo"))
                    allScripts.Add(AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript)) as MonoScript);
            }

            foreach (MonoScript monoScript in allScripts)
            {
                if (monoScript == null)
                    continue;

                Type subType = monoScript.GetClass();
                if (baseType.IsAssignableFrom(subType))
                {
                    if (subType == baseType)
                        continue;

                    if (subType.IsAbstract)
                        continue;
                    if (subType.GetCustomAttributes(typeof(HideInInspector), false).FirstOrDefault() != null)
                    {
                        continue;
                    }

                    string scriptName = subType.Name;
                    string scriptCategory = string.Empty;
                    var nameAttribute = subType.GetCustomAttributes(typeof(NameAttribute), false).FirstOrDefault() as NameAttribute;
                    if (nameAttribute != null)
                        scriptName = nameAttribute.name;

                    var categoryAttribute = subType.GetCustomAttributes(typeof(GroupAttribute), true).FirstOrDefault() as GroupAttribute;
                    if (categoryAttribute != null)
                        scriptCategory = categoryAttribute.group;

                    allRequestedScripts.Add(new ScriptInfo(subType, scriptName, scriptCategory));
                }
            }

            allRequestedScripts = allRequestedScripts.OrderBy(script => script.name).ToList();
            allRequestedScripts = allRequestedScripts.OrderBy(script => script.category).ToList();

            return allRequestedScripts;
        }
        public static void ShowObject(System.Object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                if (field.GetCustomAttributes(typeof(HideInInspector), true).FirstOrDefault() as HideInInspector != null)
                    continue;
                VariableDecsAttribute nameAttribute = field.GetCustomAttributes(typeof(VariableDecsAttribute), false).FirstOrDefault() as VariableDecsAttribute;
                field.SetValue(obj, GenericField(nameAttribute != null ? nameAttribute.name : field.Name, field.GetValue(obj), field.FieldType, field, field.GetCustomAttributes(typeof(FileSelectAttribute), false).FirstOrDefault() as FileSelectAttribute));
                GUI.backgroundColor = Color.white;
            }
        }

        public static object GenericField(string name, object value, System.Type type, MemberInfo member = null,FileSelectAttribute seleter = null)
        {
            if (type == typeof(byte))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(sbyte))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(float))
            {
                return EditorGUILayout.FloatField(name, (float)value);
            }
            else if (type == typeof(double))
            {
                return EditorGUILayout.DoubleField(name, (double)value);
            }
            else if (type == typeof(Int16))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(UInt16))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(Int32))
            {
                return EditorGUILayout.IntField(name, (int)value);
            }
            else if (type == typeof(UInt32))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(Int64))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(UInt64))
            {
                return EditorGUILayout.IntField(name, (int)value.ToString().ToInt32());
            }
            else if (type == typeof(long))
            {
                return EditorGUILayout.LongField(name, (long)value);
            }
            else if (type == typeof(ulong))
            {
                return EditorGUILayout.LongField(name, value.ToString().ToLong());
            }
            else if (type == typeof(bool))
            {
                return EditorGUILayout.Toggle(name, (bool)value);
            }
            else if (type == typeof(String))
            {
                if (seleter == null)
                {
                    return EditorGUILayout.TextField(name, (string)value);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    value = EditorGUILayout.TextField(name, (string)value);
                    if (GUILayout.Button("。", GUILayout.Width(20)))
                    {
                        string strPath = "Resources/";
                        string strVal = EditorUtility.OpenFilePanel("打开预设", seleter.startPath, seleter.suffix);
                        int iPos = strVal.LastIndexOf(strPath);
                        int toPos = strVal.LastIndexOf(".");
                        if (iPos != -1 && toPos != -1)
                            value = strVal.Substring(iPos + strPath.Length, toPos - iPos - strPath.Length);
                    }
                    EditorGUILayout.EndHorizontal();
                    return value;
                }
            }
            else if (typeof(Enum).IsAssignableFrom(type))
            {
                return EditorGUILayout.EnumPopup(name, (System.Enum)value);
            }
            else if (type == typeof(Vector2))
            {
                return EditorGUILayout.Vector2Field(name, (Vector2)value);
            }
            else if (type == typeof(Vector3))
            {
                return EditorGUILayout.Vector3Field(name, (Vector3)value);
            }
            else if (type == typeof(Color))
            {
                return EditorGUILayout.ColorField(name, (Color)value);
            }
            else if (type == typeof(Rect))
            {
                return EditorGUILayout.RectField(name, (Rect)value);
            }

            GUILayout.Label("Field '" + name + "' Type '" + type.Name + "' not yet supported for Auto Editor");
            return value;
        }
#endif
    }
}
