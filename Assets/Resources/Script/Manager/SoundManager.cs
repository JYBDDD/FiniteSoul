using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� �Ѱ� �Ŵ��� Ŭ����
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    /// <summary>
    /// BGM �� Effect�� ����ϴ� ����� �ҽ� �迭
    /// </summary>
    AudioSource[] audioSources = new AudioSource[(int)SoundType.Effect + 1];

    /// <summary>
    /// ����� Ŭ������ ������ ���� ��ųʸ�
    /// </summary>
    Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();

    protected override void Awake()
    {
        base.Awake();

        // ���� �Ŵ��� �ڽ����� BGM ����, Effect ���� ����
        string[] soundName = System.Enum.GetNames(typeof(SoundType));

        for (int i = 0; i < soundName.Length; ++i)
        {
            GameObject obj = new GameObject { name = soundName[i] };
            audioSources[i] = obj.AddComponent<AudioSource>();
            obj.transform.SetParent(transform);

            // �̸��� BGM ������� �ش� Ŭ���� ���� �ݺ�
            if (soundName[i] == "BGM")
            {
                audioSources[i].loop = true;
            }
        }

        // ����� ��ųʸ� ����
        AudioClipInit();
    }

    /// <summary>
    /// ����� �÷���
    /// </summary>
    /// <param name="name">��������� �̸�</param>
    /// <param name="soundType">�����Ÿ�� - BGM / Effect</param>
    /// <param name="soundPlayType">������÷��� Ÿ�� - Single / Multi</param>
    public void PlayAudio(string name, SoundPlayType soundPlayType, SoundType soundType = SoundType.Effect)
    {
        // Effect ����
        if(soundType == SoundType.Effect)
        {
            AudioSource audioSource = audioSources[(int)SoundType.Effect];

            // �ϳ��� Ŭ�� ����
            if(soundPlayType == SoundPlayType.Single)
            {
                audioSource.clip = AudioOnShotFind(name);
            }
            // �������� Ŭ���� �������� �Ѱ��� Ŭ�� ����
            else
            {
                var audioList = AudioMultiShotFind(name);
                audioSource.clip = audioList[UnityEngine.Random.Range(0, audioList.Count)];
            }

            audioSource.PlayOneShot(audioSource.clip);
        }
        // BGM ����
        else
        {
            AudioSource audioSource = audioSources[(int)SoundType.BGM];

            audioSource.clip = AudioOnShotFind(name);
            audioSource.Play();
        }
    }

    /// <summary>
    /// �ش�� �̸��� ���� BGM �������ִ� �޼���
    /// </summary>
    public void SceneConversionBGM()
    {
        Scene scene = SceneManager.GetActiveScene();

        // �ش� ���� �´� BGM ����� ����
        AudioClip bgmAudio = AudioOnShotFind(scene.name);
        PlayAudio(bgmAudio.name,SoundPlayType.Single, SoundType.BGM);
    }

    /// <summary>
    /// ��ųʸ����� ����� �������� ���� ����Ϸ��� �Ҷ� �ش�� ��� Ŭ���� ã�� �޼���
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private List<AudioClip> AudioMultiShotFind(string name)
    {
        List<AudioClip> audioList = new List<AudioClip>();

        foreach (KeyValuePair<string,AudioClip> audio in audioClipDic)
        {
            if(audio.Key.Contains(name))
            {
                audioList.Add(audio.Value);
            }
        }

        return audioList;
    }

    /// <summary>
    /// ��ųʸ����� ����� �ϳ��� ��½�ų Ŭ���� ã�� �޼���
    /// </summary>
    public AudioClip AudioOnShotFind(string name)
    {
        foreach(KeyValuePair<string,AudioClip> audio in audioClipDic)
        {
            if(audio.Key.Contains(name))
            {
                return audio.Value;
            }
        }

        return null;
    }



    /// <summary>
    /// ����� Ŭ�� �ʱ� ��ųʸ� ����
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    private void AudioClipInit()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Art/Sound");

        for(int i =0; i < audioClips.Length; ++i)
        {
            audioClipDic.Add(audioClips[i].name, audioClips[i]);
        }

    }
}


/// <summary>
/// ���� Ÿ�� (���, ����Ʈ)
/// </summary>
public enum SoundType
{
    BGM,
    Effect,
}

/// <summary>
/// �����ų Ÿ�� (�Ѱ�, ������ - ����)
/// </summary>
public enum SoundPlayType
{
    Single,
    Multi,
}