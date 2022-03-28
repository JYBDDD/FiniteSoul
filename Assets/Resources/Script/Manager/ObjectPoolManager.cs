using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    List<GameObject> gameObjects = new List<GameObject>();

    /// <summary>
    /// ������Ʈ ��Ȱ��ȭ
    /// </summary>
    public void GetObj(GameObject thisObject)
    {
        thisObject.SetActive(false);
        thisObject.transform.SetParent(transform); 
    }

    /// <summary>
    /// ������Ʈ ��ȯ
    /// </summary>
    public void Return(GameObject gameObject)
    {
        // ��ȯ�Ϸ��� �ϴ� �ش� ������Ʈ�� SetActive(false) ��� ��ȯ,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (gameObject == gameObjects[i] && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                return;
            }
        }

        // �ƴ϶�� �ϳ��� ������Ѵ�
        Instantiate(gameObject);
        gameObjects.Add(gameObject);
        // ����Ʈ++

    }
}
