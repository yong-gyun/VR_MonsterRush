using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolManager
{
    #region //Pool
    public class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        public Stack<Poolable> _poolStack { get; private set; } = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject go = UnityEngine.Object.Instantiate(Original);
            go.name = Original.name;

            Poolable poolable = go.GetOrAddComponent<Poolable>();
            return poolable;
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Vector3 position, Transform parent)
        {
            Poolable poolable = null;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            if (parent == null)
                parent = Managers.Scene.CurrentScene.transform;

            NavMeshAgent nav = poolable.GetComponent<NavMeshAgent>();

            if (nav != null)
            {
                nav.enabled = false;

                poolable.transform.SetParent(parent);
                poolable.IsUsing = true;
                poolable.transform.position = position;

                nav.enabled = true;
                
                return poolable;
            }
                

            poolable.transform.SetParent(parent);
            poolable.IsUsing = true;
            poolable.transform.position = position;
            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root = null;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            UnityEngine.Object.DontDestroyOnLoad(_root);
        }

        CreatePool(Managers.Resource.Load<GameObject>("Prefabs/Character/Wolf"), 20);
        CreatePool(Managers.Resource.Load<GameObject>("Prefabs/Character/Inferno"), 20);
        CreatePool(Managers.Resource.Load<GameObject>("Prefabs/Character/Crab"), 20);
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Vector3 position, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            return null;
        Poolable pool = _pool[original.name].Pop(position, parent);
        return pool;
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original;
    }

    public void CollectPool()
    {
        for(int i = 0; i < Managers.Game.mobs.Count; i++)
        {
            if (Managers.Game.mobs[i].GetComponent<Poolable>())
                Push(Managers.Game.mobs[i].GetComponent<Poolable>());
        }
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child);

        _pool.Clear();
    }
}
