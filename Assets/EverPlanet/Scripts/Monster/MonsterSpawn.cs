using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    //몬스터 풀링
    [SerializeField] MonsterFulling monsterFulling;
    //스폰 지점
    [SerializeField] List<Transform> spawnPosList = new List<Transform>();
    //맵에 활성화된 몬스터
    public static List<GameObject> activeMonster = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFlow();
    }

    /// <summary>
    /// 몬스터 스폰 과정
    /// </summary>
    void SpawnFlow()
    {
        foreach (var trans in spawnPosList)
        {
            int ranCount = Random.Range(4, 7);
            for (int i = 0; i < ranCount; i++)
            {
                float xRan = Random.Range(-3, 3);
                float zRan = Random.Range(-3, 3);
                GameObject mob = monsterFulling.MakeObj(0);
                mob.transform.position = trans.position + new Vector3(xRan, 0f, zRan);
                activeMonster.Add(mob);
            }
        }
    }
}
