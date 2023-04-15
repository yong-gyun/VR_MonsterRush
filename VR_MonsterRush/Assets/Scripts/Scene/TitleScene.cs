using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    public override void Init()
    {
        base.Init();

        Managers.Sound.PlayBGM(Define.BGM.Title);
    }
}
