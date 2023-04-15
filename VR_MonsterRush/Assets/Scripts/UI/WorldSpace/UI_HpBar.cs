using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpBar : UI_WorldSpace
{
    enum Sliders
    {
        HpSlider
    }

    public override void Init()
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(gameObject);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        Bind<Slider>(typeof(Sliders));
    }

    public void OnUpdateUI(float hp)
    {
        Get<Slider>((int)Sliders.HpSlider).value = hp;
    }
}
