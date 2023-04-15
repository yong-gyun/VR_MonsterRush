using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        if (go == null)
            return null;

        T component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }

    public static void AddUIEvent(this GameObject go, Action<PointerEventData> evtData, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evtHandler = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evtHandler.AddUIEvent(evtData);
                break;
        }
    }
}
