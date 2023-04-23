using Util.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 움직이는 객체의 부모 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody))]   // Rigidbody 를 안넣는 경우 방지
public class MoveableObject : MonoBehaviour,RecyclePooling
{
    public StaticData mainData;

    // 캐릭터, 몬스터 상태 머신
    public StateMachine<Define.State> FSM = new StateMachine<Define.State>();

    // 캐릭터,몬스터 콜라이더
    protected Collider coll;

    // 캐릭터,몬스터 물리
    protected Rigidbody rigid;

    // 캐릭터,몬스터 애니메이터
    protected Animator anim;

    // AttackController 참조
    protected AttackController Ac;

    // 움직일 수 있는지 체크
    public bool NotToMove = true;

    /// <summary>
    /// 해당 캐릭터에 맞는 값으로 초기화
    /// </summary>
    public virtual void Initialize(StaticData staticData)
    {
        mainData = staticData;
    }

    /// <summary>
    /// 필요한 컴포넌트 값 추가 (모든 자식이 사용) / 각각 필요한 기능은 override 해서 사용
    /// </summary>
    public virtual void InsertComponent()
    {
        // 참조가 되어있지 않다면 추가
        coll ??= GetComponent<Collider>();
        rigid ??= GetComponent<Rigidbody>();
        anim ??= GetComponent<Animator>();
    }

    /// <summary>
    /// 공격자의 Collider 셋팅
    /// </summary>
    public virtual void AttackColliderSet() {}
}