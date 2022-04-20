using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사체(화살)의 클래스
/// </summary>
[RequireComponent(typeof(AttackController))]
public class Arrow : ProjectileBase
{
    /// <summary>
    /// 쫒는 중인 타겟 (MoveableObject 대상)
    /// </summary>
    public GameObject Target;

    /// <summary>
    /// 해당 타깃에 맞았는지 확인하는 값
    /// </summary>
    bool hitTarget;

    private void OnEnable()
    {
        // 타겟 체크값 초기화
        hitTarget = false;
    }

    /// <summary>
    /// 타겟설정
    /// </summary>
    /// <param name="target">따라갈 타겟(몬스터라면)</param>
    /// <param name="hitPos">(땅이라면) Vec 위치</param>
    public void TargetSetting(GameObject target,Vector3 hitPos)
    {
        if(target == null)
        {
            Target = null;

            // RigidBody.AddForce로 날리도록 설정
            StartCoroutine(NullTarget());

        }
        if(target != null)
        {
            // 타겟 설정
            Target = target;

            StartCoroutine(LockTargetTracking(target,hitPos));
        }

        IEnumerator LockTargetTracking(GameObject target,Vector3 hitPos)
        {
            // 땅에 맞았음   -> hitPos값으로 화살 추척
            if(hitPos != Vector3.zero)
            {
                while (true)
                {
                    // 타겟에 맞았다면 종료
                    if (hitTarget == true)
                    {
                        yield break;
                    }

                    // 타깃을 향해 회전
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(hitPos), Time.deltaTime * 3f);

                    // 타깃을 향해 이동
                    transform.position = Vector3.Lerp(transform.position, hitPos, Time.deltaTime * 5f);

                    yield return null;
                }
            }

            // 몬스터에 맞았음 -> target으로 추적
            if(hitPos == Vector3.zero)
            {
                while (true)
                {
                    // 타겟에 맞았다면 종료
                    if(hitTarget == true)
                    {
                        yield break;
                    }

                    // 타깃을 향해 회전
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.deltaTime * 3f);

                    // 타깃을 향해 이동
                    transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 5f);

                    yield return null;
                }
            }

            yield return null;
        }

        IEnumerator NullTarget()
        {
            float duraction = 3f;
            float time = 0;

            // 타깃을 향해 이동
            transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10f, ForceMode.Impulse);

            while (true)
            {
                if(time > duraction)
                {
                    // 발사체 풀링매니저에 반환
                    ReturnObject();
                    yield break;
                }

                time += Time.deltaTime;

                yield return null;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // 몬스터에게 맞았다면 실행
        if(other.gameObject.CompareTag("Monster"))
        {
            // 타겟에 맞았으므로 활성화
            hitTarget = true;

            // 해당 몬스터의 자식으로 넣은후 화살위치를 고정
            gameObject.transform.SetParent(other.gameObject.transform);

            // 20초 활성화후 풀링매니저 리턴
            StartCoroutine(ArrowCheckTime(20f));
        }
        // 다른 곳에 맞았다면 실행
        else
        {
            // 타겟에 맞았으므로 활성화
            hitTarget = true;

            // 한번 맞은후 체크하지 않도록 변경
            gameObject.GetComponent<AttackController>().checkBool = false;

            // 3초 활성화후 풀링매니저 리턴
            StartCoroutine(ArrowCheckTime(3f));
        }
    }

    /// <summary>
    /// 주어진 시간만큼 발사체 활성화
    /// </summary>
    /// <param name="checkTime">해당 시간만큼 활성화</param>
    /// <returns></returns>
    IEnumerator ArrowCheckTime(float checkTime)
    {
        float time = 0;
        bool notCheck = false;

        while(true)
        {
            if(time > checkTime)
            {
                // 오브젝트 반환
                ReturnObject();
                yield break;
            }

            if(time > 0.1f && notCheck == false)
            {
                // 한번 맞은후 체크하지 않도록 변경
                gameObject.GetComponent<AttackController>().checkBool = false;
                notCheck = true;
            }

            time += Time.deltaTime;

            yield return null;
        }
    }

    /// <summary>
    /// 해당 오브젝트 풀링매니저에 반환
    /// </summary>
    private void ReturnObject()
    {
        ObjectPoolManager.Instance.GetPush(gameObject, ObjectPoolManager.ParentProjectile.transform);
    }
}
