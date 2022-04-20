using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사체(화살)의 클래스
/// </summary>
public class Arrow : ProjectileBase
{
    public GameObject Target = null;
    Vector3 dir;

    private void OnEnable()
    {
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var angle = Mathf.Atan2(dir.z - transform.position.z, dir.x - transform.position.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        // 방향값 수정 필요함 TODO

    }

    private void Update()
    {
        //gameObject.GetComponent<Transform>().transform.Translate(dir * 2f * Time.deltaTime);
    }

    /// <summary>
    /// 타겟설정
    /// </summary>
    public void TargetSetting(GameObject target = null)
    {
        if(target == null)
        {
            // 발사된 Arrow는 임시로 플레이어의 앞방향값을 받아와 앞방향으로 꺾이게 한후, 날아가는값을 Vector3.forward 값으로 다시 바꾼다 TODO
        }
        if(target != null)
        {
            // 타겟 설정
            Target = target;

            // 해당 적을 향해 곡선으로 회전하며 날아간다  TODO
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 몬스터에게 맞았다면 실행
        if(other.gameObject.CompareTag("Monster"))
        {
            // 해당 몬스터의 자식으로 넣은후 화살위치를 고정 TODO

            // 맞은후 20초뒤 화살을 ReturnObject()로 반환시키는 코루틴 실행 TODO
        }
        // 다른 곳에 맞았다면 실행
        else
        {
            // 튕긴후 3초뒤 ReturnObject() 반환 시킬것 TODO
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
