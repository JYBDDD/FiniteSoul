using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� ȸ���� ����� Ŭ����
/// </summary>
public class CameraController : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;  // �ʿ���� ������ �� TODO

    private void Update()
    {
        
    }

    private void CameraRotation()
    {
        // ī�޶� �ٶ󺸴� ��ǥ -> ���콺 ��ġ TODO

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
