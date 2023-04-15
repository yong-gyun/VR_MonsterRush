using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    public override void Init()
    {
        base.Init();

        Managers.UI.Clear();

        Managers.Game.CurrentGold = 50;
        Managers.Game.CurrentScore = 0;
        Managers.Game.Player.transform.position = new Vector3(-1, 13, 2.5f);
        Managers.Game.Player.transform.rotation = Quaternion.Euler(-1, 180, 2.5f);

        Managers.UI.ShowWorldSpaceUI<UI_Shop>();
        Managers.UI.ShowWorldSpaceUI<UI_Interface>();
        Managers.Sound.PlayBGM(Define.BGM.Game);
    }
}
