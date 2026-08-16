using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
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
    [SerializeField] ModeManager modeManager;
    public List<Portal> portalList = new();
    public MapType mapType;
    public static bool isKillBoss;

    private void Awake()
    {
        isKillBoss = false;
        mapType = MapType.None;
    }

    private void Start()
    {
        StartCoroutine(InspectMap());
        StartCoroutine(CheckBossMap());
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
                monsterSpawn.SpawnFlowInDeongeon();
                yield return null;
            }
            else if (mapType == MapType.TheRoom)
            {
                //더 룸 맵에 입장했을 때
                monsterSpawn.SpawnInTheRoom();
            }
            yield return new WaitForSeconds(11f);
        }
    }

    /// <summary>
    /// 보스 맵 체크
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckBossMap()
    {
        while (true)
        {
            BossKillInMap();
            yield return new WaitForSeconds(5f);
        }
    }

    /// <summary>
    /// 모드전환 연결
    /// </summary>
    public void BridgeModeManager(MapType mapType)
    {
        if(mapType == MapType.Deongeon)
        {
            OffPortal(portalList[0]);
        }
        else if (mapType == MapType.TheRoom)
        {
            OffPortal(portalList[5]);
            modeManager.ShowTimeUI();
        }
    }

    /// <summary>
    /// 포탈 Off
    /// </summary>
    public void OffPortal(Portal portal)
    {
        portal.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 포탈 On
    /// </summary>
    /// <param name="portal"></param>
    public void OnPortal(Portal portal)
    {
        portal.transform.parent.gameObject.SetActive(true);
    }

    /// <summary>
    /// 맵에 보스가 죽었는가?
    /// </summary>
    void BossKillInMap()
    {
        if (isKillBoss)
        {
            OnPortal(portalList[0]);
            isKillBoss = false;
        }
    }
}
