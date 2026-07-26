using UnityEngine;

public static class MonsterAttackCommon
{
    #region 이펙트
    public static void OnRangeEffect(GameObject effect)
    {
        effect.SetActive(true);
    }
    public static void OffRangeEffect(GameObject effect)
    {
        effect.SetActive(false);
    }
    #endregion

    #region 플레이어 감지
    public static bool IsPlayerInArea(Vector3 playerPos, Vector3 monsterPos,float range)
    {
        float dist = (playerPos - monsterPos).magnitude;
        return (dist<range)?true:false;
    }
    #endregion
}
