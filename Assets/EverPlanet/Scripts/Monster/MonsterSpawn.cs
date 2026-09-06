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
    [SerializeField] List<Transform> spawnPosListInTheRoom = new List<Transform>();
    //맵에 활성화된 몬스터
    public static List<GameObject> activeMonster = new List<GameObject>();
    //맵에 활성화된 보스 몬스터
    public static List<MonsterInfo> activeBossMonster = new List<MonsterInfo>();

   
    /// <summary>
    /// 몬스터 스폰 과정
    /// </summary>
    public void SpawnFlowInDeongeon()
    {
        Array moveTypeArray = Enum.GetValues(typeof(MonsterMoveType));
        foreach (var trans in spawnPosList)
        {
            int ranCount = UnityEngine.Random.Range(4, 7);
            int mobRannum = UnityEngine.Random.Range(0,2);
            for (int i = 0; i < ranCount; i++)
            {
                float xRan = UnityEngine.Random.Range(-3, 3);
                float zRan = UnityEngine.Random.Range(-3, 3);
                int moveRanNum = UnityEngine.Random.Range(0,(int)MonsterMoveType.Count);
                
                GameObject mob = monsterFulling.MakeObj(mobRannum);
                if (mob != null)
                {
                    MonsterMove mobMove = mob.GetComponent<MonsterMove>();

                    mobMove.chaseRange = 10f;
                    mobMove.isAggro = false;
                    mobMove.SetDiameter();
                    mobMove.InitHP_UISet();

                    mobMove.moveType = (MonsterMoveType)moveTypeArray.GetValue(moveRanNum);
                    mob.transform.position = trans.position + new Vector3(xRan, 0f, zRan);
                    activeMonster.Add(mob);
                }
            }
        }
        //보스
        if(activeBossMonster.Count > 0) return;

        GameObject bossMob = monsterFulling.MakeObj(2);
        MonsterMove bossMobMove = bossMob.GetComponent<MonsterMove>();
        bossMobMove.moveType = (MonsterMoveType)moveTypeArray.GetValue(0);
        bossMob.transform.position = spawnPosList[spawnPosList.Count-1].position;
        activeMonster.Add(bossMob);
        activeBossMonster.Add(bossMob.GetComponent<MonsterInfo>());
    }

    /// <summary>
    /// TheRoom맵 몬스터 스폰
    /// </summary>
    public void SpawnInTheRoom()
    {
        //최대 몬스터 수 제한
        if(activeMonster.Count > 180) return;

        Array moveTypeArray = Enum.GetValues(typeof(MonsterMoveType));
        foreach (var trans in spawnPosListInTheRoom)
        {
            int ranCount = UnityEngine.Random.Range(5, 9);
            int mobRannum = UnityEngine.Random.Range(0, 2);
            for (int i = 0; i < ranCount; i++)
            {
                float xRan = UnityEngine.Random.Range(-3, 3);
                float zRan = UnityEngine.Random.Range(-3, 3);
                int moveRanNum = UnityEngine.Random.Range(0, (int)MonsterMoveType.Count);

                GameObject mob = monsterFulling.MakeObj(mobRannum);
                if (mob != null)
                {
                    MonsterMove mobMove = mob.GetComponent<MonsterMove>();
                    SetMonsterSpec(mobMove);
                    mobMove.InitHP_UISet();

                    mobMove.moveType = (MonsterMoveType)moveTypeArray.GetValue(moveRanNum);
                    mob.transform.position = trans.position + new Vector3(xRan, 0f, zRan);
                    activeMonster.Add(mob);
                }
            }
        }
    }
    /// <summary>
    /// 몬스터 리젠 초기화
    /// </summary>
    public void ClearActiveMonster()
    {
        foreach (var mob in activeMonster)
        {
            if (mob != null)
            {
                mob.gameObject.SetActive(false);
            }
        }
        foreach (var mob in activeBossMonster)
        {
            if (mob != null)
            {
                mob.gameObject.SetActive(false);
            }
        }
        activeMonster.Clear();
        activeBossMonster.Clear();
    }

    /// <summary>
    /// 몬스터 스펙 설정
    /// </summary>
    void SetMonsterSpec(MonsterMove mobMove)
    {
        mobMove.mobLv = PlayerInfo.playerLv;
        mobMove.mobMaxHP = mobMove.mobLv * mobMove.mobLv * 25;
        mobMove.mobExp = mobMove.mobLv*31+5*(mobMove.mobLv/4);
        mobMove.mobAttack = mobMove.mobLv* (mobMove.mobLv/5)+2*mobMove.mobLv-50;

        mobMove.SetDiameter();
        mobMove.isAggro = true;
        mobMove.chaseRange = 100f;
    }
}
