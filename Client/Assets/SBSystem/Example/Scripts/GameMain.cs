//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using SB;
public class GameMain : MonoBehaviour
{
    [HideInInspector]
    public static GameMain Instance = null;

	void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
	}

	// Use this for initialization
	void Start()
    {
        SkillMgr.Instance.Initilize();
        BuffMgr.Instance.Initilize();
	}
	
	// Update is called once per frame
    void Update()
    {
        SkillMgr.Instance.Update();
        BuffMgr.Instance.Update();


//		Debug.Log(Time.timeScale.ToString());
	}
	
	void OnDestroy() {
	}

}

