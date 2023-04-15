using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_Wave : UI_WorldSpace
{
    enum Texts
    {
        WaveText
    }

    const float duration = 2f;

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public void Init(string message)
    {
        GetText((int)Texts.WaveText).gameObject.SetActive(true);
        GetText((int)Texts.WaveText).text = message;
        StartCoroutine(Active());
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(1f);
        GetText((int)Texts.WaveText).DOFade(0, duration);
        yield return new WaitForSeconds(duration);
        GetText((int)Texts.WaveText).DOFade(1, 0);
        GetText((int)Texts.WaveText).gameObject.SetActive(false);
    }
}