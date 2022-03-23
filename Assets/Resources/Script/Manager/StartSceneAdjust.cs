using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StartScene �� �Ѱ��ϴ� Ŭ����
/// </summary>
public class StartSceneAdjust : MonoBehaviour
{
    [SerializeField]
    Image worldImg;          // ���

    [SerializeField]
    Button newButton;        // �����ϱ� ��ư

    [SerializeField]
    Button continueButton;   // �̾��ϱ� ��ư

    [SerializeField]
    Button exitButton;       // �����ϱ� ��ư

    

    private void Start()
    {
        // �ش� ��ư�� �̺�Ʈ�� AddListner�� ���ε� ���ٰ� �� TODO
        StartCoroutine(ChangeBackGroundColor());
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// ����ȭ�� ������ ��������� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeBackGroundColor()
    {
        float duration = 4f;    // ���� ����
        float time = 0;

        float R = Random.Range(0, 255);
        float G = Random.Range(0, 255);
        float B = Random.Range(0, 255);

        while (time < duration)
        {
            time += Time.deltaTime;

            worldImg.color = Color.Lerp(worldImg.color, new Color(R,G,B), time);        // ���� ��ġ�� �ɵ� (���� ���� ���� �ȸ³�?) ? TODO

            yield return null;
        }

        StartCoroutine(ChangeBackGroundColor());

        yield return null;
    }

}
