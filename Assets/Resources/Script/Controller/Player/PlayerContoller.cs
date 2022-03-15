using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MoveableObject
{
    
    protected override void Awake()
    {
        base.Awake();

        // 시작 시 플레이어 자식에 메인카메라가 존재하지 않는다면 메인카메라를 넣어줄 것 TODO
        //  -> 현재 임시적으로 팔라딘에게 넣어줌

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;
    }

    #region 캐릭터 움직임 구현부
    private void Move()
    {
        // 이동 값 지정해줘야함 TODO
        float floatX = Input.GetAxisRaw("Horizontal") * 3f;
        float floatZ = Input.GetAxisRaw("Vertical") * 3f;

        Vector3 posVec = new Vector3(floatX, 0, floatZ).normalized * Time.deltaTime;

        transform.Translate(posVec.x, 0, posVec.z);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            // 점프 값 지정해줘야함 TODO
            rigid.AddForce(Vector3.up * 7f,ForceMode.Impulse);
        }
    }


    #endregion
}
