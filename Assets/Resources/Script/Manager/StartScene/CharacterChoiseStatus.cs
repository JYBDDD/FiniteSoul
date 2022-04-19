using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoiseStatus : MonoBehaviour
{
    #region 버튼,텍스트
    [SerializeField,Header("캐릭터창 버튼"),Tooltip("캐릭터 선택 버튼 -> 캐릭터 선택후 다음씬 이동 버튼")]
    Button ChoiseButton;
    [SerializeField,Tooltip("캐릭터 변경 버튼")]
    Button ChangeButton;
    [SerializeField,Tooltip("기본 캔버스로 리턴하는 버튼")]
    Button ReturnButton;
    [SerializeField, Tooltip("캐릭터 이름")]
    TextMeshProUGUI characterName;

    /// <summary>
    /// 캐릭터의 이름값을 넘겨 받을 변수
    /// </summary>
    public static string charcterStaticName;
    #endregion

    #region 캐릭터 스테이터스 변수
    [SerializeField,Header("캐릭터 스테이터스 변수"),Tooltip("최대체력")]
    Image maxHpImg;
    [SerializeField, Tooltip("공격력")]
    Image atkImg;
    [SerializeField, Tooltip("방어력")]
    Image defImg;
    [SerializeField, Tooltip("이동속도")]
    Image moveSpeedImg;
    #endregion

    #region 시네머신
    [SerializeField,Header("VirtualCam값 전환에 사용될 변수"),Tooltip("사용되는 메인 VirtualCam")]
    CinemachineVirtualCamera useVirtualCam;
    [SerializeField,Tooltip("캐릭터 전환시 사용될 오브젝트 Transform값 -> 캐릭터 개수만큼 들고있음")]
    List<Transform> characterTrans;
    [SerializeField,Tooltip("메인카메라 위치값")]
    Transform mainCameraTrans;

    /// <summary>
    /// 카메라 기본 위치값
    /// </summary>
    Vector3 cameraBasicPos;
    #endregion

    private void Start()
    {
        // 이벤트 바인딩
        ChoiseButton.onClick.AddListener(ChoiseCharacter);
        ChangeButton.onClick.AddListener(ChangerCharacter);
        ReturnButton.onClick.AddListener(ReturnCanvasMoving);

        // 카메라 기본 위치값 설정
        cameraBasicPos = new Vector3(4f,2.5f,3f);
    }

    /// <summary>
    /// 해당 캐릭터 스테이터스 값 설정
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

                // growthStat 이 Null 값으로 출력됨 TODO
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
    /// 캐릭터 선택을 하였을시 실행되는 메서드
    /// </summary>
    private void ChoiseCharacter()
    {
        // 데이터 초기화
        if(charcterStaticName.Contains("Archer") | charcterStaticName.Contains("Paladin"))
        {
            var playerData = GameManager.Instance.FullData.playersData.Where(_ => _.name == charcterStaticName).FirstOrDefault();
            ResourceUtil.NewDataReturn(playerData.index);
        }
        VirtualCamSetReturn();

        LoadingSceneAdjust.LoadScene("1001");
    }

    /// <summary>
    /// 바라볼 대상을 변경하는 메서드
    /// </summary>
    /// <param name="timelineAsset">실행시킬 타임라인</param>
    /// <param name="choiseCharacter">지정할 캐릭터</param>
    private void ChangerCharacter()
    {
        // 팔라딘 캐릭터를 보고있는 중이라면, 아처로 변경
        if (charcterStaticName.Contains("Paladin"))
        {
            charcterStaticName = "Archer";
            SettingData();
            return;
        }
        // 아처 캐릭터를 보고있는 중이라면, 팔라딘으로 변경
        if (charcterStaticName.Contains("Archer"))
        {
            charcterStaticName = "Paladin";
            SettingData();
            return;
        }


        void SettingData()
        {
            // 이름 설정
            characterName.text = charcterStaticName;

            var playerData = GameManager.Instance.FullData.playersData.Where(_ => _.name == charcterStaticName).SingleOrDefault();

            // 스탯값이 Null 이라면 재설정
            if (playerData.growthStat == null)
            {
                playerData = ResourceUtil.LoadSaveFile();        // 여기 고쳐졌나 확인하기  TODO
            }

            // 바라볼 대상 설정
            for (int i = 0; i < characterTrans.Count; ++i)
            {
                // 변경한 캐릭터로 카메라 포지션 전환
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
    /// 본래 캔버스가 있던 위치로 리턴
    /// </summary>
    private void ReturnCanvasMoving()
    {
        StartSceneAdjust.StartCanvasState = Define.UIDraw.Activation;
        StartSceneAdjust.ChoiseCanvasState = Define.UIDraw.Inactive;

        // VirtualCam -> 기본값으로 리턴
        VirtualCamSetReturn();
    }

    /// <summary>
    /// VirtualCam 설정값 리턴
    /// </summary>
    private void VirtualCamSetReturn()
    {
        mainCameraTrans.position = cameraBasicPos;
        useVirtualCam.enabled = true;
    }
}
