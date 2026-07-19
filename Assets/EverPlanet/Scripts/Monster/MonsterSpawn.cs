using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Array moveTypeArray = Enum.GetValues(typeof(MonsterMoveType));
        foreach (var trans in spawnPosList)
        {
            int ranCount = UnityEngine.Random.Range(4, 7);
            for (int i = 0; i < ranCount; i++)
            {
                float xRan = UnityEngine.Random.Range(-3, 3);
                float zRan = UnityEngine.Random.Range(-3, 3);
                int moveRanNum = UnityEngine.Random.Range(0,(int)MonsterMoveType.Count);
                GameObject mob = monsterFulling.MakeObj(0);
                MonsterMove mobMove = mob.GetComponent<MonsterMove>();
                mobMove.SetDiameter();

                mobMove.moveType = (MonsterMoveType)moveTypeArray.GetValue(moveRanNum);
                mob.transform.position = trans.position + new Vector3(xRan, 0f, zRan);
                activeMonster.Add(mob);
            }
        }
        //보스
        GameObject bossMob = monsterFulling.MakeObj(2);
        MonsterMove bossMobMove = bossMob.GetComponent<MonsterMove>();
        bossMobMove.moveType = (MonsterMoveType)moveTypeArray.GetValue(0);
        bossMob.transform.position = spawnPosList[spawnPosList.Count-1].position;
    }
}
