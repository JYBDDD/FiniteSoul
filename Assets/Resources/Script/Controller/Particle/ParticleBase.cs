using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 파티클(이펙트)의 부모 클래스
/// </summary>
public class ParticleBase : MonoBehaviour,RecyclePooling
{
    /// <summary>
    /// 해당 파티클 시스템 이펙트
    /// </summary>
    protected ParticleSystem particleEffect;

    protected virtual void Awake()
    {
        particleEffect = GetComponent<ParticleSystem>();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(CheckParticleTime(particleEffect));
    }

    protected IEnumerator CheckParticleTime(ParticleSystem particle)
    {
        while (true)
        {
            // 파티클 시스템 실행이 종료되었다면 실행
            if (!particle.isPlaying)
            {
                ObjectPoolManager.Instance.GetPush(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
