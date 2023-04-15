using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Title : UI_WorldSpace
{
    enum Buttons
    {
        StartButton,
        OptionButton
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StartButton).onClick.AddListener(OnClickStartButton);
        GetButton((int)Buttons.OptionButton).onClick.AddListener(OnClickSettingButton);
    }

    void OnClickStartButton()
    {
        Managers.Scene.Load(Define.Scene.Game);
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);
    }

    void OnClickSettingButton()
    {
        Application.Quit();
    }
}
