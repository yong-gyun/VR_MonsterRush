using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Over : UI_WorldSpace
{
    enum Buttons
    {
        ExitButton,
        RetryButton
    }

    enum Texts
    {
        ScoreText
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        Get<Text>((int)Texts.ScoreText).text = $"{Managers.Game.CurrentScore}";
        GetButton((int)Buttons.RetryButton).onClick.AddListener(OnClickedRetryButton);
        GetButton((int)Buttons.ExitButton).onClick.AddListener(OnClickedExitButton);
        Managers.Sound.PlayBGM(Define.BGM.Over);
    }

    void OnClickedRetryButton()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);
        Managers.Scene.Load(Define.Scene.Game);
    }

    void OnClickedExitButton()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundEffect.Click);
        Managers.Scene.Load(Define.Scene.Title);
    }
}
