using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MobBase
{
    protected override void Awake()
    {
        base.Awake();
        _stat = Managers.Data.crabStat;
        _type = Define.MobType.Crab;
        _myGold = 6;
        _myScore = 20;
    }

    public override void OnDamaged(float damage, Define.Hit hit = Define.Hit.Bullet)
    {
        damage = (7f / 10f) * damage;
        base.OnDamaged(damage);
    } 
}