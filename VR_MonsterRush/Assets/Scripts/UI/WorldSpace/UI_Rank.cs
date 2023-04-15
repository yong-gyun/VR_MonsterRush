using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Rank : UI_WorldSpace
{
    enum Texts
    {
        FirstPlaceText,
        SecondthPlaceText,
        ThirdthPlaceText,
        FourthPlaceText,
        FivethPlaceText

    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        
        for(int i = 0; i < Managers.Game.ScoreList.Count; i++)
        {
            if (i == 5)
                return;
            Texts text = (Texts) i;
            GetText((int)text).text = $"{Managers.Game.ScoreList[i]}";
        }
    }
}
