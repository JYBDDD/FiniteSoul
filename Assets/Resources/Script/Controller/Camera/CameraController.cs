using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 회전을 담당할 클래스
/// </summary>
public class CameraController : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;  // 필요없음 지워도 됨 TODO

    private void Update()
    {
        
    }

    private void CameraRotation()
    {
        // 카메라가 바라보는 목표 -> 마우스 위치 TODO

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
