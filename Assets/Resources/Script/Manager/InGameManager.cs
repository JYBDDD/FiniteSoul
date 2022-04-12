using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ΰ��ӿ� ���õ� �����͸� ������� �Ŵ��� Ŭ����
/// </summary>
public class InGameManager : Singleton<InGameManager>
{
    // �÷��̾�
    public PlayerController Player = null;
    // ���� ����Ʈ
    public List<MonsterController> Monsters = new List<MonsterController>();



    public void Update()
    {
        
    }






    /// <summary>
    /// ����Ʈ�� �ش� ���� ��ü �߰�
    /// </summary>
    /// <param name="addObject"></param>
    public void MonsterRegist(MonsterController addObject)
    {
        Monsters.Add(addObject);
    }

    /// <summary>
    /// ����Ʈ�� �ش� ���� ��ü ����
    /// </summary>
    /// <param name="removeObject"></param>
    public void MonsterRegistRemove(MonsterController removeObject)
    {
        Monsters.Remove(removeObject);
    }

    /// <summary>
    /// �÷��̾� �ΰ��ӸŴ��� ���
    /// </summary>
    public void PlayerRegist(PlayerController player)
    {
        this.Player = player;
    }

    /// <summary>
    /// �ΰ��ӿ� ��ϵ�, �÷��̾�, ���� ����Ʈ�� Ŭ����
    /// </summary>
    public void ClearInGame()
    {
        Player = null;
        Monsters.Clear();
    }

}
