using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Dictionary<string, Dictionary<AudioSource,AudioClip>> audioClips = new Dictionary<string, Dictionary<AudioSource, AudioClip>>();

    private void Start()
    {
        // ���� �Ŵ��� �ڽ����� BGM ����, Effect ���� ����
        string[] soundName = System.Enum.GetNames(typeof(SoundType));

        for(int i = 0; i < soundName.Length - 1; ++i)
        {
            GameObject obj = new GameObject { name = soundName[i] };
            audioSources[i] = obj.AddComponent<AudioSource>();
            transform.SetParent(obj.transform, false);

            // �̸��� BGM ������� �ش� Ŭ���� ���� �ݺ�
            if(soundName[i] == "BGM")
            {
                audioSources[i].loop = true;
            }
        }
    }




    /// <summary>
    /// ����� Ŭ���̸����� ��ųʸ��� �����ϴ� ������ ã��, ������� ��ųʸ��� Ŭ�� ����
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    /*public AudioClip FindAudioClip(string clipName)
    {
        // �ش� ��ŷ��Ͽ�  ����� Ŭ���� ���� �̸��� ���ٸ� ������ ����, �ƴ϶�� �׳ɽ���  TODO




    }*/


}

/// <summary>
/// ���� Ÿ�� (���, ����Ʈ)
/// </summary>
public enum SoundType
{
    BGM,
    Effect,
}
