using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class EditName : EditorWindow
{
    public Action<string> CallbackFunc;
    private string _name = "New Name";
    
    private bool _destroy = false;
    public void OnEnable()
    {

    }
    public void OnGUI()
    {
        GUILayout.Label("输入名字");
        _name = GUILayout.TextField(_name, 25);
        if (GUILayout.Button("确定"))
        {
            OnOK();
            this.Close();
        }
    }

    void OnOK()
    {
        if (_name == "" || CallbackFunc == null)
        {
            this.Close();
            return;
        }
        CallbackFunc(_name);
    }
}
