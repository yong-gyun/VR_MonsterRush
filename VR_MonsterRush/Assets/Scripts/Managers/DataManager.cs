using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, MobStat> wolfStat { get; private set; } = new Dictionary<int, MobStat>();
    public Dictionary<int, MobStat> crabStat { get; private set; } = new Dictionary<int, MobStat>();
    public Dictionary<int, MobStat> infernoStat { get; private set; } = new Dictionary<int, MobStat>();
    public List<int> ScoreData { get; private set; } = new List<int>();

    public void Init()
    {
        wolfStat = LoadJson<MosStatData, int, MobStat>("WolfStat").MakeDic();
        crabStat = LoadJson<MosStatData, int, MobStat>("CrabStat").MakeDic();
        infernoStat = LoadJson<MosStatData, int, MobStat>("InfernoDragonStat").MakeDic();
    
        if(PlayerPrefs.HasKey("Rank1"))
        {
            for(int i = 1; i <= 5; i++)
            {
                ScoreData.Add(PlayerPrefs.GetInt($"Rank{i}"));
            }
        }
    }

    Loader LoadJson<Loader, TKey, TValue>(string path) where Loader : ILoader<TKey, TValue>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}