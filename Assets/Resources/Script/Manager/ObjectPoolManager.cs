using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    List<GameObject> gameObjects = new List<GameObject>();

    /// <summary>
    /// 오브젝트 비활성화
    /// </summary>
    public void GetObj(GameObject thisObject)
    {
        thisObject.SetActive(false);
        thisObject.transform.SetParent(transform); 
    }

    /// <summary>
    /// 오브젝트 반환
    /// </summary>
    public void Return(GameObject gameObject)
    {
        // 반환하려고 하는 해당 오브젝트가 SetActive(false) 라면 반환,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (gameObject == gameObjects[i] && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                return;
            }
        }

        // 아니라면 하나를 재생성한다
        Instantiate(gameObject);
        gameObjects.Add(gameObject);
        // 리스트++

    }
}
