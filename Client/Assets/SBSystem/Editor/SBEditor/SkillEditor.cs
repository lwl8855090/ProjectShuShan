using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SB
{
    public class SkillEditor : BaseEditor
    {
        public string _skillPath;


        public List<MetaSkill> _skills = new List<MetaSkill>();


        [MenuItem("UnityTool/SkillEditor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SkillEditor)).position = new UnityEngine.Rect(300, 300, 1024, 720);
        }
            
        public override void onInitTreeView()
        {
            _skillPath = Application.dataPath + @"/SBSystem/Resources/Data/Script/Skill";
		    TreeNode skillNode = new TreeNode ();
		    skillNode.Data = new NodeData(NodeData.eType.Root,_skillPath);
		    skillNode.Text = "Skill";
            _treeView.AddNode(skillNode);
            LoadSkill(_skillPath, skillNode);
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
                        CreateSkillStage(file.FullName, skillNode, name);
                        node.AddNode(skillNode);
                    }
                    AssetDatabase.Refresh();
                };
                window.Show(false);
            }
            else if (opType == eOpType.eOpType_SaveMeta)
            {
                MetaSkill skill = node.ExtraData as MetaSkill;
                if (data.Type == NodeData.eType.Entity && skill != null && node.Parent != null)
                {
                    Utility.Serilize(skill, data.Path);
                }
                AssetDatabase.Refresh();
            }
            else if (opType == eOpType.eOpType_DeleteMeta)
            {
                MetaSkill skill = node.ExtraData as MetaSkill;
                if (data.Type == NodeData.eType.Entity && skill != null && node.Parent != null)
                {
                    FileInfo file = new FileInfo(data.Path);
                    file.Delete();
                    _skills.Remove(skill);
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
        void LoadSkill(string folder, TreeNode node)
        {
            DirectoryInfo dir = new DirectoryInfo(folder);
            FileInfo[] files = dir.GetFiles("*.xml");
            foreach (FileInfo file in files)
            {
                TreeNode skillNode = new TreeNode();
                skillNode.Data = new NodeData(NodeData.eType.Entity, file.FullName);
                skillNode.Text = file.Name;

                LoadSkillStage(file.FullName, skillNode);
                node.AddNode(skillNode);
            }
            DirectoryInfo[] dirInfos = dir.GetDirectories();
            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                TreeNode skillNode = new TreeNode();
                skillNode.Data = new NodeData(NodeData.eType.Group, dirInfo.FullName);
                skillNode.Text = dirInfo.Name;
                node.AddNode(skillNode);
                LoadSkill(dirInfo.FullName, skillNode);
            }
        }

        void LoadSkillStage(string path, TreeNode node)
        {
            MetaSkill skill = Utility.DeSerilize(typeof(MetaSkill),path) as MetaSkill;
            if (skill == null)
            {
                return;
            }
            _skills.Add(skill);
            node.ExtraData = skill;

            TreeNode singNode = new TreeNode();
            singNode.Data = new NodeData(NodeData.eType.Stage, path);
            singNode.Text = "SingStage";
            singNode.ExtraData = skill.SingStage;
            node.AddNode(singNode);

            TreeNode channelNode = new TreeNode();
            channelNode.Data = new NodeData(NodeData.eType.Stage, path);
            channelNode.Text = "ChannelStage";
            channelNode.ExtraData = skill.ChannelStage;
            node.AddNode(channelNode);

            TreeNode castNode = new TreeNode();
            castNode.Data = new NodeData(NodeData.eType.Stage, path);
            castNode.Text = "CastStage";
            castNode.ExtraData = skill.CastStage;
            node.AddNode(castNode);

            TreeNode endNode = new TreeNode();
            endNode.Data = new NodeData(NodeData.eType.Stage, path);
            endNode.Text = "EndStage";
            endNode.ExtraData = skill.EndStage;
            node.AddNode(endNode);


            TreeNode pandingNode = new TreeNode();
            pandingNode.Data = new NodeData(NodeData.eType.Stage, path);
            pandingNode.Text = "PandingStage";
            pandingNode.ExtraData = skill.PandingStage;
            node.AddNode(pandingNode);

        }

        void CreateSkillStage(string path, TreeNode node, string name)
        {
            MetaSkill skill = new MetaSkill();
            skill.SkillName = name;
            _skills.Add(skill);
            node.ExtraData = skill;

            TreeNode singNode = new TreeNode();
            singNode.Data = new NodeData(NodeData.eType.Stage, path);
            singNode.Text = "SingStage";
            singNode.ExtraData = skill.SingStage;
            node.AddNode(singNode);

            TreeNode channelNode = new TreeNode();
            channelNode.Data = new NodeData(NodeData.eType.Stage, path);
            channelNode.Text = "ChannelStage";
            channelNode.ExtraData = skill.ChannelStage;
            node.AddNode(channelNode);

            TreeNode castNode = new TreeNode();
            castNode.Data = new NodeData(NodeData.eType.Stage, path);
            castNode.Text = "CastStage";
            castNode.ExtraData = skill.CastStage;
            node.AddNode(castNode);

            TreeNode endNode = new TreeNode();
            endNode.Data = new NodeData(NodeData.eType.Stage, path);
            endNode.Text = "EndStage";
            endNode.ExtraData = skill.EndStage;
            node.AddNode(endNode);

            Utility.Serilize(skill, path);
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
            MetaSkill skill = null;
            bool single = false;
            if (data.Type == NodeData.eType.Entity)
            {
                skill = node.ExtraData as MetaSkill;
            }
            else if (data.Type == NodeData.eType.Stage)
            {
                single = true;
                skill = new MetaSkill();
                if (node.Text == "SingStage")
                {
                    skill.SingStage = node.ExtraData as MetaStage;
                }
                else if (node.Text == "ChannelStage")
                {
                    skill.ChannelStage = node.ExtraData as MetaStage;
                }
                else if (node.Text == "CastStage")
                {
                    skill.CastStage = node.ExtraData as MetaStage;
                }
                else if (node.Text == "EndStage")
                {
                    skill.EndStage = node.ExtraData as MetaStage;
                }
                else if (node.Text == "PandingStage")
                {
                    skill.PandingStage = node.ExtraData as MetaStage;
                }
            }
            if (skill == null)
            {
                return;
            }
            ActionSkill skillEntity = new ActionSkill();
            SkillMgr.Instance.AddSkillEntity(skillEntity);
            Debug.Log("SkillEntity" + Time.time.ToString());
            skillEntity.SkillData = skill;
            skillEntity.Attacker = (ulong)Selection.activeGameObject.GetInstanceID();
            if (single)
            {
                skillEntity.SingTime = 0.0f;
                skillEntity.ChannelTime = 0.0f;
                skillEntity.CastTime = 3.0f;
            }
            else
            {
                skillEntity.SingTime = 0.0f;
                skillEntity.ChannelTime = 0.0f;
                skillEntity.CastTime = 0.0f;
            }
            Vector3 specialPos = Selection.activeGameObject.transform.position + Selection.activeGameObject.transform.forward * 4;
            specialPos.y += 2;
            skillEntity.SpecailPos = specialPos;

            //skillEntity.ProcessInterface = ScriptMgr.Instance.GetGlobalTable("ISkill_default");

            skillEntity.Start();
        }

    }
}
