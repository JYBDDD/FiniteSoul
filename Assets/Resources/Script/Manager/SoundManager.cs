using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사운드 총괄 매니저 클래스
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    /// <summary>
    /// BGM 과 Effect를 사용하는 오디오 소스 배열
    /// </summary>
    AudioSource[] audioSources = new AudioSource[(int)SoundType.Effect + 1];

    /// <summary>
    /// 오디오 클립들을 가지고 있을 딕셔너리
    /// </summary>
    Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();

    private void Start()
    {
        // 사운드 매니저 자식으로 BGM 폴더, Effect 폴더 생성
        string[] soundName = System.Enum.GetNames(typeof(SoundType));

        for(int i = 0; i < soundName.Length - 1; ++i)
        {
            GameObject obj = new GameObject { name = soundName[i] };
            audioSources[i] = obj.AddComponent<AudioSource>();
            transform.SetParent(obj.transform, false);

            // 이름이 BGM 폴더라면 해당 클립은 무한 반복
            if(soundName[i] == "BGM")
            {
                audioSources[i].loop = true;
            }
        }

        // 오디오 딕셔너리 설정
        AudioClipInit();
    }

    /// <summary>
    /// 오디오 플레이
    /// </summary>
    /// <param name="name">오디오파일 이름</param>
    /// <param name="soundType">오디오타입 - BGM / Effect</param>
    /// <param name="soundPlayType">오디오플레이 타입 - Single / Multi</param>
    public void PlayAudio(string name,SoundType soundType,SoundPlayType soundPlayType)
    {
        // Effect 실행
        if(soundType == SoundType.Effect)
        {
            AudioSource audioSource = audioSources[(int)SoundType.Effect];

            // 하나의 클립 실행
            if(soundPlayType == SoundPlayType.Single)
            {
                audioSource.clip = AudioOnShotFind(name);
            }
            // 여러개의 클립중 랜덤으로 한개의 클립 실행
            else
            {
                var audioList = AudioMultiShotFind(name);
                audioSource.clip = audioList[UnityEngine.Random.Range(0, audioList.Count)];
            }

            audioSource.PlayOneShot(audioSource.clip);
        }
        // BGM 실행
        else
        {
            AudioSource audioSource = audioSources[(int)SoundType.BGM];

            audioSource.clip = AudioOnShotFind(name);
            audioSource.Play();
        }
    }

    /// <summary>
    /// 딕셔너리에서 오디오 여러개를 랜덤 출력하려고 할시 해당된 모든 클립을 찾는 메서드
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
    /// 딕셔너리에서 오디오 하나만 출력시킬 클립을 찾는 메서드
    /// </summary>
    private AudioClip AudioOnShotFind(string name)
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
    /// 오디오 클립 초기 딕셔너리 설정
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
/// 사운드 타입 (브금, 이펙트)
/// </summary>
public enum SoundType
{
    BGM,
    Effect,
}

/// <summary>
/// 실행시킬 타입 (한개, 여러게 - 랜덤)
/// </summary>
public enum SoundPlayType
{
    Single,
    Multi,
}
