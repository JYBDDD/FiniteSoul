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

    /// <summary>
    /// 타겟에 닿았는지 체크
    /// </summary>
    bool hitCheck = false;

    AttackController attackController;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        attackController = GetComponent<AttackController>();
    }

    private void OnEnable()
    {
        // 중력값 초기화
        rigid.useGravity = false;
        // 물리값 미적용 상태
        rigid.isKinematic = true;

        // 타겟 체크 Bool값 재설정
        hitCheck = false;

        //rigid.AddForce(InGameManager.Instance.Player.transform.forward * 50f, ForceMode.Impulse);     TODO 삭제

    }

    public void movePoint(Vector3 hitPoint)
    {
        StartCoroutine(AccelArrow(hitPoint));
    }

    IEnumerator AccelArrow(Vector3 hitPoint)
    {
        while(true)
        {
            // 타겟에 닿은상태라면 종료
            if(hitCheck == true)
            {
                yield break;
            }

            // 거리가 다르더라도 동일한 속력으로 출력
            transform.position = Vector3.MoveTowards(transform.position, hitPoint, 0.3f);


            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 몬스터에게 맞았다면 실행
        if(other.gameObject.CompareTag("Monster"))
        {
            // 중력값 설정
            rigid.useGravity = true;
            // 물리값 적용 상태
            rigid.isKinematic = false;

            // 타겟 체크
            hitCheck = true;

            // 20초 활성화후 풀링매니저 리턴
            StartCoroutine(ArrowCheckTime(20f));
        }
        // 다른 곳에 맞았다면 실행
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Untagged"))
        {
            // 중력값 설정
            rigid.useGravity = true;
            // 물리값 적용 상태
            rigid.isKinematic = false;

            // 타겟 체크
            hitCheck = true;

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

        while(true)
        {
            if(time > checkTime)
            {
                // 오브젝트 반환
                ReturnObject();
                yield break;
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
