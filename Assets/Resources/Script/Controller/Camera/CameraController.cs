using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� ȸ���� ����� Ŭ����
/// </summary>
public class CameraController : MonoBehaviour
{
    // �÷��̾� ���� ��ġ���� 0.4�� ���� �ʰ� ���󰡵��� �ϱ� TODO
    public GameObject player;   // �÷��̾� ������ ������Ʈ�� �־��ٰ��� TODO
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
/*        if(!Input.anyKey && transform.position != originPos)   // �ƹ�Ű�� ������ �ʾ��� ��, ��ġ�� ���� 
        {
            transform.position = Vector3.Lerp(transform.position, originPos, Time.deltaTime);
            Debug.Log("����");
            Debug.Log($"originTrans : {originPos}"); // ���Ŀ� originTrans ���� �����;;
        }*/
        if(Input.GetKey(KeyCode.W))    // ����Ű �Է½� ī�޶� �ڷ� �и�
        {
            transform.position = Vector3.Lerp(originPos, new Vector3(originPos.x, originPos.y, originPos.z - 0.5f), Time.deltaTime);
            Debug.Log("����");

            // x �� 0����, y �� 2.7 ����, z �� �ִ� -3f / �ּ� -3.5f
            // ���Ѵ�� �и��� ���� �ƴ� ���� ���� �̻� �з��� ��, ���̻� �и��� �ʵ��� ����
        }
    }



    // �÷��̾ W Ű�� ������ �ִٸ� ī�޶� �������� �и�
    // �÷��̾ S Ű�� ������ �ִٸ� ī�޶� �������� �и�
    // �÷��̾ D Ű�� ������ �ִٸ� ī�޶� �������� �и�
    // �÷��̾ A Ű�� ������ �ִٸ� ī�޶� ���������� �и�
}
