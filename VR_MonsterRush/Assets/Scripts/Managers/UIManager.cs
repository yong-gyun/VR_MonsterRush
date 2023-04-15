using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    Stack<UI_Popup> _stackPopup = new Stack<UI_Popup>();
    public List<UI_WorldSpace> worldSpaces { get; set; } = new List<UI_WorldSpace>();
    UI_Scene _sceneUI = null;
    int _order = 10;

    public GameObject Root
    {
        get
        {
            if(root == null)
            {
                GameObject go = GameObject.Find("@UI_Root");

                if (go == null)
                    go = new GameObject { name = "@UI_Root" };

                root = go;
            }

            return root;
        }
    }

    GameObject root = null;

    public void SetCanvas(GameObject go, bool sort = false)
    {
        if (go == null)
            return;
        
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.overrideSorting = true;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }

        canvas.worldCamera = Camera.main;
    }

    public T MakeWorldSpaceUI<T>(Transform parent, string name = null) where T : UI_WorldSpace
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}", parent);
        
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.WorldSpace;
        return go.GetOrAddComponent<T>();
    }

    public T ShowWorldSpaceUI<T>(string name = null) where T : UI_WorldSpace
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameObject.Find(name);

        if (go == null)
            return null;

        T component = go.GetComponent<T>();

        if(component != null)
        {
            go.gameObject.SetActive(true);
            worldSpaces.Add(component);
            return component;
        }

        return null;
    }

    public T ShowUIPopup<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}", Root.transform);
        T popup = Util.GetOrAddComponent<T>(go);
        _stackPopup.Push(popup);

        return popup;
    }

    public T ShowUIScene<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/{name}", Root.transform);
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        return sceneUI;
    }

    public void CloseWorldSpaceUI<T>(string name = null) where T : UI_WorldSpace
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Debug.Log(name);
        
        for(int i = 0; i < worldSpaces.Count; i++)
        {
            if (worldSpaces[i].name == name)
            {
                worldSpaces[i].gameObject.SetActive(false);
                return;
            }
        }
    }

    public void ClosePopupUI()
    {
        UI_Popup popup = _stackPopup.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popup != _stackPopup.Peek())
            return;

        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (_stackPopup.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        worldSpaces.Clear();
        CloseAllPopupUI();
    }
}