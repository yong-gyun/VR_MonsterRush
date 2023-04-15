using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WorldSpace : UI_Base
{
    protected virtual void Awake()
    {
        Init();
    }
    
    protected virtual void Start()
    {
        gameObject.AddComponent<OVRRaycaster>().pointer = Managers.Input.LaserPointer.gameObject;
    }

    public override void Init()
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(gameObject);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
    }
}
