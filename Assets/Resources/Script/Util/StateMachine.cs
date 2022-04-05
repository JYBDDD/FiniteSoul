using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceUtil.StateMachine
{

    /// <summary>
    /// ���� �ӽ�
    /// </summary>
    /// <typeparam name="TState">���� ��</typeparam>
    public class StateMachine<TState>
    {
        // ������Ʈ�� ������Ͻ�, true�� ����Ǵ� BoolŸ��
        private bool hasEnterUpdate = false;
        // ������Ʈ�� ���� ���޽�Ű�� �븮��
        public Action updateAction;

        /// <summary>
        /// ����(�⺻�� Idle)
        /// </summary>
        public Define.State State = Define.State.Idle;

        /// <summary>
        /// �ش� ���¸ӽ��� ���°� ���� (�ѹ��� ȣ��)
        /// </summary>
        /// <param name="tState">�����ϰ��� �ϴ� ����</param>
        /// <param name="action">�����ϰ��� �ϴ� ���¿��� �����ų �޼ҵ�</param>
        /// <param name="enterUpdate">true üũ�� Update�� ��ȯ</param>
        public void ChangeState(Define.State tState,Action action,bool enterUpdate = false)
        {
            State = tState;
            hasEnterUpdate = enterUpdate;
            GetMethod(action);
        }

        /// <summary>
        /// �ѹ��� ȣ���ؾ��ϴ� �޼ҵ� ȣ��� ���
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private Action GetMethod(Action action)
        {
            // ChangeState �޼ҵ� ����� enterUpdate�� Bool ���� ���� ����Ǵ� ��
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
            // �ش� ������Ʈ�� �븮�ڰ� Null �� �ƴҰ�� ����
            if (updateAction != null)
            {
                UpdateMethod(updateAction);
                //Debug.Log(updateAction.Method);
            }
        }


    }




    /*// internal : ����� ���� ��� Ŭ���������� ���� ����
    /// <summary>
    /// ������Ʈ ����,����
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TDriver"></typeparam>
    internal class StateOrganize<TState,TDriver>
    {
        public TState state;

        // ���� ������ �ݹ�(�����)
        private Func<TState> stateProviderCallback;

        // ������Ʈ �ӽ� ���޿� fsm
        private StateMachine<TState, TDriver> fsm;


        /// <summary>
        /// �ش� ��ƾ�� �������϶� ����Ǵ� BoolŸ��
        /// </summary>
        public bool hasPlayingRoutine;
        /// <summary>
        /// �ش� ��ƾ���� �����Ϸ��� �Ҷ� ����� �븮��
        /// </summary>
        public Action EnterCall = StateMachineRunner.DoNothing;
        /// <summary>
        /// �ش� ��ƾ���� �ڷ�ƾ�� ����Ϸ��ҽ� ����� �븮��(��ȯ�� �ʿ�)
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
        /// ������� ������Ʈ�ӽ��� ����ִ� ����Ʈ
        /// </summary>
        private List<StateDriverRunner> stateMachineList = new List<StateDriverRunner>();

        public void StateMachineInit(GameObject thisObject)
        {
            
        }



        /// <summary>
        /// �ƹ��۾�(�ڷ�ƾ X)�� ���������� ���� (�ʱ� �� ������)
        /// </summary>
        public static void DoNothing()
        {

        }

        /// <summary>
        /// �ڷ�ƾ�۾��� ����X (�ʱ� �� ������)
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
            // �ش� ����Ʈ�� ���� ������Ʈ�� �����ٰ���, Update�� �־�� �Ѵٸ� TODO
        }

        public void FixedUpdate()
        {
            // �ش� ����Ʈ�� ���� ������Ʈ�� �����ٰ���, FixedUpdate�� �־�� �Ѵٸ� TODO
        }

        public void LateUpdate()
        {
            // �ش� ����Ʈ�� ���� ������Ʈ�� �����ٰ���, LateUpdate�� �־�� �Ѵٸ� TODO
        }
    }*/

}

