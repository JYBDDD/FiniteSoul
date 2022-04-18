using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �ٶ󺸴� VirtualCam Ŭ����
/// </summary>
public class PlayerVirualCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        // �÷��̾ ���󰡰� �ٶ󺸵��� ����
        virtualCam.Follow = InGameManager.Instance.Player.transform;
        virtualCam.LookAt = InGameManager.Instance.Player.transform;
    }
}
