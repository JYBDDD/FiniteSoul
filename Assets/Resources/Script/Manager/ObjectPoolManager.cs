using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    List<GameObject> gameObjects = new List<GameObject>();

    /// <summary>
    /// Ǯ���Ŵ����� ���� �θ�
    /// </summary>
    public static GameObject ParentMonster;

    /// <summary>
    /// Ǯ���Ŵ����� �߻�ü �θ�
    /// </summary>
    public static GameObject ParentProjectile;

    protected override void Awake()
    {
        base.Awake();

        // ���� Ǯ�� �θ� ����
        ParentMonster = new GameObject { name = "Monster" };
        ParentMonster.transform.SetParent(transform);   // ���ͺθ��� ����Root�θ�� Ǯ���Ŵ����� ����

        // Projectile Ǯ�� �θ� ����
        ParentProjectile = new GameObject { name = "Projectile" };
        ParentProjectile.transform.SetParent(transform);    // �߻�ü�θ��� ����Root�θ�� Ǯ���Ŵ����� ����
    }

    /// <summary>
    /// ������Ʈ ��Ȱ��ȭ, GetPool�� �������� ���� ���¶�� Ǯ�������� �����ʴ´�.
    /// </summary>
    /// <param name="thisObject">���Ͻ�ų ������Ʈ</param>
    /// <param name="trans">�ش� ������Ʈ�� �θ� ��ȯ�� �ʿ��ҽ� ��밡��</param>
    public void GetPush(GameObject thisObject,Transform trans = null)
    {
        thisObject.SetActive(false);

        // �θ� ��ȯ�� �ʿ��� ��� ����
        if(trans != null)
        {
            // �θ� �缳��
            thisObject.transform.SetParent(trans);
        }

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
        GameObject returnObj = ResourceUtil.InsertPrefabs(resourcePath);

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
