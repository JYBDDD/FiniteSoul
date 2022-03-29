using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpawn", menuName = "MonsterSpawnsPos")]
public class NewScriptableObject : ScriptableObject
{
    /// <summary>
    /// �������� �ε���
    /// </summary>
    public int stageIndex;
    /// <summary>
    /// ������ ���� �ε��� , Locations ��ġ�� ���� �ε����� �ش� ���͸� �����Ұ�
    /// </summary>
    public int[] monsterIndex;
    /// <summary>
    /// ���� ��ġ
    /// </summary>
    public Vector3[] locations;
}
