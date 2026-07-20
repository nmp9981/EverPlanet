using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour {

    [SerializeField] GameObject bossHPObj;
    [SerializeField] PlayerInfo playerInfo;

    [Header("보스 정보")]
    [SerializeField] TextMeshProUGUI bossTextUI;
    [SerializeField] Image bossHpBar;
    [SerializeField] Image bossImage;

    float viewLimitDist2 = 100;
    float curTime=0.1f;
    float inspectTime = 0.15f;

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime>inspectTime)
        {
            curTime = 0;
            MonsterInfo mob = Cal_BossToPayer();
            ShowBossHP(mob);
        }
    }

    /// <summary>
    /// 보스와 플레이어간 거리 계산
    /// </summary>
    MonsterInfo Cal_BossToPayer()
    {
        float closestDist = float.MaxValue;
        MonsterInfo closetMob = null;
        foreach (var mob in MonsterSpawn.activeBossMonster)
        {
            Vector3 mobToPlayer = mob.gameObject.transform.position-playerInfo.gameObject.transform.position;
            float dist2 = mobToPlayer.x * mobToPlayer.x + mobToPlayer.z * mobToPlayer.z;

            if (dist2 < viewLimitDist2 && dist2<closestDist)
            {
                closestDist = dist2;
                closetMob = mob;
            }
        }
        return closetMob;
    }
    /// <summary>
    /// 보스몬스터 HP 보이기
    /// </summary>
    void ShowBossHP(MonsterInfo mobInfo)
    {
        if(mobInfo == null || mobInfo.mobCurrentHP<=0)
        {
            bossHPObj.SetActive(false);
            return;
        }
        bossHPObj.SetActive(true);
        bossTextUI.text = $"{mobInfo.mobCurrentHP} / {mobInfo.mobMaxHP}";
        bossHpBar.fillAmount = (float)mobInfo.mobCurrentHP / (float)mobInfo.mobMaxHP;
    }
}
