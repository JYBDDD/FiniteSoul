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
        private Action updateAction;
        // �ѹ��� �����Ű�� ���� ���޽�Ű�� �븮��
        private Action onEnableAction;


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
            onEnableAction = action;
            GetMethod(action);
        }

        /// <summary>
        /// ������Ʈ ���Ѿ��� ���� �ѹ��� ������Ѿ��� ���� �������ִ� �޼ҵ�
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private void GetMethod(Action action)
        {
            // UpdateMethod() ����
            if(hasEnterUpdate == true)
            {
                updateAction = action;
            }
            // GetMethod ����
            if(hasEnterUpdate == false)
            {
                updateAction = null;
                onEnableAction.Invoke();
            }
        }

        /// <summary>
        /// ����ϴ� ������Ʈ���� ȣ��(FSM) ������Ʈ��
        /// </summary>
        public void UpdateMethod()
        {
            // �ش� ������Ʈ�� �븮�ڰ� Null �� �ƴҰ�� ����
            if (updateAction != null)
            {
                updateAction.Invoke();
            }
        }
    }
}

