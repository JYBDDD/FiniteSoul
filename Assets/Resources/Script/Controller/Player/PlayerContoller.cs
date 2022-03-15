using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MoveableObject
{
    
    protected override void Awake()
    {
        base.Awake();

        // ���� �� �÷��̾� �ڽĿ� ����ī�޶� �������� �ʴ´ٸ� ����ī�޶� �־��� �� TODO
        //  -> ���� �ӽ������� �ȶ�򿡰� �־���

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;
    }

    #region ĳ���� ������ ������
    private void Move()
    {
        // �̵� �� ����������� TODO
        float floatX = Input.GetAxisRaw("Horizontal") * 3f;
        float floatZ = Input.GetAxisRaw("Vertical") * 3f;

        Vector3 posVec = new Vector3(floatX, 0, floatZ).normalized * Time.deltaTime;

        transform.Translate(posVec.x, 0, posVec.z);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            // ���� �� ����������� TODO
            rigid.AddForce(Vector3.up * 7f,ForceMode.Impulse);
        }
    }


    #endregion
}
