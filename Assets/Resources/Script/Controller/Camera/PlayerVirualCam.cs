using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 바라보는 VirtualCam 클래스
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
        // 플레이어를 따라가고 바라보도록 설정
        virtualCam.Follow = InGameManager.Instance.Player.transform;
        virtualCam.LookAt = InGameManager.Instance.Player.transform;
    }
}
