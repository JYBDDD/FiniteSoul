using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoiseStatus : MonoBehaviour
{
    #region ��ư,�ؽ�Ʈ
    [SerializeField,Header("ĳ����â ��ư"),Tooltip("ĳ���� ���� ��ư -> ĳ���� ������ ������ �̵� ��ư")]
    Button ChoiseButton;
    [SerializeField,Tooltip("ĳ���� ���� ��ư")]
    Button ChangeButton;
    [SerializeField,Tooltip("�⺻ ĵ������ �����ϴ� ��ư")]
    Button ReturnButton;
    [SerializeField, Tooltip("ĳ���� �̸�")]
    TextMeshProUGUI characterName;

    /// <summary>
    /// ĳ������ �̸����� �Ѱ� ���� ����
    /// </summary>
    public static string charcterStaticName;
    #endregion

    #region ĳ���� �������ͽ� ����
    [SerializeField,Header("ĳ���� �������ͽ� ����"),Tooltip("�ִ�ü��")]
    Image maxHpImg;
    [SerializeField, Tooltip("���ݷ�")]
    Image atkImg;
    [SerializeField, Tooltip("����")]
    Image defImg;
    [SerializeField, Tooltip("�̵��ӵ�")]
    Image moveSpeedImg;
    #endregion

    #region �ó׸ӽ�
    [SerializeField,Header("VirtualCam�� ��ȯ�� ���� ����"),Tooltip("���Ǵ� ���� VirtualCam")]
    CinemachineVirtualCamera useVirtualCam;
    [SerializeField,Tooltip("ĳ���� ��ȯ�� ���� ������Ʈ Transform�� -> ĳ���� ������ŭ �������")]
    List<Transform> characterTrans;
    [SerializeField,Tooltip("����ī�޶� ��ġ��")]
    Transform mainCameraTrans;

    /// <summary>
    /// ī�޶� �⺻ ��ġ��
    /// </summary>
    Vector3 cameraBasicPos;
    #endregion

    private void Start()
    {
        // �̺�Ʈ ���ε�
        ChoiseButton.onClick.AddListener(ChoiseCharacter);
        ChangeButton.onClick.AddListener(ChangerCharacter);
        ReturnButton.onClick.AddListener(ReturnCanvasMoving);

        // ī�޶� �⺻ ��ġ�� ����
        cameraBasicPos = new Vector3(4f,2.5f,3f);
    }

    /// <summary>
    /// �ش� ĳ���� �������ͽ� �� ����
    /// </summary>
    private void StatusFillAmountChange(UsePlayerData playerData)
    {
        StartCoroutine(ChangeStatus(playerData));

        IEnumerator ChangeStatus(UsePlayerData playerData)
        {
            float duraction = 0.3f;
            float time = 0;
            while(true)
            {
                if(time >= duraction)
                {
                    ChangeEnd();
                    yield break;
                }

                time += Time.deltaTime;

                // growthStat �� Null ������ ��µ� TODO
                maxHpImg.fillAmount = Mathf.Lerp(maxHpImg.fillAmount, playerData.growthStat.maxHp / 300f, time / duraction);
                atkImg.fillAmount = Mathf.Lerp(atkImg.fillAmount, playerData.growthStat.atk / 50f, time / duraction);
                defImg.fillAmount = Mathf.Lerp(defImg.fillAmount, playerData.growthStat.def / 10f, time / duraction);
                moveSpeedImg.fillAmount = Mathf.Lerp(moveSpeedImg.fillAmount, playerData.moveSpeed / 10f, time / duraction);

                yield return null;
            }
        }


        void ChangeEnd()
        {
            maxHpImg.fillAmount = playerData.growthStat.maxHp / 300f;
            atkImg.fillAmount = playerData.growthStat.atk / 50f;
            defImg.fillAmount = playerData.growthStat.def / 10f;
            moveSpeedImg.fillAmount = playerData.moveSpeed / 10f;
        }

    }

    /// <summary>
    /// ĳ���� ������ �Ͽ����� ����Ǵ� �޼���
    /// </summary>
    private void ChoiseCharacter()
    {
        // ������ �ʱ�ȭ
        if(charcterStaticName.Contains("Archer") | charcterStaticName.Contains("Paladin"))
        {
            var playerData = GameManager.Instance.FullData.playersData.Where(_ => _.name == charcterStaticName).FirstOrDefault();
            ResourceUtil.NewDataReturn(playerData.index);
        }
        VirtualCamSetReturn();

        LoadingSceneAdjust.LoadScene("1001");
    }

    /// <summary>
    /// �ٶ� ����� �����ϴ� �޼���
    /// </summary>
    /// <param name="timelineAsset">�����ų Ÿ�Ӷ���</param>
    /// <param name="choiseCharacter">������ ĳ����</param>
    private void ChangerCharacter()
    {
        // �ȶ�� ĳ���͸� �����ִ� ���̶��, ��ó�� ����
        if (charcterStaticName.Contains("Paladin"))
        {
            charcterStaticName = "Archer";
            SettingData();
            return;
        }
        // ��ó ĳ���͸� �����ִ� ���̶��, �ȶ������ ����
        if (charcterStaticName.Contains("Archer"))
        {
            charcterStaticName = "Paladin";
            SettingData();
            return;
        }


        void SettingData()
        {
            // �̸� ����
            characterName.text = charcterStaticName;

            var playerData = GameManager.Instance.FullData.playersData.Where(_ => _.name == charcterStaticName).SingleOrDefault();

            // ���Ȱ��� Null �̶�� �缳��
            if (playerData.growthStat == null)
            {
                playerData = ResourceUtil.LoadSaveFile();        // ���� �������� Ȯ���ϱ�  TODO
            }

            // �ٶ� ��� ����
            for (int i = 0; i < characterTrans.Count; ++i)
            {
                // ������ ĳ���ͷ� ī�޶� ������ ��ȯ
                if (charcterStaticName.Contains("Paladin") == true)
                {
                    StartCoroutine(ChangeCharacterCourtine(Define.ChoiseCharacterPos.paladinPos));
                    StatusFillAmountChange(playerData);
                }
                if (charcterStaticName.Contains("Archer") == true)
                {
                    StartCoroutine(ChangeCharacterCourtine(Define.ChoiseCharacterPos.archerPos));
                    StatusFillAmountChange(playerData);
                }
                useVirtualCam.enabled = false;
            }
        }

        IEnumerator ChangeCharacterCourtine(Vector3 changePos)
        {
            float duraction = 0.3f;
            float time = 0;

            while (true)
            {
                if (time >= duraction)
                {
                    mainCameraTrans.position = changePos;
                    yield break;
                }

                time += Time.deltaTime;

                mainCameraTrans.position = Vector3.Lerp(mainCameraTrans.position, changePos, time / duraction);
                yield return null;
            }
        }

    }

    /// <summary>
    /// ���� ĵ������ �ִ� ��ġ�� ����
    /// </summary>
    private void ReturnCanvasMoving()
    {
        StartSceneAdjust.StartCanvasState = Define.UIDraw.Activation;
        StartSceneAdjust.ChoiseCanvasState = Define.UIDraw.Inactive;

        // VirtualCam -> �⺻������ ����
        VirtualCamSetReturn();
    }

    /// <summary>
    /// VirtualCam ������ ����
    /// </summary>
    private void VirtualCamSetReturn()
    {
        mainCameraTrans.position = cameraBasicPos;
        useVirtualCam.enabled = true;
    }
}
