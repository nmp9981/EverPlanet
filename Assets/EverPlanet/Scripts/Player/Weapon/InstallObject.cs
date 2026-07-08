using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InstallObject : MonoBehaviour
{
    private float timer;
    private float curTimer=0;
    private float maxTimer = 10;
    private int counter;
    public float waitTime = 0.3f;

    public int maxLimitXDist = 5;
    public int maxLimitYDist = 8;
    public int maxTargetCount = 100;

    private void OnEnable()
    {
        timer = 0;
        counter = 0;
        curTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //시간 경과
        if(curTimer>=maxTimer) gameObject.SetActive(false);

        //다시 공격 가능하게
        if (timer > waitTime)
        {
            AttackDoteDamage();
        }
        curTimer += Time.deltaTime;
        timer += Time.deltaTime;
    }

    /// <summary>
    /// 도드데미지 입히기
    /// </summary>
    void AttackDoteDamage()
    {
        List<GameObject> mobs = PlayerAttackCommon.TargetMonstersInRange(this.gameObject.transform.position, maxLimitXDist, maxLimitYDist, maxTargetCount);
        foreach (GameObject monster in mobs)
        {
            var mobCollide = monster.GetComponent<Collider>();
            PlayerAttackCommon.PlayerToMonsterAttack(mobCollide, 200, (counter % 8) + 2);
            counter += 1;
            timer = 0;
        }
    }
}
