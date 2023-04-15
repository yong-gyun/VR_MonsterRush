using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public SceneBase CurrentScene { get { return GameObject.FindObjectOfType<SceneBase>(); } }

    public void Load(Define.Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void Clear() { CurrentScene.Clear(); }
}
