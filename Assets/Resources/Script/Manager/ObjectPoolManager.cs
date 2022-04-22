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

    /// <summary>
    /// ��ƼŬ(����Ʈ)�� �θ�
    /// </summary>
    public static GameObject ParentParticle;

    protected override void Awake()
    {
        base.Awake();

        // ���� Ǯ�� �θ� ����
        ParentMonster = new GameObject { name = "Monster" };
        ParentMonster.transform.SetParent(transform);   // ���ͺθ��� ����Root�θ�� Ǯ���Ŵ����� ����

        // Projectile Ǯ�� �θ� ����
        ParentProjectile = new GameObject { name = "Projectile" };
        ParentProjectile.transform.SetParent(transform);    // �߻�ü�θ��� ����Root�θ�� Ǯ���Ŵ����� ����

        ParentParticle = new GameObject { name = "Particle" };
        ParentParticle.transform.SetParent(transform);  // ��ƼŬ�θ��� ����Root�θ�� Ǯ���Ŵ����� ����
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
    public GameObject GetPool<T>(string resourcePath,Define.CharacterType type = Define.CharacterType.None) where T : MonoBehaviour,RecyclePooling
    {
        // �ش� ������Ʈ�� �̸�
        string ObjectName = Resources.Load(resourcePath).name;

        // ��ȯ�Ϸ��� �ϴ� �ش� ������Ʈ�� SetActive(false) ��� ��ȯ,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (ObjectName + "(Clone)" == gameObjects[i].name && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                return gameObjects[i];
            }
        }

        // �ƴ϶�� �ϳ��� ������Ѵ�
        GameObject returnObj = ResourceUtil.InsertPrefabs(resourcePath);

        // type�� ���Ͷ��, ������Ʈ�� �θ� ���ͷ� ����
        if(type == Define.CharacterType.Monster)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(0));
        }
        // type�� �߻�ü ���, ������Ʈ�� �θ� Projectile�� ����
        if(type == Define.CharacterType.Projectile)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(1));
        }
        // type�� ��ƼŬ �̶��, ������Ʈ�� �θ� Particle�� ����
        if(type == Define.CharacterType.Particle)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(2));
        }

        // ����Ʈ++
        gameObjects.Add(returnObj);

        return returnObj;
    }
}
