using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeDic();
}

[Serializable]
public class MobStat
{
    public int level;
    public float hp;
    public float damage;
    public float speed;
    public float attackRange;
    public int minGold;
    public int maxGold;
    public int score;
}

[Serializable]
public class MosStatData : ILoader<int, MobStat>
{
    public List<MobStat> stat = new List<MobStat>();

    public Dictionary<int, MobStat> MakeDic()
    {
        Dictionary<int, MobStat> myDic = new Dictionary<int, MobStat>();

        foreach(MobStat stat in stat)
        {
            myDic.Add(stat.level, stat);
        }

        return myDic;
    }
}
