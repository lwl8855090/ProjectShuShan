using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;


public class TreeNode
{
    public TreeNode Parent = null;
    public List<TreeNode> Nodes = new List<TreeNode>();
    public int Level = 0;
    public System.Object Data;
    public System.Object ExtraData;
    public bool FoldOut = false;
    public string Text;

    public void AddNode(TreeNode node)
    {
        node.Level = Level + 1;
        node.Parent = this;
        Nodes.Add(node);
    }
}

public class TreeView
{
    public delegate void SelectChanged();
    public delegate void FoldOutChanged(TreeNode tn);

    public List<TreeNode> Nodes = new List<TreeNode>();

    public TreeNode SelectedNode = null;

    public SelectChanged SelectChangedEvent;

    public float Width = 10.0f;
    public int _tmpIndex = 0;
    private Vector2 scrollPos = Vector2.zero;

    public void AddNode(TreeNode node)
    {
        node.Level = 0;
        node.Parent = null;
        Nodes.Add(node);
    }

    public void RemoveNode(TreeNode node)
    {
        node.Parent.Nodes.Remove(node);
        if (SelectedNode == node)
        {
            SelectedNode = null;
            SelectChangedEvent();
        }
    }

    public void OnDraw()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        GUILayout.Label("", GUILayout.Height(20 * (_tmpIndex + 1)));
        _tmpIndex = 0;
        int saveIndent = EditorGUI.indentLevel;
        foreach (TreeNode node in Nodes)
        {
            DrawNode(node);
        }
        EditorGUI.indentLevel = saveIndent;
        EditorGUILayout.EndScrollView();
    }

    void DrawNode(TreeNode node)
    {
        if (Event.current.button == 0 && Event.current.type == EventType.mouseDown)
        {
            if (Event.current.mousePosition.y + scrollPos.y > 20 * _tmpIndex && Event.current.mousePosition.y + scrollPos.y < (20 * _tmpIndex + 16))
            {
                if (SelectedNode != node)
                {
                    SelectedNode = node;
                    SelectChangedEvent();
                }
            }
        }
        int saveIndent = EditorGUI.indentLevel;
        node.FoldOut = EditorGUI.Foldout(new Rect(0, 20 * _tmpIndex, Width, 16), node.FoldOut, node.Text);
        if (SelectedNode == node)
        {
            Drawing.DrawRect(new Rect(0, 20 * _tmpIndex, Width, 16), new Color(0f, 0.25f, 1f, 0.3f));
        }
        EditorGUI.indentLevel++;
        _tmpIndex++;
        if (node.FoldOut)
        {
            foreach (TreeNode node2 in node.Nodes)
            {
                DrawNode(node2);
            }
        }
        EditorGUI.indentLevel--;
        EditorGUI.indentLevel = saveIndent;
    }
}