using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 움직이는 객체의 부모 클래스
/// </summary>
public class MoveableObject : MonoBehaviour,RecyclePooling
{
    public StaticData mainData;

    // 캐릭터,몬스터 상태
    public Define.State State = Define.State.Idle;

    // 캐릭터,몬스터 콜라이더
    protected Collider coll;

    // 캐릭터,몬스터 물리
    protected Rigidbody rigid;

    // 캐릭터,몬스터 애니메이터
    protected Animator anim;

    // AttackController 참조 받을 것임 TODO

    /// <summary>
    /// 해당 캐릭터에 맞는 값으로 초기화
    /// </summary>
    public virtual void Initialize(MoveableObject thisObject)
    {
        mainData = thisObject.mainData;

        // AttackController 참조 박을 것임 TODO

    }

    /// <summary>
    /// 필요한 컴포넌트 값 추가 (모든 자식이 사용)
    /// </summary>
    public virtual void InsertComponent()
    {
        // 참조가 되어있지 않다면 추가
        coll ??= GetComponent<Collider>();
        rigid ??= GetComponent<Rigidbody>();
        anim ??= GetComponent<Animator>();
    }
}