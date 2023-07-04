using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MobBase
{
    protected override void Awake()
    {
        base.Awake();

        _stat = Managers.Data.wolfStat;
        _type = Define.MobType.Wolf;
        _myGold = 4;
        _myScore = 25;
    }
}