using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ΰ��ӿ� ���õ� �����͸� ������� �Ŵ��� Ŭ����
/// </summary>
public class InGameManager : Singleton<InGameManager>
{
    // �÷��̾�
    public MoveableObject Player = null;
    // ���� ����Ʈ
    public List<MoveableObject> Monsters = new List<MoveableObject>();



    public void Update()
    {
        
    }






    /// <summary>
    /// ����Ʈ�� �ش� ���� ��ü �߰�
    /// </summary>
    /// <param name="addObject"></param>
    public void AddInGameMonsterList(MoveableObject addObject)
    {
        Monsters.Add(addObject);
    }

    /// <summary>
    /// ����Ʈ�� �ش� ���� ��ü ����
    /// </summary>
    /// <param name="removeObject"></param>
    public void ReMoveInGameMonsterList(MoveableObject removeObject)
    {
        Monsters.Remove(removeObject);
    }

    /// <summary>
    /// �÷��̾� �ΰ��ӸŴ��� ���
    /// </summary>
    public void PlayerRegist(MoveableObject player)
    {
        this.Player = player;
    }

    /// <summary>
    /// �ΰ��ӿ� ��ϵ�, �÷��̾�, ���͸���Ʈ�� Ŭ����
    /// </summary>
    public void ClearInGame()
    {
        Player = null;
        Monsters.Clear();
    }

}
