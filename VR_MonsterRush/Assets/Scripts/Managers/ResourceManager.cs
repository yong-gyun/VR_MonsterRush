using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceManager
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        T obj = Resources.Load<T>(path);

        if(obj == null)
        {
            Debug.Log($"Faild : not found this path {path}");
            return null;
        }

        return obj;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null) 
            return null;

        if(prefab.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(prefab).gameObject;
        }

        GameObject go = Object.Instantiate(prefab, parent);

        int index = go.name.IndexOf("(Clone)");

        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);

        if (go == null)
            return null;

        go.transform.position = position;
        go.transform.rotation = rotation;

        return go;
    }

    public void Destroy(GameObject go, float time = 0)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();

        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go, time);
    }
}