using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Crosshair : UI_WorldSpace
{
    enum Images
    {
        Crosshair
    }
    ARAVRInput.Controller _controller;

    public void SetDirection(ARAVRInput.Controller controller)
    {
        _controller = controller;
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
    }

    private void Update()
    {
        ARAVRInput.DrawCrosshair(gameObject.transform, true, _controller);
    }
}
