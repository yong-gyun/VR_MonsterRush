using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Interface : UI_WorldSpace
{
    enum Texts
    {
        ScoreText,
        GoldText,
        TowerHpText
    }

    enum Sliders
    {
        TowerHpSlider
    }

    public override void Init()
    {
        base.Init(); 
        Managers.UI.worldSpaces.Add(this);
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
    }

    private void Update()
    {
        GetText((int)Texts.ScoreText).text = $": {Managers.Game.CurrentScore}";
        GetText((int)Texts.GoldText).text = $"{Managers.Game.CurrentGold}";
        GetText((int)Texts.TowerHpText).text = $"{Managers.Game.Tower.HP} / {Managers.Game.Tower.MaxHP}";
        Get<Slider>((int)Sliders.TowerHpSlider).value = Managers.Game.Tower.HP / Managers.Game.Tower.MaxHP;
    }
}
