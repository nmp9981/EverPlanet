using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHPBar : MonoBehaviour { 

    [SerializeField] List<MonsterInfo> bossList = new List<MonsterInfo>();
    [SerializeField] PlayerInfo playerInfo;

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 보스와 플레이어간 거리 계산
    /// </summary>
    void Cal_BossToPayer()
    {
        
    }
}
