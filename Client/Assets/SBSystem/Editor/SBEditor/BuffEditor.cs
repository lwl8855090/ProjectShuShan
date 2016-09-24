using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SB
{
    public class BuffEditor : BaseEditor
    {
        public string _buffPath;


        public List<MetaBuff> _buffs = new List<MetaBuff>();


        [MenuItem("UnityTool/BuffEditor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(BuffEditor)).position = new UnityEngine.Rect(300, 300, 1024, 720);
        }

        public override void onInitTreeView()
        {
            _buffPath = Application.dataPath + @"/SBSystem/Resources/Data/Script/Buff";
            TreeNode buffNode = new TreeNode();
            buffNode.Data = new NodeData(NodeData.eType.Root, _buffPath);
            buffNode.Text = "Buff";
            _treeView.AddNode(buffNode);
            LoadBuff(_buffPath, buffNode);
        }



        public override void ListMenuEvent(System.Object obj)
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
            eOpType opType = (eOpType)obj;
            if (opType == null)
            {
                return;
            }
            if (opType == eOpType.eOpType_AddGroup)
            {
                EditName window = GetWindow(typeof(EditName)) as EditName;
                window.CallbackFunc = (name) =>
                {
                    DirectoryInfo dir = new DirectoryInfo(data.Path);
                    if (Directory.Exists(Path.Combine(data.Path, name)))
                    {
                        return;
                    }
                    DirectoryInfo sub = dir.CreateSubdirectory(name);
                    if (sub != null)
                    {
                        TreeNode skillNode = new TreeNode();
                        skillNode.Data = new NodeData(NodeData.eType.Group, sub.FullName);
                        skillNode.Text = sub.Name;
                        node.AddNode(skillNode);
                    }
                    AssetDatabase.Refresh();
                };
                window.Show(false);
            }
            else if (opType == eOpType.eOpType_AddMeta)
            {
                EditName window = GetWindow(typeof(EditName)) as EditName;
                window.CallbackFunc = (name) =>
                {
                    DirectoryInfo dir = new DirectoryInfo(data.Path);
                    FileInfo file = new FileInfo(dir.FullName + "/" + name + ".xml");
                    if (!file.Exists)
                    {
                        FileStream fs = file.Create();
                        fs.Close();
                        TreeNode skillNode = new TreeNode();
                        skillNode.Data = new NodeData(NodeData.eType.Entity, file.FullName);
                        skillNode.Text = file.Name;
                        CreateBuffStage(file.FullName, skillNode, name);
                        node.AddNode(skillNode);
                    }
                    AssetDatabase.Refresh();
                };
                window.Show(false);
            }
            else if (opType == eOpType.eOpType_SaveMeta)
            {
                MetaBuff buff = node.ExtraData as MetaBuff;
                if (data.Type == NodeData.eType.Entity && buff != null && node.Parent != null)
                {
                    Utility.Serilize(buff, data.Path);
                }
                AssetDatabase.Refresh();
            }
            else if (opType == eOpType.eOpType_DeleteMeta)
            {
                MetaBuff buff = node.ExtraData as MetaBuff;
                if (data.Type == NodeData.eType.Entity && buff != null && node.Parent != null)
                {
                    FileInfo file = new FileInfo(data.Path);
                    file.Delete();
                    _buffs.Remove(buff);
                    _treeView.RemoveNode(node);
                }
                AssetDatabase.Refresh();
            }
            else if (opType == eOpType.eOpType_CopyStage)
            {

            }
            else if (opType == eOpType.eOpType_PasteStage)
            {

            }
        }
        void LoadBuff(string folder, TreeNode node)
        {
            DirectoryInfo dir = new DirectoryInfo(folder);
            FileInfo[] files = dir.GetFiles("*.xml");
            foreach (FileInfo file in files)
            {
                TreeNode skillNode = new TreeNode();
                skillNode.Data = new NodeData(NodeData.eType.Entity, file.FullName);
                skillNode.Text = file.Name;

                LoadBuffStage(file.FullName, skillNode);
                node.AddNode(skillNode);
            }
            DirectoryInfo[] dirInfos = dir.GetDirectories();
            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                TreeNode skillNode = new TreeNode();
                skillNode.Data = new NodeData(NodeData.eType.Group, dirInfo.FullName);
                skillNode.Text = dirInfo.Name;
                node.AddNode(skillNode);
                LoadBuff(dirInfo.FullName, skillNode);
            }
        }

        void LoadBuffStage(string path, TreeNode node)
        {
            MetaBuff buff = Utility.DeSerilize(typeof(MetaBuff), path) as MetaBuff;
            if (buff == null)
            {
                return;
            }
            _buffs.Add(buff);
            node.ExtraData = buff;

            TreeNode castNode = new TreeNode();
            castNode.Data = new NodeData(NodeData.eType.Stage, path);
            castNode.Text = "CastStage";
            castNode.ExtraData = buff.CastStage;
            node.AddNode(castNode);

            TreeNode fireNode = new TreeNode();
            fireNode.Data = new NodeData(NodeData.eType.Stage, path);
            fireNode.Text = "FireStage";
            fireNode.ExtraData = buff.FireStage;
            node.AddNode(fireNode);

            TreeNode endNode = new TreeNode();
            endNode.Data = new NodeData(NodeData.eType.Stage, path);
            endNode.Text = "EndStage";
            endNode.ExtraData = buff.EndStage;
            node.AddNode(endNode);

        }

        void CreateBuffStage(string path, TreeNode node, string name)
        {
            MetaBuff buff = new MetaBuff();
            buff.BuffName = name;
            _buffs.Add(buff);
            node.ExtraData = buff;

            TreeNode castNode = new TreeNode();
            castNode.Data = new NodeData(NodeData.eType.Stage, path);
            castNode.Text = "CastStage";
            castNode.ExtraData = buff.CastStage;
            node.AddNode(castNode);

            TreeNode fireNode = new TreeNode();
            fireNode.Data = new NodeData(NodeData.eType.Stage, path);
            fireNode.Text = "FireStage";
            fireNode.ExtraData = buff.FireStage;
            node.AddNode(fireNode);

            TreeNode endNode = new TreeNode();
            endNode.Data = new NodeData(NodeData.eType.Stage, path);
            endNode.Text = "EndStage";
            endNode.ExtraData = buff.EndStage;
            node.AddNode(endNode);

            Utility.Serilize(buff, path);
            //skill.Save();
        }

        public override void playStage()
        {
            base.playStage();
            if (Selection.activeGameObject == null)
            {
                return;
            }
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
            MetaBuff buff = null;
            bool single = false;
            if (data.Type == NodeData.eType.Entity)
            {
                buff = node.ExtraData as MetaBuff;
            }
            else if (data.Type == NodeData.eType.Stage)
            {
                single = true;
                buff = new MetaBuff();
                if (node.Text == "CastStage")
                {
                    buff.CastStage = node.ExtraData as MetaStage;
                }
                else if (node.Text == "FireStage")
                {
                    buff.FireStage = node.ExtraData as MetaStage;
                }
                else if (node.Text == "EndStage")
                {
                    buff.EndStage = node.ExtraData as MetaStage;
                }
            }
            if (buff == null)
            {
                return;
            }

            ActionBuff buffEntity = new ActionBuff();
            BuffMgr.Instance.AddBuffEntity(buffEntity);
            buffEntity.BuffData = buff;
            //ActorTemplate act = Selection.activeGameObject.GetComponent<ActorTemplate>();
            //foreach (Actor actor in ActorMgr.Instance.Actors.Values)
            //{
            //    ActorTemplate at = actor.Template;
            //    if (at == null)
            //    {
            //        continue;
            //    }
            //    if (act == at)
            //    {
            //        continue;
            //    }
            //    buffEntity.Targeters.Add(at.ID);
            //}
            buffEntity.Attacker = (ulong)Selection.activeGameObject.GetInstanceID();
            if (single)
            {
                buffEntity.FireTime = 1.0f;
                buffEntity.LifeTime = 3.0f;
            }
            else
            {
                buffEntity.FireTime = 1.0f;
                buffEntity.LifeTime = 10.0f;
            }

            //buffEntity.ProcessInterface = ScriptMgr.Instance.GetGlobalTable("IBuff_default");

            buffEntity.Start();
        }
    }
}
