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
        arrowObject.transform.position = SpawnPos.transform.position;
        // 발사체 Forward 값
        // TODO
    }
}
