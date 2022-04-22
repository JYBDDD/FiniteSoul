using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ��ƼŬ(����Ʈ)�� �θ� Ŭ����
/// </summary>
public class ParticleBase : MonoBehaviour,RecyclePooling
{
    /// <summary>
    /// �ش� ��ƼŬ �ý��� ����Ʈ
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
            // ��ƼŬ �ý��� ������ ����Ǿ��ٸ� ����
            if (!particle.isPlaying)
            {
                ObjectPoolManager.Instance.GetPush(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
