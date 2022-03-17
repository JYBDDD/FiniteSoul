using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 움직이는 객체의 부모 클래스
/// </summary>
public class MoveableObject : MonoBehaviour
{
    public StaticData mainData;

    // 캐릭터 상태
    public Define.State State = Define.State.Idle;

    // 캐릭터 콜라이더
    protected Collider coll;

    // 캐릭터 물리
    protected Rigidbody rigid;

    // 캐릭터 애니메이터
    protected Animator anim;

    /// <summary>
    /// 해당 캐릭터에 맞는 값으로 초기화
    /// </summary>
    public virtual void Initialize()
    {
        // 참조가 되어있지 않다면 추가
        coll ??= GetComponent<Collider>();
        rigid ??= GetComponent<Rigidbody>();
        anim ??= GetComponent<Animator>();

        SetStat();
    }

    /// <summary>
    /// 스텟 변경시 변경된 스탯을 재설정 해주는 메소드
    /// </summary>
    public virtual void SetStat()
    {
        



    }
}