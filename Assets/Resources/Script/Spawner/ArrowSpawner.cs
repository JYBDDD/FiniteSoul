using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 화살을 스폰시키는 클래스
/// </summary>
public class ArrowSpawner : MonoBehaviour
{
    // Arrow 프리팹을 해당 스포너에서 정면으로 발사한다

    // 마우스가 가리키는 방향에 적이 있을경우
    // 해당 적을 향해 곡선으로 회전하며 날아간다

    // 근처에 적이 없다면 밑에 구문 실행
    // 발사된 Arrow는 임시로 플레이어의 앞방향값을 받아와 앞방향으로 꺾이게 한후, 날아가는값을 Vector3.forward 값으로 다시 바꾼다



    public void ArrowSpawn()
    {

    }

}
