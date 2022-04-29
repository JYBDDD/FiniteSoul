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

    /// <summary>
    /// �÷��̾��� �θ�
    /// </summary>
    public static GameObject ParentPlayer;

    /// <summary>
    /// ��� ���� �θ�
    /// </summary>
    public static GameObject ParentRune;

    protected override void Awake()
    {
        base.Awake();
        // ��� �θ��� ����Root�θ�� Ǯ���Ŵ����� ����

        // ���� Ǯ�� �θ�
        ParentMonster = new GameObject { name = "Monster" };
        ParentMonster.transform.SetParent(transform); 

        // Projectile Ǯ�� �θ�
        ParentProjectile = new GameObject { name = "Projectile" };
        ParentProjectile.transform.SetParent(transform); 

        // ��ƼŬ Ǯ�� �θ�
        ParentParticle = new GameObject { name = "Particle" };
        ParentParticle.transform.SetParent(transform); 

        // �÷��̾� Ǯ�� �θ�
        ParentPlayer = new GameObject { name = "Player" };
        ParentPlayer.transform.SetParent(transform);

        // ��� ���� �θ�
        ParentRune = new GameObject { name = "Rune" };
        ParentRune.transform.SetParent(transform);
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
    public GameObject GetPool<T>(string resourcePath,Vector3 postion,Define.CharacterType type = Define.CharacterType.None) where T : MonoBehaviour,RecyclePooling
    {
        // �ش� ������Ʈ�� �̸�
        string ObjectName = Resources.Load(resourcePath).name;

        // ��ȯ�Ϸ��� �ϴ� �ش� ������Ʈ�� SetActive(false) ��� ��ȯ,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (ObjectName + "(Clone)" == gameObjects[i].name && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                gameObjects[i].transform.position = postion;
                return gameObjects[i];
            }
        }

        // �ƴ϶�� �ϳ��� ������Ѵ�
        GameObject returnObj = ResourceUtil.InsertPrefabs(resourcePath);
        returnObj.transform.position = postion;

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
        if(type == Define.CharacterType.Player)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(3));
        }
        if(type == Define.CharacterType.Rune)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(4));
        }

        // ����Ʈ++
        gameObjects.Add(returnObj);

        return returnObj;
    }
}
