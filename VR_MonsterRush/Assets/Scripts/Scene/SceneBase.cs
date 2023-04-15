using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        Clear();

        GameObject go = GameObject.Find("@UIHelpers");
        OVRInputModule inputModule = null;

        if(go == null)
        {
            go = Managers.Resource.Instantiate("UI/UIHelpers");
            go.name = "@UIHelper";
        }

        inputModule = go.GetComponentInChildren<OVRInputModule>();
        inputModule.rayTransform = GameObject.Find("CenterEyeAnchor").transform;
        inputModule.m_Cursor = Managers.Input.LaserPointer;
    }

    public virtual void Clear() 
    { 
        Managers.Game.Clear();
        Managers.UI.Clear(); 
    }
}
