using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawning : MonoBehaviour
{
    Transform[] spawnPoint = new Transform[6];
    GameObject _wolf;
    GameObject _inferno;
    GameObject _crab;
    int _currentWave = 1;
    [SerializeField] List<MobBase> mobs = new List<MobBase>();
    IEnumerator waveCorutine;

    enum Wave
    {
        First,
        Second,
        Third,
        Boss,
    }

    private void Start()
    {
        Init();
        mobs = Managers.Game.mobs;
    }

    public void Init()
    {
        for(int i = 0; i < spawnPoint.Length; i++)
        {
            spawnPoint[i] = transform.Find($"SpawnPoint_{i + 1}");
        }

        _wolf = Managers.Resource.Load<GameObject>("Prefabs/Character/Wolf");
        _inferno = Managers.Resource.Load<GameObject>("Prefabs/Character/Inferno");
        _crab = Managers.Resource.Load<GameObject>("Prefabs/Character/Crab");


        waveCorutine = FirstWave();
        StartCoroutine(waveCorutine);
    }

    MobBase GetMob(Define.MobType type, int idx = 0)
    {
        MobBase mob = null;

        switch (type)
        {
            case Define.MobType.Wolf:
                 mob = Managers.Pool.Pop(_wolf, spawnPoint[idx].position).GetComponent<MobBase>();
                break;
            case Define.MobType.InfernoDragon:
                mob = Managers.Pool.Pop(_inferno, spawnPoint[idx].position).GetComponent<MobBase>();
                break;
            case Define.MobType.Crab:
                mob = Managers.Pool.Pop(_crab, spawnPoint[idx].position).GetComponent<MobBase>();
                break;
        }

        Managers.Game.mobs.Add(mob);
        mob.Init(_currentWave);
        mob._hpBar.OnUpdateUI(mob.MaxHP);
        return mob;
    }

    public void StopWave()
    {;
        StopCoroutine(waveCorutine);
        gameObject.SetActive(false);
    }

    IEnumerator FirstWave()
    {
        Managers.UI.ShowWorldSpaceUI<UI_Wave>().Init("Wave 1");
        Managers.Sound.PlayBGM(Define.BGM.Game);
        yield return new WaitForSeconds(3f);

        float delay = 7f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Crab, 3);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Crab, 2);
        GetMob(Define.MobType.Crab, 3);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Crab, 2);
        GetMob(Define.MobType.Crab, 3);
        yield return wait;

        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Wolf, 5);
        GetMob(Define.MobType.Wolf, 0);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Crab, 4);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.Crab, 4);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 2);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;
        
        waveCorutine = SecondWave();
        yield return new WaitForSeconds(3f);
        
        Managers.Game.Tower.Heal(Managers.Game.Tower.MaxHP / 4f);
        StartCoroutine(waveCorutine);
    }

    IEnumerator SecondWave()
    {
        _currentWave++;
        Managers.UI.ShowWorldSpaceUI<UI_Wave>().Init("Wave 2");
        yield return new WaitForSeconds(3f);

        float delay = 4.5f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;          
                                    
        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Crab, 3);
        yield return wait;          
                                    
        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3); 
        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Crab, 5);
        yield return wait;          
                                    
        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.Wolf, 5);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;          
                                    
                                    
        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Crab, 5);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;          
                                    
        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Crab, 5);
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.InfernoDragon, 2);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Crab, 3);
        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Crab, 5);
        yield return wait;
        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Crab, 5);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.Wolf, 5);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;


        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Crab, 5);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Crab, 5);
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.InfernoDragon, 2);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Crab, 3);
        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Crab, 5);
        yield return wait;
        Managers.Game.Tower.Heal(Managers.Game.Tower.MaxHP / 2f);
        waveCorutine = ThirdWave(); 
        yield return new WaitForSeconds(3f);
        StartCoroutine(waveCorutine);
    }

    
    IEnumerator ThirdWave()
    {
        waveCorutine = LastWave();
        Managers.UI.ShowWorldSpaceUI<UI_Wave>().Init("Wave 3");
        yield return new WaitForSeconds(3f);

        float delay = 6;
        WaitForSeconds wait = new WaitForSeconds(delay);

        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Crab, 3);
        GetMob(Define.MobType.Crab, 0);
        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Crab, 2);
        GetMob(Define.MobType.Crab, 3);
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.InfernoDragon, 2);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        yield return wait;

        _currentWave++;
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.InfernoDragon, 2);
        GetMob(Define.MobType.InfernoDragon, 0);
        yield return wait;

        GetMob(Define.MobType.Crab, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Crab, 4);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.Crab, 4);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 5);
        GetMob(Define.MobType.Crab, 0);
        yield return wait;

        waveCorutine = LastWave();
        Managers.Game.Tower.Heal(Managers.Game.Tower.MaxHP);
        yield return new WaitForSeconds(4f);
        StartCoroutine(waveCorutine);
    }

    IEnumerator LastWave()
    {
        Managers.UI.ShowWorldSpaceUI<UI_Wave>().Init("Last wave");
        yield return new WaitForSeconds(3f);

        float delay = 4;
        WaitForSeconds wait = new WaitForSeconds(delay);

        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Crab, 0);
        GetMob(Define.MobType.InfernoDragon, 1);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Wolf, 2);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        yield return wait;

        _currentWave++;
        GetMob(Define.MobType.InfernoDragon, 4);
        GetMob(Define.MobType.InfernoDragon, 5);
        GetMob(Define.MobType.Wolf, 0);
        yield return wait;
        

        GetMob(Define.MobType.InfernoDragon, 2);
        GetMob(Define.MobType.InfernoDragon, 0);
        GetMob(Define.MobType.InfernoDragon, 4);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 5);
        yield return wait;

        _currentWave++;
        GameObject boss = Managers.Resource.Instantiate("Character/Boss", spawnPoint[0].position, Quaternion.identity);
        
        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 0);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 5);
        yield return wait;

        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Crab, 0);
        GetMob(Define.MobType.InfernoDragon, 1);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Wolf, 4);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 5);
        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Wolf, 5);

        yield return new WaitForSeconds(4);
        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 5);
        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Crab, 2);
        GetMob(Define.MobType.Crab, 3);


        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 0);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 3);
        GetMob(Define.MobType.InfernoDragon, 5);
        yield return wait;

        GetMob(Define.MobType.Crab, 4);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Crab, 0);
        GetMob(Define.MobType.InfernoDragon, 1);
        yield return wait;

        GetMob(Define.MobType.Wolf, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Wolf, 4);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        yield return wait;

        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 5);
        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Wolf, 2);
        GetMob(Define.MobType.Wolf, 3);
        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Wolf, 5);

        yield return new WaitForSeconds(4);
        GetMob(Define.MobType.InfernoDragon, 1);
        GetMob(Define.MobType.InfernoDragon, 5);
        GetMob(Define.MobType.Wolf, 4);
        GetMob(Define.MobType.Crab, 2);
        GetMob(Define.MobType.Crab, 3);

        yield return new WaitForSeconds(10f);

        if(boss.gameObject != null)
            Managers.Resource.Destroy(boss.gameObject);
        
        Managers.Sound.PlayBGM(Define.BGM.Clear);
        Managers.UI.ShowWorldSpaceUI<UI_Wave>().Init("집을 지켜냈다 !");

        yield return new WaitForSeconds(3f);
        Managers.UI.ShowWorldSpaceUI<UI_Wave>().Init("Clear");

        yield return new WaitForSeconds(2f);
        Managers.Game.Over();
        Managers.Game.SetRank();
    }
}
