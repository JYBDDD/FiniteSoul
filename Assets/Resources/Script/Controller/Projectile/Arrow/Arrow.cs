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
    /// 발사체(화살)의 강체
    /// </summary>
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // 중력값 초기화
        rigid.useGravity = false;

        rigid.AddForce(InGameManager.Instance.Player.transform.forward * 50f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 몬스터에게 맞았다면 실행
        if(other.gameObject.CompareTag("Monster"))
        {
            // 중력값 설정
            rigid.useGravity = true;

            // 20초 활성화후 풀링매니저 리턴
            StartCoroutine(ArrowCheckTime(20f));
        }
        // 다른 곳에 맞았다면 실행
        else
        {
            // 중력값 설정
            rigid.useGravity = true;

            // 한번 맞은후 체크하지 않도록 변경
            gameObject.GetComponent<AttackController>().checkBool = false;

            // 3초 활성화후 풀링매니저 리턴
            StartCoroutine(ArrowCheckTime(3f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 몬스터에게 맞았다면 실행
        if (other.gameObject.CompareTag("Monster"))
        {
            // 중력값 설정
            rigid.useGravity = true;

            // 20초 활성화후 풀링매니저 리턴
            StartCoroutine(ArrowCheckTime(20f));
        }
        // 다른 곳에 맞았다면 실행
        else
        {
            // 중력값 설정
            rigid.useGravity = true;

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

            if(time > 0.3f && notCheck == false)
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
