using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    None,
    Deongeon,
    TheRoom,
    Count
}

public class MapManage : MonoBehaviour
{
    [SerializeField] MonsterSpawn monsterSpawn;
    public List<Portal> portalList = new();
    public MapType mapType;

    private void Awake()
    {
        mapType = MapType.None;
    }

    private void Start()
    {
        StartCoroutine(InspectMap());
    }

    /// <summary>
    /// 맵 검사, 5초마다 반복
    /// </summary>
    /// <returns></returns>
    IEnumerator InspectMap()
    {
        while (true)
        {
            if(mapType == MapType.Deongeon)
            {
                //던전 맵에 입장했을 때
            }
            else if (mapType == MapType.TheRoom)
            {
                //더 룸 맵에 입장했을 때
                monsterSpawn.SpawnInTheRoom();
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
