using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// �Ѱ� ������
    /// </summary>
    public static FullDataCollection FullData { get => Instance.fullData; set => value = Instance.fullData; }
    private FullDataCollection fullData = new FullDataCollection();


    // ������ FullDataCollection ���Ժ� ���⿡ ���� ���� TODO
}
