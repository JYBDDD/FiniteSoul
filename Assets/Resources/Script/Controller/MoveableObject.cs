using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 움직이는 객체의 부모 클래스
/// </summary>
public class MoveableObject : MonoBehaviour
{
    // 총괄 데이터


    // 캐릭터 콜라이더
    protected Collider coll;

    // 캐릭터 물리
    protected Rigidbody rigid;

    /// <summary>
    /// 해당 캐릭터에 맞는 값으로 초기화
    /// </summary>
    public virtual void Initialize()
    {
        
    }

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
    }
}
