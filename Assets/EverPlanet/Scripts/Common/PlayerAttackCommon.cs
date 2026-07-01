using System.Collections.Generic;
using UnityEngine;

public static class PlayerAttackCommon
{
    const float PIDiv03 = Mathf.PI * 0.3f;

    /// <summary>
    /// 플레이어로부터 가장 가까운 몬스터 반환 
    /// 이때 플레이어가 바라보는 방향 고려
    /// </summary>
    public static GameObject NearMonserFromPlayer(Vector3 lookDir, Vector3 playerPos, float limitDist, float limitAngle = PIDiv03)
    {
        GameObject nearMob = null;
        float dist = float.MaxValue;
        foreach (var mob in MonsterSpawn.activeMonster)
        {
            //방향이 같은지 검사
            Vector3 diff = mob.transform.position - playerPos;
            //diff가 양수면 몬스터가 오른쪽, 음수면 몬스터가 왼쪽에 있다.
            //아래 결과가 음수면 캐릭터가 바라보는 방향에는 해당 몬스터가 없다.
            float dotValue = diff.x * lookDir.x;//내적 값
            if (dotValue < 0)
                continue;

            //거리 검사
            float curDist = diff.magnitude;
            //사거리 밖
            if (curDist > limitDist)
                continue;

            //사잇각이 너무 높으면 근처 몬스터로 보지 않는다.
            float cos = dotValue / curDist;
            float theta = Mathf.Abs(Mathf.Acos(cos));
            if (theta > limitAngle)
                continue;

            //더 가까운 거리
            if (curDist < dist)
            {
                dist = curDist;
                nearMob = mob;
            }
        }
        return nearMob;
    }
    /// <summary>
    /// 플레이어로부터 범위내에 있는 몬스터들 반환
    /// 이때 플레이어가 바라보는 방향 고려
    /// </summary>
    public static List<GameObject> TargetMonstersFromPlayer(Vector3 lookDir, Vector3 playerPos, float limitXDist, float limitYDist, float countslimit,float limitAngle = PIDiv03)
    {
        List<GameObject> mobInArea = new List<GameObject>();
        float dist = float.MaxValue;
        foreach (var mob in MonsterSpawn.activeMonster)
        {
            //방향이 같은지 검사
            Vector3 diff = mob.transform.position - playerPos;
            //diff가 양수면 몬스터가 오른쪽, 음수면 몬스터가 왼쪽에 있다.
            //아래 결과가 음수면 캐릭터가 바라보는 방향에는 해당 몬스터가 없다.
            float dotValue = diff.x * lookDir.x;//내적 값
            if (dotValue < 0)
                continue;

            //거리 검사
            float curDist = diff.magnitude;
            //X축 범위 제한
            if (curDist > limitXDist)
                continue;

            //y축 범위 제한
            if (Mathf.Abs(mob.transform.position.y - playerPos.y) > limitYDist)
                continue;

            //사잇각이 너무 높으면 근처 몬스터로 보지 않는다.
            float cos = dotValue / curDist;
            float theta = Mathf.Abs(Mathf.Acos(cos));
            if (theta > limitAngle)
                continue;

            //범위내에 몬스터가 있으므로 공격 대상으로 설정
            mobInArea.Add(mob);

            //최대 타깃수에 도달하면 반복문을 실행하지않고 빠져나간다.
            if (mobInArea.Count >= countslimit)
                break;
        }
        return mobInArea;
    }
    /// <summary>
    /// 크리티컬 판정
    /// </summary>
    /// <returns></returns>
    public static bool IsCritical()
    {
        int criValue = Random.Range(1, 101);

        if (criValue <= PlayerInfo.criticalRate)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 공격 데미지 미스 판정
    /// </summary>
    /// <returns></returns>
    public static bool IsAttackMiss(int mobLv, int mobAvoid)
    {
        int diffLv = Mathf.Max(0, mobLv - 10);
        int totalPlayerAcc = 50;

        //요구 명중치
        int requireAcc = (diffLv*mobAvoid*2)/15 + (mobAvoid*11)/3;
        
        //확정 공격
        if (totalPlayerAcc >= requireAcc)
            return false;

        //명중 확률 구하기
        int accRate = ((totalPlayerAcc * 2 - requireAcc) * 100) / totalPlayerAcc;
        int accRan = Random.Range(0, 100);
        if (accRate >= accRan)
            return false;

        //최종 미스
        return true;
    }

    /// <summary>
    /// 피격 데미지 미스 판정
    /// 플레이어의 회피율에만 영향 받음
    /// </summary>
    /// <returns></returns>
    public static bool IsHitMiss(int mobLv)
    {
        //int diffLV = Mathf.Max(0, mobLv - PlayerManager.PlayerInstance.PlayerLV);
        //int totalAvoid = PlayerManager.PlayerInstance.PlayerAvoid + PlayerManager.PlayerInstance.PlayerAddAvoid;
        int diffLV = Mathf.Max(0, mobLv - 10);
        int totalAvoid = 10;
        int avoidRate = 900+diffLV-totalAvoid/2;
        int avoidRan = Random.Range(0,1000);

        if (avoidRate <= avoidRan)
            return true;
        return false;
    }

    /// <summary>
    /// 캐릭터 -> 몬스터 공격
    /// </summary>
    public static void PlayerToMonsterAttack(Collider collision)
    {
        int maxDamage = (int)PlayerInfo.attackPower;
        int minDamage = (int)(PlayerInfo.attackPower * PlayerInfo.workmanship * 0.01);
        int damage = Random.Range(minDamage, maxDamage);

        if (PlayerAttackCommon.IsCritical())
        {
            damage = (damage * PlayerInfo.criticalDamage) / 100;
            PlayerAttackCommon.ShowCriticalDamageAsSkin(damage, collision.gameObject, 1);
        }
        else PlayerAttackCommon.ShowDamageAsSkin(damage, collision.gameObject, 1);

        //몬스터가 데미지를 입음
        collision.gameObject.GetComponent<MonsterInfo>().DecreaseMonsterHP(damage);
    }

    /// <summary>
    /// 공격 데미지 보이기
    /// </summary>
    /// <param name="Damage">데미지</param>
    /// <param name="playerPos">플레이어 위치</param>
    public static void ShowDamageAsSkin(int Damage, GameObject monsterPos, int hitNum)
    {
        string damageString = Damage.ToString();
        float damageLength = DamageObjectFulling.DamageSkinInstance.damageImage[0].bounds.size.x * damageString.Length;
        Bounds bounds = monsterPos.GetComponent<BoxCollider>().bounds;
        Vector3 damageStartPos = bounds.center + Vector3.up * (hitNum * DamageObjectFulling.DamageSkinInstance.damageImage[0].bounds.size.y+0.5f) + damageLength * Vector3.left * 0.2f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = DamageObjectFulling.DamageSkinInstance.MakeObj((damageString[i] - '0'));
            damImg.transform.position = damageStartPos + Vector3.right * DamageObjectFulling.DamageSkinInstance.damageImage[0].bounds.size.x * i * 0.5f;
        }
        InputKeyManager.orderSortNum = (InputKeyManager.orderSortNum + 1)%InputKeyManager.maxOrderSortNum;
    }
    /// <summary>
    /// 공격 미스 데미지 보이기
    /// </summary>
    /// <param name="Damage">데미지</param>
    /// <param name="playerPos">플레이어 위치</param>
    public static void ShowMissAttackDamageAsSkin(GameObject monsterPos, int hitNum)
    {
        Bounds bounds = monsterPos.GetComponent<BoxCollider>().bounds;
        Vector3 damageStartPos = bounds.center + Vector3.up * (hitNum * DamageObjectFulling.DamageSkinInstance.damageImage[0].bounds.size.y + 0.5f);

        GameObject damImg = DamageObjectFulling.DamageSkinInstance.MakeObj(30);
        damImg.transform.position = damageStartPos;
        InputKeyManager.orderSortNum = (InputKeyManager.orderSortNum + 1) % InputKeyManager.maxOrderSortNum;
    }

