using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceUtil.StateMachine
{

    /// <summary>
    /// 상태 머신
    /// </summary>
    /// <typeparam name="TState">상태 값</typeparam>
    public class StateMachine<TState>
    {
        // 업데이트를 사용중일시, true로 변경되는 Bool타입
        private bool hasEnterUpdate = false;
        // 업데이트시 값을 전달시키는 대리자
        private Action updateAction;
        // 한번만 실행시키는 값을 전달시키는 대리자
        private Action onEnableAction;


        /// <summary>
        /// 상태(기본값 Idle)
        /// </summary>
        public Define.State State = Define.State.Idle;

        /// <summary>
        /// 해당 상태머신의 상태값 변경 (한번만 호출)
        /// </summary>
        /// <param name="tState">변경하고자 하는 상태</param>
        /// <param name="action">변경하고자 하는 상태에서 실행시킬 메소드</param>
        /// <param name="enterUpdate">true 체크시 Update로 전환</param>
        public void ChangeState(Define.State tState,Action action,bool enterUpdate = false)
        {
            State = tState;
            hasEnterUpdate = enterUpdate;
            onEnableAction = action;
            GetMethod(action);
        }

        /// <summary>
        /// 업데이트 시켜야할 값과 한번만 실행시켜야할 값을 구분해주는 메소드
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private void GetMethod(Action action)
        {
            // UpdateMethod() 실행
            if(hasEnterUpdate == true)
            {
                updateAction = action;
            }
            // GetMethod 실행
            if(hasEnterUpdate == false)
            {
                updateAction = null;
                onEnableAction.Invoke();
            }
        }

        /// <summary>
        /// 사용하는 컴포넌트에서 호출(FSM) 업데이트용
        /// </summary>
        public void UpdateMethod()
        {
            // 해당 업데이트용 대리자가 Null 이 아닐경우 실행
            if (updateAction != null)
            {
                updateAction.Invoke();
            }
        }
    }
}

