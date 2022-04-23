using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ĳ���� ����� ������� �� Ŭ����
/// </summary>
public class Rune : MonoBehaviour,RecyclePooling
{
    /// <summary>
    /// ��� ���� Ȱ��ȭ���� �����ϴ� Ŭ����    -> DropRuneDataSetting ȣ��� Ȱ��ȭ�� (�÷��̾� �����)
    /// </summary>
    public static bool DropRuneTrue = false;

    /// <summary>
    /// ��� ���� ���� ��
    /// </summary>
    public static float DropRuneAmount = 0;

    /// <summary>
    /// ��� ���� ��ġ��
    /// </summary>
    public static Vector3 DropRunePos = Vector3.zero;

    /// <summary>
    /// ���� �ʵ����� ���
    /// </summary>
    public static void RuneDrop()
    {
        // ������ �ִ� ���� 0���� ���Ҿ���,
        // �� ����� ������ BoolŸ�� ������ Ȱ��ȭ �Ǿ��ٸ� ���� -> DropRuneDataSetting ȣ��� Ȱ��ȭ��(�÷��̾� �����)
        if(DropRuneTrue == true && DropRuneAmount > 0)
        {
            var runeObj = ObjectPoolManager.Instance.GetPool<Rune>(Define.DropRunePath.dropRunePath, Define.CharacterType.Rune);

            // ��� �� ��ġ�� ����
            runeObj.transform.position = DropRunePos;
        }

    }

    /// <summary>
    /// ���� ���� �÷��̾�� �ѱ�� �޼���
    /// </summary>
    public static void DropRuneHandOver()
    {
        InGameManager.Instance.Player.playerData.currentRune += DropRuneAmount;
    }

    /// <summary>
    /// ����� ���� ������ ����   (�÷��̾� ����� ȣ�� , ���������嵵 ���� ����)
    /// </summary>
    /// <param name="dropRune">����� ���� ��</param>
    /// <param name="dropPos">����� ��ġ</param>
    public static void DropRuneDataSetting(float dropRune,Vector3 dropPos)
    {
        // ������� Ȱ��ȭ�ϵ��� ����
        DropRuneTrue = true;

        // ��� ����� ��ġ�� ����
        DropRuneAmount = dropRune;
        DropRunePos = dropPos;


        // ���� ����Ǿ��� �ֹ߼� ������     ->  (��ġ, �÷��̾� �ε��� ���� ���)
        var loadFile = ResourceUtil.LoadSaveFile().playerVolatility;

        // ����Ǵ� �÷��̾���ġ�� ���� ��ں���ġ�� ����, �ƴ϶�� Vector3.Zero 
        var playerVolData = new PlayerVolatilityData(InGameManager.Instance.Player.playerData, new Vector3(loadFile.posX, loadFile.posY, loadFile.posZ), StageManager.stageData);

        UsePlayerData playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == loadFile.index).SingleOrDefault();
        GrowthStatData growthStatData = GameManager.Instance.FullData.growthsData.Where(_ => _.index == playerData.growthRef).SingleOrDefault();

        // ���� �־��� ����� ������
        UsePlayerData changeData = new UsePlayerData(growthStatData, playerData, playerVolData);

        // �÷��̾��� CurrentRune = 0; ���� ����
        changeData.currentRune = 0;

        // ���� ü�� ������
        // �ø� �����Ͱ� �������� �ʴ´ٸ�
        if(playerVolData.raiseHp == 0)
        {
            // �⺻������ ����
            changeData.playerVolatility.currentHp = playerData.maxHp;
        }
        else
        {
            // �ø� ���ݰ����� ����
            changeData.playerVolatility.currentHp = playerVolData.raiseHp;
        }


        // ������ ����
        ResourceUtil.SaveData(new PlayerVolatilityData(changeData, new Vector3(loadFile.posX, loadFile.posY, loadFile.posZ), StageManager.stageData));
    }
}
