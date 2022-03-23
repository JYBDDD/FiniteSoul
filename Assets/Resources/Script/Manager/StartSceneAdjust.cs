using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StartScene 을 총괄하는 클래스
/// </summary>
public class StartSceneAdjust : MonoBehaviour
{
    [SerializeField]
    Image worldImg;          // 배경

    [SerializeField]
    Button newButton;        // 새로하기 버튼

    [SerializeField]
    Button continueButton;   // 이어하기 버튼

    [SerializeField]
    Button exitButton;       // 종료하기 버튼

    

    private void Start()
    {
        // 해당 버튼의 이벤트는 AddListner로 바인딩 해줄것 임 TODO
        StartCoroutine(ChangeBackGroundColor());
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// 바탕화면 색상을 변경시켜줄 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeBackGroundColor()
    {
        float duration = 4f;    // 변경 간격
        float time = 0;

        float R = Random.Range(0, 255);
        float G = Random.Range(0, 255);
        float B = Random.Range(0, 255);

        while (time < duration)
        {
            time += Time.deltaTime;

            worldImg.color = Color.Lerp(worldImg.color, new Color(R,G,B), time);        // 여기 고치면 될듯 (색상 변경 값이 안맞나?) ? TODO

            yield return null;
        }

        StartCoroutine(ChangeBackGroundColor());

        yield return null;
    }

}
