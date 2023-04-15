using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoController : MobBase
{
   private Transform _firePos;

    protected override void Awake()
    {
        base.Awake();
        _stat = Managers.Data.infernoStat;
        _type = Define.MobType.InfernoDragon;
        _firePos = transform.Find("FirePos");
        _myGold = 10;
        _myScore = 50;
    }

    public override void OnAttack()
    {
        Debug.Log("Attack");
        GameObject beginFireball = Managers.Resource.Instantiate("Effect/BegineFireball", _firePos.position + Vector3.forward * 0.5f, Quaternion.Euler(0, 90, 90));
        Managers.Resource.Destroy(beginFireball, 1f);
        Managers.Resource.Instantiate("Item/Fireball", _firePos.position, Quaternion.identity).GetOrAddComponent<Fireball>().Init(_damage);
    }
}

