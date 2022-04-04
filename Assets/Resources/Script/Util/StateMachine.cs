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
    /// <typeparam name="TDriver">�߰� �̺�Ʈ</typeparam>
    public class StateMachine<TState,TDriver>
    {
        public event Action<TState> Changed;

        /*public TState State
        {
            get 
            {

            }
        }*/

        public void ChangeState(TState tState)
        {

        }
    }

    public interface IStateMachine<TDriver>
    {
        TDriver Driver { get; }
    }


    // internal : ����� ���� ��� Ŭ���������� ���� ����
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
        private List<IStateMachine<StateDriverRunner>> stateMachineList = new List<IStateMachine<StateDriverRunner>>();

        // stateMachineList�� add ������ Initialize ��������ε�.. TState�� ��ã��  TODO
        //public StateMachine<TState> 



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
            
        }
    }



}

