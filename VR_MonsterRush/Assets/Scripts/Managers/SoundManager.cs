using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    Dictionary<Define.SoundEffect, AudioClip> _effectClips = new Dictionary<Define.SoundEffect, AudioClip>();
    Dictionary<Define.BGM, AudioClip> _bgmClips = new Dictionary<Define.BGM, AudioClip>();

    AudioSource[] _effectSources = new AudioSource[System.Enum.GetValues(typeof(Define.SoundEffect)).Length];
    AudioSource _bgmSource = null;

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            for (int i = 0; i < System.Enum.GetValues(typeof(Define.BGM)).Length; i++)
            {
                AudioClip clip = Managers.Resource.Load<AudioClip>($"Sound/BGM/{(Define.BGM)i}");
                _bgmClips.Add((Define.BGM)i, clip);
            }

            for (int i = 0; i < System.Enum.GetValues(typeof(Define.SoundEffect)).Length; i++)
            {
                AudioClip clip = Managers.Resource.Load<AudioClip>($"Sound/Effect/{(Define.SoundEffect)i}");
                _effectClips.Add((Define.SoundEffect)i, clip);
            }

            if (_bgmSource == null)
            {
                GameObject go = new GameObject { name = "BGM_Soruce" };
                _bgmSource = go.AddComponent<AudioSource>();
                go.transform.SetParent(root.transform);
            }

            _bgmSource.loop = true;

            for (int i = 0; i < _effectSources.Length; i++)
            {
                GameObject go = new GameObject { name = $"SFX_Source({i + 1})" };
                go.transform.SetParent(root.transform);
                _effectSources[i] = go.AddComponent<AudioSource>();
            }
        }
    }

    public void PlayBGM(Define.BGM type)
    {
        if (_bgmSource.isPlaying)
            _bgmSource.Stop();

        if (type == Define.BGM.Clear)
            _bgmSource.loop = false;
        else
            _bgmSource.loop = true;

        _bgmSource.clip = _bgmClips[type];
        _bgmSource.Play();
    }

    public void PlaySoundEffect(Define.SoundEffect type)
    {
        for (int i = 0; i < _effectSources.Length; i++)
        {
            if (_effectSources[i].isPlaying == false)
            {
                _effectSources[i].PlayOneShot(_effectClips[type]);
                return;
            }
        }
    }

    public void StopBGM()
    {
        if (_bgmSource.isPlaying)
            _bgmSource.Stop();

        _bgmSource.clip = null;
    }

    public void SetVolumeToBgm(float volume)
    {
        _bgmSource.volume = volume;
    }

    public void SetVolumeToEffect(float volume)
    {
        for(int i = 0; i < _effectSources.Length; i++)
        {
            _effectSources[i].volume = volume;
        }
    }

}
