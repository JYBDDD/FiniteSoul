using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 (궁수)클래스
/// </summary>
public class Archer : PlayerController
{
    [SerializeField, Tooltip("발사체 생성 위치값")]
    GameObject SpawnPos;

    /// <summary>
    /// 락온된 타겟값을 생성시킨 발사체에 넘겨주는 변수
    /// </summary>
    public GameObject lockOnTarget;

    /// <summary>
    /// 락온된 위치값
    /// </summary>
    public Vector3 hitPos;

    RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void NormalAttackState()
    {
        // 현재 마우스 위치로 Ray를 쏜후 몬스터, 땅에 맞았다면 타겟을 발사체의 Target값으로 넘겨준다
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Monster") + 1 << LayerMask.NameToLayer("Default");
        if (Physics.Raycast(ray,out hit,layerMask))
        {
            // 땅에 맞았다면 실행
            if(!hit.transform.CompareTag("Monster"))
            {
                // 위치값 삽입
                hitPos = hit.point;
                // 몬스터가 아니더라도 값은 임시로 들고있는다
                lockOnTarget = hit.transform.gameObject;
            }
            // 몬스터에 맞았다면
            if(hit.transform.CompareTag("Monster"))
            {
                // 타겟 설정
                lockOnTarget = hit.transform.gameObject;
                // 위치값 초기화
                hitPos = Vector3.zero;
            }

        }
        // 아닐경우 타겟값을 Null 값 셋팅
        else
        {
            lockOnTarget = null;
            hitPos = Vector3.zero;
        }

        base.NormalAttackState();

    }





    /// <summary>
    /// 발사체를 생성하는 메소드(Shoot 애니메이션에서 재생  -> Animation Event)
    /// </summary>
    public void ArrowSpawn()
    {
        // 발사체 생성
        var arrowObject = ObjectPoolManager.Instance.GetPool<Arrow>(Define.ProjectilePath.arrowPath, Resources.Load(Define.ProjectilePath.arrowPath).name);
        // 발사체 생성 위치값
        arrowObject.transform.position = SpawnPos.transform.position;
        // 발사체 Forward 값
        var parentBowPos = SpawnPos.transform.parent.position;
        arrowObject.transform.rotation = Quaternion.LookRotation(new Vector3(parentBowPos.x,0,parentBowPos.z));
        // 발사체 타겟 설정(타겟이 null 일경우도 발사체에서 감지)
        arrowObject.GetComponent<Arrow>().TargetSetting(lockOnTarget,hitPos);
        // 해당 발사체에 값 설정 
        var arrowAttackC = arrowObject.GetComponent<AttackController>();
        arrowAttackC.staticData = mainData;
        arrowAttackC.checkBool = true;

    }
}
