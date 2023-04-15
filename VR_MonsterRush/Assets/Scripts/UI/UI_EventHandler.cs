using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    Action<PointerEventData> onClickAction = null;

    public void OnPointerClick(PointerEventData evtData)
    {
        if (onClickAction != null)
            onClickAction.Invoke(evtData);
    }

    public void AddUIEvent(Action<PointerEventData> evtData)
    {
        onClickAction -= evtData;
        onClickAction += evtData;
    }
}
