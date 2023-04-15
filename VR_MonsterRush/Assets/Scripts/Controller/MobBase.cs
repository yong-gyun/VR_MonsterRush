using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBase : MonoBehaviour
{
    public Define.MobType Type { get { return _type; } }
    public float MaxHP { get { return _maxHP; } }
    public float MoveSpeed { get { return _speed; } }
    public float AttackRange { get { return _attackRange; } }
    public NavMeshAgent _agent { get; set; }
    public UI_HpBar _hpBar { get; set; }
    
    protected Animator _animator;
    protected Transform _target;
    protected Dictionary<int, MobStat> _stat;
    
    [SerializeField] protected Define.State _state;
    protected Define.MobType _type;
    protected float _maxHP;
    protected float _hp;
    protected float _damage;
    protected float _speed;
    protected float _attackRange;
    
    protected int _myScore;
    protected int _myGold;
    static int _avoidance = 30;


    public Transform Target
    {
        get
        {
            if (_target == null)
                _target = Managers.Game.Tower.transform;

            return _target;
        }
    }

    public virtual Define.State State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            switch (_state)
            {
                case Define.State.Move:
                    _animator.CrossFade("Move", 0.1f);
                    break;
                case Define.State.Attack:
                    _animator.CrossFade("Attack", 0.1f, -1, 0);
                    break;
                case Define.State.Hit:
                    _animator.CrossFade("Hit", 0.1f);
                    break;
                case Define.State.Stun:
                    Debug.Log("Stun");
                    _animator.CrossFade("Stun", 0.1f);
                    break;
                case Define.State.Die:
                    _animator.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    protected virtual void Awake()
    {
        _animator = Util.GetOrAddComponent<Animator>(gameObject);
        _agent = Util.GetOrAddComponent<NavMeshAgent>(gameObject);
        _agent.avoidancePriority = _avoidance++;
        _agent.radius = 0.35f;
        _agent.angularSpeed = 0f;
        _hpBar = Managers.UI.MakeWorldSpaceUI<UI_HpBar>(transform);
    }

    protected virtual void Update()
    {
        switch(State)
        {
            case Define.State.Move:
                UpdateMove();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
        }
    }

    public virtual void Init(int level)
    {
        _maxHP = _stat[level].hp;
        _speed = _stat[level].speed;
        _attackRange = _stat[level].attackRange;
        _damage = _stat[level].damage;
        _myGold = Random.Range(_stat[level].minGold, _stat[level].maxGold);
        _myScore = _stat[level].score;
        _hp = MaxHP;
        _state = Define.State.Move;
        _agent.speed = _speed;
    }

    protected virtual void UpdateMove()
    {
        Vector3 dir = Target.position - transform.position;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
        _agent.SetDestination(Target.position);

        if ((Target.position - transform.position).magnitude <= _attackRange)
        {
            State = Define.State.Attack;
            _agent.SetDestination(transform.position);
            return;
        }
    }

    protected virtual void UpdateAttack()
    {
        Debug.Log("Update Attack");

        if ((Target.position - transform.position).magnitude > _attackRange)
        {
            State = Define.State.Move;
            Quaternion qua = Quaternion.LookRotation(Target.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, 20 * Time.deltaTime);
            return;
        }
    }

    public virtual void OnDamaged(float damage, Define.Hit hit = Define.Hit.Bullet)
    {
        if (_state == Define.State.Die)
            return;

        _hp -= damage;
        State = Define.State.Hit;
        _hpBar.OnUpdateUI(_hp / _maxHP);
        Debug.Log($"{gameObject.name} {_hp}");

        //@Todo make knock back
        _agent.SetDestination(transform.position);

        if (_hp <= 0)
        {
            State = Define.State.Die;

            if (hit == Define.Hit.Bomb)
                _myGold = (4 / 10) * _myGold;
            Managers.Game.CurrentGold += _myGold;
            Managers.Game.CurrentScore += _myScore;
        }
    }

    public virtual void OnHit()
    {
        if ((Target.position - transform.position).magnitude > _attackRange)
            State = Define.State.Move;
        else
            State = Define.State.Attack;
    }

    public virtual void OnAttack() 
    {
        Debug.Log("OnAttack");
        Managers.Game.Tower.OnDamaged(_damage); 
    }

    public virtual void OnStun()
    {
        State = Define.State.Stun;
        _agent.enabled = false;
    }

    public virtual void OnDie()
    {
        Debug.Log($"Die {name}");
        Managers.Game.mobs.Remove(this);
        Managers.Pool.Push(GetComponent<Poolable>());
    }
}
