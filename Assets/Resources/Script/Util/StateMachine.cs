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
        public Action updateAction;

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
            GetMethod(action);
        }

        /// <summary>
        /// 한번만 호출해야하는 메소드 호출시 사용
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private Action GetMethod(Action action)
        {
            // ChangeState 메소드 실행시 enterUpdate의 Bool 값에 의해 변경되는 곳
            if(hasEnterUpdate == true)
            {
                updateAction = action;
            }
            if(hasEnterUpdate == false)
            {
                updateAction = null;
            }

            return action;
        }

        private Action UpdateMethod(Action updateAction)
        {
            return updateAction;
        }

        public void PassOverUpdate()
        {
            // 해당 업데이트용 대리자가 Null 이 아닐경우 실행
            if (updateAction != null)
            {
                UpdateMethod(updateAction);
                //Debug.Log(updateAction.Method);
            }
        }


    }




    /*// internal : 어셈블리 내의 상속 클래스에서만 접근 가능
    /// <summary>
    /// 스테이트 정리,설정
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TDriver"></typeparam>
    internal class StateOrganize<TState,TDriver>
    {
        public TState state;

        // 상태 제공자 콜백(결과값)
        private Func<TState> stateProviderCallback;

        // 스테이트 머신 전달용 fsm
        private StateMachine<TState, TDriver> fsm;


        /// <summary>
        /// 해당 루틴이 실행중일때 변경되는 Bool타입
        /// </summary>
        public bool hasPlayingRoutine;
        /// <summary>
        /// 해당 루틴으로 접근하려고 할때 사용할 대리자
        /// </summary>
        public Action EnterCall = StateMachineRunner.DoNothing;
        /// <summary>
        /// 해당 루틴으로 코루틴을 사용하려할시 사용할 대리자(반환값 필요)
        /// </summary>
        public Func<IEnumerator> EnterRoutine = StateMachineRunner.DoNothingCoroutine;


        public StateOrganize(StateMachine<TState,TDriver> fsm,TState state,Func<TState> stateProvider)
        {
            this.fsm = fsm;
            this.state = state; 
            stateProviderCallback = stateProvider;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StateMachineRunner : MonoBehaviour
    {
        /// <summary>
        /// 사용중인 스테이트머신을 들고있는 리스트
        /// </summary>
        private List<StateDriverRunner> stateMachineList = new List<StateDriverRunner>();

        public void StateMachineInit(GameObject thisObject)
        {
            
        }



        /// <summary>
        /// 아무작업(코루틴 X)도 실행중이지 않음 (초기 값 설정용)
        /// </summary>
        public static void DoNothing()
        {

        }

        /// <summary>
        /// 코루틴작업이 실행X (초기 값 설정용)
        /// </summary>
        /// <returns></returns>
        public static IEnumerator DoNothingCoroutine()
        {
            yield break;
        }
    }

    public class StateDriverRunner : MonoBehaviour
    {
        public void Update()
        {
            // 해당 리스트에 들어온 스테이트를 돌려줄것임, Update에 넣어야 한다면 TODO
        }

        public void FixedUpdate()
        {
            // 해당 리스트에 들어온 스테이트를 돌려줄것임, FixedUpdate에 넣어야 한다면 TODO
        }

        public void LateUpdate()
        {
            // 해당 리스트에 들어온 스테이트를 돌려줄것임, LateUpdate에 넣어야 한다면 TODO
        }
    }*/

}

