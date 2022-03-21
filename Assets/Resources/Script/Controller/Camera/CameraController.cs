using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 회전을 담당할 클래스
/// </summary>
public class CameraController : MonoBehaviour
{
    // 플레이어 현재 위치보다 0.4초 정도 늦게 따라가도록 하기 TODO
    public GameObject player;   // 플레이어 생성시 오브젝트를 넣어줄것임 TODO
    Vector3 originPos;

    private void Awake()
    {
        originPos = new Vector3(0, 2.7f, -3f);
    }

    private void Update()
    {
        CameraLerpMove();
    }

    private void CameraLerpMove()
    {
/*        if(!Input.anyKey && transform.position != originPos)   // 아무키도 누르지 않았을 시, 위치값 복귀 
        {
            transform.position = Vector3.Lerp(transform.position, originPos, Time.deltaTime);
            Debug.Log("복귀");
            Debug.Log($"originTrans : {originPos}"); // 추후에 originTrans 값이 변경됨;;
        }*/
        if(Input.GetKey(KeyCode.W))    // 전진키 입력시 카메라가 뒤로 밀림
        {
            transform.position = Vector3.Lerp(originPos, new Vector3(originPos.x, originPos.y, originPos.z - 0.5f), Time.deltaTime);
            Debug.Log("전진");

            // x 값 0고정, y 값 2.7 고정, z 값 최대 -3f / 최소 -3.5f
            // 무한대로 밀리는 것이 아닌 일정 범위 이상 밀렸을 시, 더이상 밀리지 않도록 설정
        }
    }



    // 플레이어가 W 키를 누르고 있다면 카메라가 뒤쪽으로 밀림
    // 플레이어가 S 키를 누르고 있다면 카메라가 앞쪽으로 밀림
    // 플레이어가 D 키를 누르고 있다면 카메라가 왼쪽으로 밀림
    // 플레이어가 A 키를 누르고 있다면 카메라가 오른쪽으로 밀림
}
