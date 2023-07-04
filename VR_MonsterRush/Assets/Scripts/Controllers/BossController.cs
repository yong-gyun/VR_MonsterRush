using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MobBase
{
    public override Define.State State 
    {   
        get => base.State; 
        
        set
        {
            _state = value;

            switch (_state)
            {
                case Define.State.Idle:
                    _animator.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Move:
                    _animator.CrossFade("Move", 0.1f);
                    break;
                case Define.State.Attack:
                    _animator.CrossFade("Attack", 0.1f, -1, 0);
                    break;
                case Define.State.Die:
                    _animator.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    float _idleTime = 0;
    float _waitTime = 4f;
    Transform _firePos;

    protected override void Awake()
    {
        _animator = Util.GetOrAddComponent<Animator>(gameObject);
        _agent = Util.GetOrAddComponent<UnityEngine.AI.NavMeshAgent>(gameObject);
        _agent.avoidancePriority = 15;
        _agent.radius = 0.25f;
        _agent.angularSpeed = 0f;
        Managers.Game.mobs.Add(this);
        Init();
    }

    protected override void Update()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Move:
                UpdateMove();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
        }
    }
    public void Init()
    {
        _maxHP = 1000;
        _speed = 5;
        _attackRange = 25;
        _damage = 30;
        _firePos = transform.Find("FirePos");
        _hp = MaxHP;
        _state = Define.State.Move;
        _agent.speed = _speed;
        _agent.avoidancePriority = 15;
        _myScore = 5000;

        StartCoroutine(Healing());
    }

    private void UpdateIdle()
    {
        if (_idleTime <= _waitTime)
        {
            _idleTime += Time.deltaTime;
        }   
        else
        {
            State = Define.State.Move;
            _idleTime = 0;
        }
    }
    
    public override void OnAttack()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Healing()
    {
        while(true)
        {
            _hp += 8;

            if (_hp > MaxHP)
                _hp = MaxHP;

            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject beginFireball = Managers.Resource.Instantiate("Effect/BegineFireball", _firePos.position + Vector3.forward * 0.5f, Quaternion.Euler(0, 90, 90));
            Managers.Resource.Destroy(beginFireball, 1f);
            Managers.Resource.Instantiate("Item/Fireball", _firePos.position, Quaternion.identity).GetOrAddComponent<Fireball>().Init(_damage);
            Managers.Sound.PlaySoundEffect(Define.SoundEffect.Fireball);
            yield return new WaitForSeconds(0.2f); 
        }

        State = Define.State.Idle;
    }

    public override void OnDamaged(float damage, Define.Hit hit)
    {
        if (_state == Define.State.Die)
            return;

        _hp -= damage;
        Debug.Log($"{gameObject.name} {_hp}");

        _agent.SetDestination(transform.position);

        if (_hp <= 0)
        {
            State = Define.State.Die;

            Managers.Game.CurrentGold += _myGold;
            Managers.Game.CurrentScore += _myScore;
        }
    }

    public override void OnDie()
    {
        Debug.Log($"Die {name}");
        Managers.Game.mobs.Remove(this);
        Managers.Resource.Destroy(gameObject);
    }
}
