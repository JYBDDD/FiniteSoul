using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    List<GameObject> gameObjects = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        // 몬스터 풀링 부모 생성
        new GameObject { name = "Monster" }.transform.SetParent(transform);
        // Projectile 풀링 부모 생성
        new GameObject { name = "Projectile" }.transform.SetParent(transform);
    }

    /// <summary>
    /// 오브젝트 비활성화, GetPool로 생성하지 않은 상태라면 풀링폴더로 들어가지않는다.
    /// </summary>
    public void GetPush(GameObject thisObject)
    {
        thisObject.SetActive(false);
    }

    /// <summary>
    /// 오브젝트 반환
    /// </summary>
    public GameObject GetPool<T>(string resourcePath,string ObjectName,Define.CharacterType type = Define.CharacterType.None) where T : MonoBehaviour,RecyclePooling
    {
        // 반환하려고 하는 해당 오브젝트가 SetActive(false) 라면 반환,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (ObjectName == gameObjects[i].name && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                return gameObjects[i];
            }
        }

        // 아니라면 하나를 재생성한다
        GameObject returnObj = ResoureUtil.InsertPrefabs(resourcePath);

        // type이 몬스터라면, 오브젝트의 부모를 몬스터로 설정한다
        if(type == Define.CharacterType.Monster)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(0));
        }
        // type이 None 이라면, 오브젝트의 부모를 Projectile로 설정한다
        if(type == Define.CharacterType.None)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(1));
        }

        // 리스트++
        gameObjects.Add(returnObj);

        return returnObj;
    }
}
