using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    List<GameObject> gameObjects = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        // ���� Ǯ�� �θ� ����
        new GameObject { name = "Monster" }.transform.SetParent(transform);
        // Projectile Ǯ�� �θ� ����
        new GameObject { name = "Projectile" }.transform.SetParent(transform);
    }

    /// <summary>
    /// ������Ʈ ��Ȱ��ȭ, GetPool�� �������� ���� ���¶�� Ǯ�������� �����ʴ´�.
    /// </summary>
    public void GetPush(GameObject thisObject)
    {
        thisObject.SetActive(false);
    }

    /// <summary>
    /// ������Ʈ ��ȯ
    /// </summary>
    public GameObject GetPool<T>(string resourcePath,string ObjectName,Define.CharacterType type = Define.CharacterType.None) where T : MonoBehaviour,RecyclePooling
    {
        // ��ȯ�Ϸ��� �ϴ� �ش� ������Ʈ�� SetActive(false) ��� ��ȯ,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (ObjectName == gameObjects[i].name && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                return gameObjects[i];
            }
        }

        // �ƴ϶�� �ϳ��� ������Ѵ�
        GameObject returnObj = ResoureUtil.InsertPrefabs(resourcePath);

        // type�� ���Ͷ��, ������Ʈ�� �θ� ���ͷ� �����Ѵ�
        if(type == Define.CharacterType.Monster)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(0));
        }
        // type�� None �̶��, ������Ʈ�� �θ� Projectile�� �����Ѵ�
        if(type == Define.CharacterType.None)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(1));
        }

        // ����Ʈ++
        gameObjects.Add(returnObj);

        return returnObj;
    }
}
