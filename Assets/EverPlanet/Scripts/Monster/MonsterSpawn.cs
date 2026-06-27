using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    //몬스터 풀링
    [SerializeField] MonsterFulling monsterFulling;
    //맵에 활성화된 몬스터
    public static List<GameObject> activeMonster = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFlow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 몬스터 스폰 과정
    /// </summary>
    void SpawnFlow()
    {
        for(int i = 0; i < 20; i++)
        {
            float xRan = Random.Range(-10, 10);
            float zRan = Random.Range(-10, 10);
            GameObject mob = monsterFulling.MakeObj(0);
            mob.transform.position = new Vector3(xRan, 0f, zRan);
        }
    }
}
