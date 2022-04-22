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

    public static RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void NormalAttackState()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if(hit.point == null)
        {
            hit.point = Vector3.forward;
        }

        base.NormalAttackState();
    }

    /// <summary>
    /// 발사체를 생성하는 메소드(Shoot 애니메이션에서 재생  -> Animation Event)
    /// </summary>
    public void ArrowSpawn()
    {
        // 발사체 생성
        var arrowObject = ObjectPoolManager.Instance.GetPool<Arrow>(Define.ProjectilePath.arrowPath,Define.CharacterType.Projectile);
        // 발사체 생성 위치값, 방향값 지정
        ResourceUtil.PosDirectionDesign(arrowObject, SpawnPos.transform.position, Quaternion.LookRotation(hit.point - transform.position)); 
        // 발사체 해당 방향으로 이동 설정
        arrowObject.GetComponent<Arrow>().movePoint(hit.point);
        // 해당 발사체에 값 설정 
        var arrowAttackC = arrowObject.GetComponent<AttackController>();
        arrowAttackC.AttackControllerInit(mainData, playerData.atkType);
    }
}