    /// <summary>
    /// 크리티컬 공격 데미지 보이기
    /// </summary>
    /// <param name="Damage">데미지</param>
    /// <param name="playerPos">플레이어 위치</param>
    public static void ShowCriticalDamageAsSkin(int Damage, GameObject monsterPos, int hitNum)
    {
        string damageString = Damage.ToString();
        float damageLength = DamageObjectFulling.DamageSkinInstance.criticalDamageImage[0].bounds.size.x * damageString.Length;
        Bounds bounds = monsterPos.GetComponent<BoxCollider>().bounds;
        Vector3 damageStartPos = bounds.center +Vector3.up * (hitNum * DamageObjectFulling.DamageSkinInstance.criticalDamageImage[0].bounds.size.y + 0.5f) + damageLength * Vector3.left * 0.2f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = DamageObjectFulling.DamageSkinInstance.MakeObj((damageString[i] - '0') + 10);
            damImg.transform.position = damageStartPos + Vector3.right * DamageObjectFulling.DamageSkinInstance.criticalDamageImage[0].bounds.size.x * i * 0.5f;
        }
        InputKeyManager.orderSortNum = (InputKeyManager.orderSortNum + 1) % InputKeyManager.maxOrderSortNum;
    }
    /// <summary>
    /// 피격 데미지 보이기
    /// </summary>
    /// <param name="Damage">데미지</param>
    /// <param name="playerPos">플레이어 위치</param>
    public static void ShowDamageAsSkin(long Damage, GameObject playerPos)
    {
        string damageString = Damage.ToString();
        float damageLength = DamageObjectFulling.DamageSkinInstance.hitDamageImage[0].bounds.size.x * damageString.Length;
        Bounds bounds = playerPos.GetComponent<BoxCollider2D>().bounds;
        Vector3 damageStartPos = bounds.center + Vector3.up * (bounds.size.y * 0.5f + 0.5f) + damageLength * Vector3.left * 0.25f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = DamageObjectFulling.DamageSkinInstance.MakeObj((damageString[i] - '0') + 20);
            damImg.transform.position = damageStartPos + Vector3.right * DamageObjectFulling.DamageSkinInstance.hitDamageImage[0].bounds.size.x * i * 1.5f;
        }
        InputKeyManager.orderSortNum = (InputKeyManager.orderSortNum + 1) % InputKeyManager.maxOrderSortNum;
    }

    /// <summary>
    /// 피격 미스 데미지 보이기
    /// </summary>
    /// <param name="Damage">데미지</param>
    /// <param name="playerPos">플레이어 위치</param>
    public static void ShowMissHitDamageAsSkin(GameObject playerPos)
    {
        Bounds bounds = playerPos.GetComponent<BoxCollider2D>().bounds;
        Vector3 damageStartPos = bounds.center + Vector3.up * (bounds.size.y * 0.5f + 0.5f);

        GameObject damImg = DamageObjectFulling.DamageSkinInstance.MakeObj(31);
        damImg.transform.position = damageStartPos;
        InputKeyManager.orderSortNum = (InputKeyManager.orderSortNum + 1) % InputKeyManager.maxOrderSortNum;
    }

    /// <summary>
    /// 몬스터가 캐릭터의 공격 반경 내에 있는가?
    /// AABB충돌 검출 방식 사용
    /// </summary>
    /// <returns></returns>
    public static bool IsMonsterInPlayerAttackArea(Bounds monsterArea, Bounds playerArea)
    {
        Vector3 maxMob = monsterArea.max;
        Vector3 minMob = monsterArea.min;
        Vector3 maxPlayer = playerArea.max;
        Vector3 minPlayer = playerArea.min;

        //2D이므로 x,y좌표만 비교
        bool isXCollide = false;
        bool isYCollide = false;

        //충돌 검사
        if (maxMob.x > minPlayer.x && minMob.x < maxPlayer.x)
        {
            isXCollide = true;
        }
        if (maxMob.y > minPlayer.y && minMob.y < maxPlayer.y)
        {
            isYCollide = true;
        }

        //충돌함
        if (isXCollide && isYCollide)
        {
            return true;
        }
        //충돌안함
        return false;
    }
}
