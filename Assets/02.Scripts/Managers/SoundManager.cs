using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; //�з���

    Dictionary<string, AudioClip> _audipClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i =0; i< soundNames.Length-1; i++) // maxcount �־���
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.BGM].loop = true;
            _audioSources[(int)Define.Sound.LoopEffect].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audipClips.Clear();
    }
   public void Play(string path, Define.Sound type = Define.Sound.Effect,float pitch = 1.0f) // ���� 2�� ����
   {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);

    }
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;
        if (type == Define.Sound.BGM)
        {
          
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];

            if (audioSource.isPlaying) // bgm�� �������̶��
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if(type ==Define.Sound.Effect) //effect
        {

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.LoopEffect];
            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();
        }

    }
    public void StopSound(string path)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, Define.Sound.LoopEffect);
        AudioSource audioSource = _audioSources[(int)Define.Sound.LoopEffect];
        audioSource.clip = audioClip;

        audioSource.Stop();

    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        AudioClip audioClip = null;

        if (type == Define.Sound.BGM)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path); // bgm�� ���ֺ��ϴ°� �ƴ϶�
        }


        else if(type== Define.Sound.Effect)//effect                �޸� �Ƴ���
        {
            if (_audipClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audipClips.Add(path, audioClip);
            }
        }
        else
        {
            if (_audipClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audipClips.Add(path, audioClip);
            }
        }
        if (audioClip == null)
            Debug.Log("AudioClip Missing! ");

        return audioClip;
    }
}
