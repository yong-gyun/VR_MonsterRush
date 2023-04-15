using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float MaxHP { get { return _maxHP; } }
    public float HP { get { return _hp; } }
    float _hp = 0;
    float _maxHP = 2000;
    bool isHit;
    GameObject[] blastLocations = new GameObject[6];

    private void Start()
    {
        for(int i = 0; i < blastLocations.Length; i++)
        {
            blastLocations[i] = Util.FindChild(gameObject, $"BlastLocation_{i + 1}", true);
        }

        _hp = _maxHP;
        tag = "Tower";
        isHit = false;
    }

    public void OnDamaged(float damage)
    {
        if (isHit)
            return;
       _hp -= damage;

        if(_hp <= 0 && isHit == false)
        {
            StartCoroutine(OnDie());
        }
    }

    public void Heal(float value)
    {
        _hp += value;
        Debug.Log($"Heal {value}");
        if (_hp > _maxHP)
            _hp = _maxHP;
    }

    IEnumerator OnDie()
    {
        if (isHit == true)
            yield break;

        _hp = 0;
        isHit = true;
        Managers.Game.Over(); 
        yield return new WaitForSeconds(1f);


        for (int j = 0; j < blastLocations.Length; j++)
        {
            GameObject go = Managers.Resource.Instantiate("Effect/ExplosionParticle", blastLocations[j].transform.position, Quaternion.identity);
            Managers.Sound.PlaySoundEffect(Define.SoundEffect.ExplosionTower);
            Managers.Sound.StopBGM();
            Managers.Resource.Destroy(go, 3f);
            yield return new WaitForSeconds(0.25f);
        }

        for (int i = 1; i < 3; i++)
        {
            GameObject smoke = Managers.Resource.Instantiate("Effect/SmokeParticle", blastLocations[2].transform.position, Quaternion.identity);
            smoke.transform.localScale = new Vector3(60, 60, 60);
        }

        yield return new WaitForSeconds(1f);

        Managers.Resource.Instantiate("UI/WroldSpace/UI_Over", new Vector3(-1, 22, 0.5f), Quaternion.identity);
    }

    IEnumerator Healing()
    {
        WaitForSeconds wait = new WaitForSeconds(8);

        while (true)
        {
            yield return wait;
            _hp += 20;

            if (_hp > _maxHP)
                _hp = _maxHP;
        }
    }
}
