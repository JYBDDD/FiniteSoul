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



    /// <summary>
    /// 발사체를 생성하는 메소드(Shoot 애니메이션에서 재생  -> Animation Event)
    /// </summary>
    public void ArrowSpawn()
    {
        // 발사체 생성
        var arrowObject = ObjectPoolManager.Instance.GetPool<Arrow>(Define.ProjectilePath.arrowPath, Resources.Load(Define.ProjectilePath.arrowPath).name);
        // 발사체 생성 위치값
        arrowObject.transform.position = SpawnPos.transform.position + InGameManager.Instance.Player.transform.forward;
        // 발사체 Forward 값
        var parentBowPos = InGameManager.Instance.Player.transform.position;
        arrowObject.transform.rotation = Quaternion.LookRotation(new Vector3(parentBowPos.x,0,parentBowPos.z));
        // 해당 발사체에 값 설정 
        var arrowAttackC = arrowObject.GetComponent<AttackController>();
        arrowAttackC.staticData = mainData;
        arrowAttackC.checkBool = true;

    }
}
