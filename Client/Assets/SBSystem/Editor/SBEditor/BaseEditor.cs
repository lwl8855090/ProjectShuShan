using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace SB
{
    public class NodeData
    {
        public enum eType
        {
            Root,
            Group,
            Entity,
            Stage,
        }

        public eType Type;
        public string Path;

        public NodeData()
        {
        }
        public NodeData(eType type, string path)
        {
            Type = type;
            Path = path;
        }
    }

    public enum eOpType
    {
        eOpType_AddGroup,
        eOpType_AddMeta,

        eOpType_SaveMeta,
        eOpType_DeleteMeta,

        eOpType_CopyStage,
        eOpType_PasteStage,
    }

    public class BaseEditor : EditorWindow
    {
        public Rect cursorChangeRect; //技能列表和网格之间的那一段区域
        public Rect frameSliderRect; //帧操作区域，帧的移动只在该区域才响应

        public Rect windowRect = new Rect(200, 200, 300, 600); //技能属性网格区域
        public Rect colorRect = new Rect(300, 300, 200, 300); //颜色管理器面板区域


        public Rect skillListRct = new Rect(0, 0, 100, 100); //技能列表区域，初始化值
        public Rect gridRect = new Rect(100, 0, 100, 100);//技能原子网格编辑区，初始化值

        public float gridOffset = 50.0f;
        public float pixPerRuller = 25.0f;
        public float circleRadius = 5.0f;
        public float gridHeight = 20.0f;
        public float gridHeightOffset = 37.0f;


        public float horScrollVal = 0.0f;
        public float verScrollVal = 0.0f;


        public bool showMenu = false;
        public bool showListMenu = false;
        public bool resize = false;
        public bool dragFrame = false;
        public bool dragKey = false;


        public int selectFrame = 0;
        public Vector2 selectedKey = new Vector2(-1.0f, -1.0f);
        public Vector2 rClickInfo = new Vector2(-1.0f, -1.0f);

        public TreeView _treeView = null;

        public MetaStage _curMetaStage = null;

        public MetaAtom selectedAtom = null;
        public MetaAtom cacheAtom = null;


        public void OnEnable()
        {
            cursorChangeRect = new Rect(185, 0, 2, position.height);
            frameSliderRect = new Rect(150, 0, 2, 37);
            InitTreeView();
        }


        public void InitTreeView()
        {
            _treeView = new TreeView();
            _treeView.SelectChangedEvent += SelectChanged;
            onInitTreeView();
        }
        public virtual void onInitTreeView()
        { 
            
        }


        public virtual void SelectChanged()
        {
            TreeNode node = _treeView.SelectedNode;
            if (node == null)
            {
                return;
            }
            NodeData data = node.Data as NodeData;
            if (data == null)
            {
                return;
            }
            if (data.Type != NodeData.eType.Stage)
            {
                return;
            }
            _curMetaStage = node.ExtraData as MetaStage;
            onSelectChanged();
        }


        public virtual void onSelectChanged()
        {
            
        }
        void refreshRect()
        {
            cursorChangeRect.height = position.height;
            skillListRct.width = cursorChangeRect.x;
            skillListRct.height = position.height;
            gridRect.x = cursorChangeRect.x + cursorChangeRect.width;
            frameSliderRect.x = gridRect.x;
            gridRect.y = 0;
            gridRect.width = position.width - gridRect.x;
            frameSliderRect.width = gridRect.width;
            gridRect.height = position.height;
        }

        public void OnGUI()
        {
            refreshRect();

            GUILayout.BeginArea(skillListRct);
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUI.backgroundColor = new Color(1f, 1f, 1f, 0.5f);
            if (GUILayout.Button("S", EditorStyles.toolbarButton, GUILayout.Width(20)))
            {
                SaveAll();
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("▲", EditorStyles.toolbarButton, GUILayout.Width(20)))
            {
                playStage();
            }
            if (GUILayout.Button("ㄖ", EditorStyles.toolbarButton, GUILayout.Width(20)))
            {
                UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
            }
            if (GUILayout.Button("■", EditorStyles.toolbarButton, GUILayout.Width(20)))
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            GUILayout.EndHorizontal();

            OnGUISkillList();
            GUILayout.EndArea();
            Handles.color = Color.gray;
            Handles.DrawLine(new Vector3(cursorChangeRect.x, cursorChangeRect.y, 0), new Vector3(cursorChangeRect.x, cursorChangeRect.height, 0));
            EditorGUIUtility.AddCursorRect(cursorChangeRect, MouseCursor.ResizeHorizontal);

            //绘制编辑区
            GUILayout.BeginArea(gridRect);
            //绘制标尺
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            float offset = gridOffset - horScrollVal;
            //int iStart = horScrollVal - gridOffset;
            int iCnt = (int)((gridRect.width - (gridOffset - horScrollVal)) / pixPerRuller);
            int iStartFrm = 0;
            if (horScrollVal - gridOffset > 0)
            {
                iStartFrm = (int)((horScrollVal - gridOffset) / pixPerRuller);
            }

            for (int i = iStartFrm; i < iCnt; ++i)
            {
                if ((gridOffset - horScrollVal) + i * pixPerRuller < 0.0f)
                {
                    continue;
                }
                if (i % 5 == 0)
                {
                    Handles.Label(new Vector3((gridOffset - horScrollVal) + i * pixPerRuller + 2, 0, 0), string.Format("{0,3:F}", i * 0.01f));
                    Drawing.DrawLine(new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, 5), new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, 18), selectFrame == i ? Color.red : Color.grey, 0.5f, true);
                    Drawing.DrawLine(new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, 7), new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, gridRect.height - 17), selectFrame == i ? Color.red : Color.gray, 0.5f, true);
                }
                else
                {
                    Drawing.DrawLine(new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, 15), new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, 18), selectFrame == i ? Color.red : new Color(0.3f, 0.3f, 0.3f), 0.5f, true);
                    Drawing.DrawLine(new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, gridHeightOffset), new Vector2((gridOffset - horScrollVal) + i * pixPerRuller, gridRect.height - 17), selectFrame == i ? Color.red : new Color(0.3f, 0.3f, 0.3f), 0.5f, true);
                }
            }
            iCnt = (int)((gridRect.height - gridHeightOffset) / gridHeight);
            for (int i = 0; i < iCnt; ++i)
            {
                Handles.DrawLine(new Vector3(0, gridHeightOffset + i * gridHeight, 0), new Vector3(gridRect.width - 17, gridHeightOffset + i * gridHeight, 0));
            }

            if (_curMetaStage != null)
            {
                foreach (MetaFrame frm in _curMetaStage.FrameList)
                {
                    if (frm.Index < iStartFrm)
                    {
                        continue;
                    }
                    for (int j = 0; j < frm.MetaAtomList.Count; ++j)
                    {
                        if (selectedAtom == frm.MetaAtomList[j])
                        {
                            Handles.color = Color.red;
                        }
                        else
                        {
                            Handles.color = Color.black;
                        }
                        MetaAtom currSkillAtom = frm.MetaAtomList[j];
                        if (currSkillAtom != null) //空白帧不绘制
                        {
                            float fHeight = ((gridHeightOffset + currSkillAtom.Index * gridHeight) + (gridHeightOffset + (currSkillAtom.Index + 1) * gridHeight)) / 2;
                            //优先绘制外框
                            Handles.DrawSolidDisc(new Vector3((gridOffset - horScrollVal) + frm.Index * pixPerRuller, fHeight, 0), Vector3.forward, 5);
                        }
                    }
                }
            }

            horScrollVal = GUI.HorizontalScrollbar(new Rect(0, gridRect.height - 17, gridRect.width - 17, 14), horScrollVal, 100, 0, 27000);
            verScrollVal = GUI.VerticalScrollbar(new Rect(gridRect.width - 17, 37, 14, gridRect.height - 37 - 17), verScrollVal, 100, 0, 27000);
            GUILayout.EndArea();


            if (selectedAtom != null)
            {
                NameAttribute nameAttribute = selectedAtom.GetType().GetCustomAttributes(typeof(NameAttribute), false).FirstOrDefault() as NameAttribute;
                BeginWindows();
                windowRect = GUI.Window(1, windowRect, DoProperty, nameAttribute != null ? nameAttribute.name : "属性面板");
                EndWindows();
            }

            ShowGridMenu();
            ShowListMenu();
            EditorEventListener(); 
            Repaint();
        }


        void OnGUISkillList()
        {
            //空Label用于撑开ScrollView的滚动条
            _treeView.Width = skillListRct.width;
            _treeView.OnDraw();
        }

        void DoProperty(int id)
        {
            if (selectedAtom != null)
            {
                MethodInfo memInfo = selectedAtom.GetType().GetMethod("OnGUI");
                if (memInfo != null)
                {
                    memInfo.Invoke(selectedAtom, new object[] { });
                }
                else
                {
                    Utility.ShowObject(selectedAtom);
                }
            }
            GUI.DragWindow();
        }


        void ShowGridMenu()
        {
            if (showMenu)
            {
                showMenu = false;
                if (selectedAtom == null)
                {
                    GenericMenu.MenuFunction2 Selected = delegate(System.Object obj)
                    {
                        if (obj as String == "paste")
                        {
                            AddSkillAtom(cacheAtom);
                        }
                        else
                        {
                            Type tp = obj as Type;
                            if (tp == null)
                            {
                                return;
                            }
                            MetaAtom atom = Activator.CreateInstance(tp) as MetaAtom;
                            atom.Owner = _curMetaStage != null ? _curMetaStage.Owner : null;
                            if (atom is PlayEffctAtom)
                            {
                                PlayEffctAtom ea = atom as PlayEffctAtom;
                                ea.FlagIndex = atom.Owner != null ? atom.Owner.GetFlagIndex() : 0;
                            }
                            else if (atom is PlaySoundAtom)
                            {
                                PlaySoundAtom sa = atom as PlaySoundAtom;
                                sa.FlagIndex = atom.Owner != null ? atom.Owner.GetFlagIndex() : 0;
                            }
                            AddSkillAtom(atom);
                        }
                    };

                    GenericMenu menu = new GenericMenu();


                    var scriptInfos = Utility.GetAllScriptsOfTypeCategorized(typeof(MetaAtom));


                    foreach (Utility.ScriptInfo script in scriptInfos)
                    {
                        if (string.IsNullOrEmpty(script.category))
                            menu.AddItem(new GUIContent(script.name), false, Selected, script.type);
                    }


                    foreach (Utility.ScriptInfo script in scriptInfos)
                    {
                        if (!string.IsNullOrEmpty(script.category))
                            menu.AddItem(new GUIContent(script.category + "/" + script.name), false, Selected, script.type);
                    }
                    menu.AddSeparator("/");
                    menu.AddItem(new GUIContent("粘贴"), false, Selected, "paste");
                    menu.ShowAsContext();
                }
                else
                {
                    GenericMenu.MenuFunction2 Selected = delegate(object selectedType)
                    {
                        if (selectedType.ToString() == "delete")
                        {
                            RemoveSkillAtom(selectedAtom);
                        }
                        else if (selectedType.ToString() == "copy")
                        {
                            CopySkillAtom(selectedAtom);
                        }
                    };
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("删除"), false, Selected, "delete");
                    menu.AddItem(new GUIContent("复制"), false, Selected, "copy");
                    menu.ShowAsContext();
                }
                Event.current.Use();
            }
        }
        void ShowListMenu()
        {
            if (showListMenu)
            {
                showListMenu = false;
                TreeNode node = _treeView.SelectedNode;
                if (node == null)
                {
                    return;
                }
                NodeData data = node.Data as NodeData;
                if (data == null)
                {
                    return;
                }
                GenericMenu.MenuFunction2 Selected = ListMenuEvent;

                GenericMenu menu = new GenericMenu();
                if (data.Type != NodeData.eType.Entity && data.Type != NodeData.eType.Stage)
                {
                    menu.AddItem(new GUIContent("创建分组"), false, Selected, eOpType.eOpType_AddGroup);
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("添加"), false, Selected, eOpType.eOpType_AddMeta);
                    menu.AddSeparator("");
                }
                if (data.Type == NodeData.eType.Entity)
                {
                    menu.AddItem(new GUIContent("保存"), false, Selected, eOpType.eOpType_SaveMeta);
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("删除"), false, Selected, eOpType.eOpType_DeleteMeta);
                    menu.AddSeparator("");
                }
                if (data.Type == NodeData.eType.Stage)
                {
                    menu.AddItem(new GUIContent("复制Stage"), false, Selected, eOpType.eOpType_CopyStage);
                    menu.AddItem(new GUIContent("粘贴Stage"), false, Selected, eOpType.eOpType_PasteStage);
                }
                menu.AddSeparator("");
                menu.ShowAsContext();
                Event.current.Use();
            }
        }


        public virtual void ListMenuEvent(System.Object obj)
        {

        }

        void EditorEventListener()
        {
            if (Event.current.type == EventType.mouseDown)
            {
                if (cursorChangeRect.Contains(Event.current.mousePosition))
                    resize = true;
                else
                {
                    if (frameSliderRect.Contains(Event.current.mousePosition))
                        selectFrame = (int)Math.Round((Event.current.mousePosition.x + horScrollVal - gridRect.x - gridOffset) / pixPerRuller);
                    if (gridRect.Contains(Event.current.mousePosition))
                    {
                        int localSelectFrame = (int)Math.Round((Event.current.mousePosition.x + horScrollVal - gridRect.x - gridOffset) / pixPerRuller);
                        int index = (int)((Event.current.mousePosition.y - gridHeightOffset) / gridHeight);
                        float fValue = (Event.current.mousePosition.y - gridHeightOffset) % gridHeight;
                        if (fValue > (gridHeight / 2 - circleRadius))
                        {
                            selectedAtom = GetMetaAtom(localSelectFrame, index);
                        }
                        else
                        {
                            selectedAtom = null;
                        }
                        if (selectedAtom != null)
                        {
                            selectedKey = new Vector2(localSelectFrame, index);
                        }
                        else
                        {
                            selectedKey = new Vector2(-1.0f, -1.0f);
                        }
                    }
                }
                if (Event.current.button == 1)
                {
                    if (Event.current.mousePosition.x > gridRect.x && !frameSliderRect.Contains(Event.current.mousePosition))
                        showMenu = true;
                    else
                    {
                        showListMenu = true;
                    }
                    int frameX = (int)Math.Round((Event.current.mousePosition.x + horScrollVal - gridRect.x - gridOffset) / pixPerRuller);
                    int rowY = (int)((Event.current.mousePosition.y - gridHeightOffset) / gridHeight);
                    rClickInfo = new Vector2(frameX, rowY);
                }

                dragFrame = false;
                dragKey = false;
            }
            if (Event.current.type == EventType.mouseDrag)
            {
                if (Event.current.button == 2)
                {
                    //horScrollVal -= (Event.current.mousePosition - middlePos).x;
                    //verScrollVal += (Event.current.mousePosition - middlePos).y;
                }
                else
                {
                    if (frameSliderRect.Contains(Event.current.mousePosition))
                    {
                        if (GetFrameInfo(selectFrame) != null)
                        {
                            dragFrame = true;
                        }
                    }
                    if (gridRect.Contains(Event.current.mousePosition))
                    {
                        if (selectedKey.x > -1)
                        {
                            dragKey = true;
                        }
                    }
                }
            }
            if (resize)
            {
                cursorChangeRect.Set(Event.current.mousePosition.x, 0, 2, position.height);
            }
            if (Event.current.type == EventType.MouseUp)
            {
                if (resize)
                    resize = false;
                else
                {

                }
                if (dragFrame)
                {
                    int dstFrame = (int)Math.Round((Event.current.mousePosition.x + horScrollVal - gridRect.x - gridOffset) / pixPerRuller);
                    if (GetFrameInfo(dstFrame) == null) //不能再根据第一个元素来判断是否为空帧了！
                    {
                        ChangeFrame(selectFrame, dstFrame);
                        selectFrame = dstFrame;
                    }
                }
                dragFrame = false;
                if (dragKey)
                {
                    int dstFrame = (int)Math.Round((Event.current.mousePosition.x + horScrollVal - gridRect.x - gridOffset) / pixPerRuller);
                    dstFrame = dstFrame < 0 ? 0 : dstFrame;
                    int dstRow = (int)((Event.current.mousePosition.y - gridHeightOffset) / gridHeight);
                    Vector2 dstKey = new Vector2(dstFrame, dstRow);
                    if (GetMetaAtom(dstFrame, dstRow) == null)
                    {
                        ChangeKey(selectedKey, dstKey);
                        selectedKey = dstKey;
                    }

                }
                dragKey = false;
            }
        }


        public MetaAtom GetMetaAtom(int frame, int index)
        {
            if (_curMetaStage == null)
            {
                return null;
            }
            foreach (MetaFrame frm in _curMetaStage.FrameList)
            {
                if (frm.Index != frame)
                {
                    continue;
                }
                foreach (MetaAtom atm in frm.MetaAtomList)
                {
                    if (atm.Index == index)
                    {
                        return atm;
                    }
                }
            }
            return null;
        }


        public MetaFrame GetFrameInfo(int index)
        {
            if (_curMetaStage == null) return null;
            foreach (MetaFrame frm in _curMetaStage.FrameList)
            {
                if (frm.Index != index)
                {
                    continue;
                }
                return frm;
            }
            return null;
        }


        public void ChangeFrame(int srcFrame, int dstFrame)
        {
            if (_curMetaStage == null)
            {
                return;
            }
            foreach (MetaFrame frm in _curMetaStage.FrameList)
            {
                if (frm.Index != srcFrame)
                {
                    continue;
                }
                frm.Index = dstFrame;
            }
        }


        public void ChangeKey(Vector2 srcKey, Vector2 dstKey)
        {
            if (_curMetaStage == null)
            {
                return;
            }
            MetaFrame srcFrameInfo = GetFrameInfo((int)srcKey.x);
            MetaFrame dstFrameInfo = GetFrameInfo((int)dstKey.x);
            if (srcFrameInfo == null) return;
            if (dstFrameInfo == null)
            { // 如果目标帧不存在
                dstFrameInfo = new MetaFrame();
                dstFrameInfo.Index = (int)dstKey.x;
                _curMetaStage.FrameList.Add(dstFrameInfo);
            }
            selectedAtom.Index = (int)dstKey.y;
            srcFrameInfo.MetaAtomList.Remove(selectedAtom);
            dstFrameInfo.MetaAtomList.Add(selectedAtom);

            if (srcFrameInfo.MetaAtomList.Count == 0)
            {
                _curMetaStage.FrameList.Remove(srcFrameInfo);
            }
        }


        public void AddSkillAtom(MetaAtom metaAtom)
        {
            if (_curMetaStage == null)
            {
                return;
            }
            if (metaAtom == null)
            {
                return;
            }
            if (rClickInfo.y >= 0)
            {
                metaAtom.Index = (int)rClickInfo.y;
            }
            MetaFrame frame = null;
            foreach (MetaFrame frm in _curMetaStage.FrameList)
            {
                if (frm.Index != (int)rClickInfo.x)
                {
                    continue;
                }
                frame = frm;
            }
            if (frame == null)
            {
                frame = new MetaFrame();
                frame.Index = (int)rClickInfo.x;
                _curMetaStage.FrameList.Add(frame);

                _curMetaStage.FrameList = _curMetaStage.FrameList.OrderBy(frm => frm.Index).ToList();
            }
            frame.MetaAtomList.Add(metaAtom);
            selectedAtom = metaAtom;
        }

        public void CopySkillAtom(MetaAtom metaAtom)
	    {
            cacheAtom = metaAtom.Clone();
	    }


        public void RemoveSkillAtom(MetaAtom metaAtom)
        {
            if (_curMetaStage == null)
            {
                return;
            }
            MetaFrame frame = null;
            foreach (MetaFrame frm in _curMetaStage.FrameList)
            {
                if (!frm.MetaAtomList.Contains(metaAtom))
                {
                    continue;
                }
                frame = frm;
            }
            if (frame == null)
            {
                return;
            }
            frame.MetaAtomList.Remove(metaAtom);
            if (frame.MetaAtomList.Count == 0)
            {
                _curMetaStage.FrameList.Remove(frame);
            }
            selectedAtom = null;
        }



        public virtual void playStage()
        {
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.isPaused = false;
                UnityEditor.EditorApplication.isPlaying = true;
                return;
            }
        }


        public virtual void SaveAll()
        {
            foreach (TreeNode var in _treeView.Nodes)
            {
                RecursiveNodeSave(var);
            }
            AssetDatabase.Refresh();
        }

        public void RecursiveNodeSave(TreeNode node)
        {
            NodeData data = node.Data as NodeData;
            if (data == null)
            {
                return;
            }
            if (data.Type == NodeData.eType.Root || data.Type == NodeData.eType.Group)
            {
                foreach (TreeNode var in node.Nodes)
                {
                    RecursiveNodeSave(var);
                }
                return;
            }
            if (data.Type != NodeData.eType.Entity)
            {
                return;
            }
            if (node.ExtraData != null)
            {
                Utility.Serilize(node.ExtraData, data.Path);
            }
        }
    }
}
