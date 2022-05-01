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
    Dictionary<string, Dictionary<AudioSource,AudioClip>> audioClips = new Dictionary<string, Dictionary<AudioSource, AudioClip>>();

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
    }




    /// <summary>
    /// 오디오 클립이름으로 딕셔너리에 존재하는 값인지 찾음, 없을경우 딕셔너리에 클립 삽입
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    /*public AudioClip FindAudioClip(string clipName)
    {
        // 해당 딕셔러니에  오디오 클립에 같은 이름이 없다면 삽입후 실행, 아니라면 그냥실행  TODO




    }*/


}

/// <summary>
/// 사운드 타입 (브금, 이펙트)
/// </summary>
public enum SoundType
{
    BGM,
    Effect,
}
