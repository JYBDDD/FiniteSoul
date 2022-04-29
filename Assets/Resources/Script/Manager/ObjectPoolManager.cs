using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    List<GameObject> gameObjects = new List<GameObject>();

    /// <summary>
    /// 풀링매니저의 몬스터 부모
    /// </summary>
    public static GameObject ParentMonster;

    /// <summary>
    /// 풀링매니저의 발사체 부모
    /// </summary>
    public static GameObject ParentProjectile;

    /// <summary>
    /// 파티클(이펙트)의 부모
    /// </summary>
    public static GameObject ParentParticle;

    /// <summary>
    /// 플레이어의 부모
    /// </summary>
    public static GameObject ParentPlayer;

    /// <summary>
    /// 드랍 룬의 부모
    /// </summary>
    public static GameObject ParentRune;

    protected override void Awake()
    {
        base.Awake();
        // 모든 부모의 최종Root부모는 풀링매니저로 설정

        // 몬스터 풀링 부모
        ParentMonster = new GameObject { name = "Monster" };
        ParentMonster.transform.SetParent(transform); 

        // Projectile 풀링 부모
        ParentProjectile = new GameObject { name = "Projectile" };
        ParentProjectile.transform.SetParent(transform); 

        // 파티클 풀링 부모
        ParentParticle = new GameObject { name = "Particle" };
        ParentParticle.transform.SetParent(transform); 

        // 플레이어 풀링 부모
        ParentPlayer = new GameObject { name = "Player" };
        ParentPlayer.transform.SetParent(transform);

        // 드랍 룬의 부모
        ParentRune = new GameObject { name = "Rune" };
        ParentRune.transform.SetParent(transform);
    }

    /// <summary>
    /// 오브젝트 비활성화, GetPool로 생성하지 않은 상태라면 풀링폴더로 들어가지않는다.
    /// </summary>
    /// <param name="thisObject">리턴시킬 오브젝트</param>
    /// <param name="trans">해당 오브젝트를 부모에 반환이 필요할시 사용가능</param>
    public void GetPush(GameObject thisObject,Transform trans = null)
    {
        thisObject.SetActive(false);

        // 부모 반환이 필요한 경우 실행
        if(trans != null)
        {
            // 부모 재설정
            thisObject.transform.SetParent(trans);
        }

    }

    /// <summary>
    /// 오브젝트 반환
    /// </summary>
    public GameObject GetPool<T>(string resourcePath,Vector3 postion,Define.CharacterType type = Define.CharacterType.None) where T : MonoBehaviour,RecyclePooling
    {
        // 해당 오브젝트의 이름
        string ObjectName = Resources.Load(resourcePath).name;

        // 반환하려고 하는 해당 오브젝트가 SetActive(false) 라면 반환,
        for(int i = 0; i < gameObjects.Count; ++i)
        {
            if (ObjectName + "(Clone)" == gameObjects[i].name && !gameObjects[i].activeSelf)
            {
                gameObjects[i].SetActive(true);
                gameObjects[i].transform.position = postion;
                return gameObjects[i];
            }
        }

        // 아니라면 하나를 재생성한다
        GameObject returnObj = ResourceUtil.InsertPrefabs(resourcePath);
        returnObj.transform.position = postion;

        // type이 몬스터라면, 오브젝트의 부모를 몬스터로 설정
        if(type == Define.CharacterType.Monster)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(0));
        }
        // type이 발사체 라면, 오브젝트의 부모를 Projectile로 설정
        if(type == Define.CharacterType.Projectile)
        {
            returnObj.transform.SetParent(gameObject.transform.GetChild(1));
        }
        // type이 파티클 이라면, 오브젝트의 부모를 Particle로 설정
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

        // 리스트++
        gameObjects.Add(returnObj);

        return returnObj;
    }
}
