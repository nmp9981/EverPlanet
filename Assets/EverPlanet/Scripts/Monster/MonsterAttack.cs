using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    MonsterInfo monsterInfo;

    [SerializeField] List<GameObject> attackObj = new List<GameObject>();//공격 투사체
    [SerializeField] GameObject target;//타겟 범위
    [SerializeField] float attackRange;//사거리 범위
    [SerializeField] EffectFulling effectFulling;

    private bool firstAttack;

    private void OnEnable()
    {
        monsterInfo = this.gameObject.GetComponent<MonsterInfo>();
        effectFulling = GameObject.Find("ObjectFulling").GetComponent<EffectFulling>();
    }

    private void Start()
    {
        target = GameObject.Find("Player");
        firstAttack = true;
    }

    /// <summary>
    /// 몬스터 -> 플레이어 공격 코루틴 시작
    /// </summary>
    public void StartMonsterAttackCoroutain()
    {
        if (firstAttack)
        {
            StartCoroutine(MonsterToPlayerAttack());
            firstAttack = false;
        }
    }

    /// <summary>
    /// 몬스터 -> 플레이어 공격
    /// </summary>
    IEnumerator MonsterToPlayerAttack()
    {
        while (true)
        {
            //3초뒤
            yield return new WaitForSeconds(3f);

            //어그로 여부
            if (!monsterInfo.isAggro) continue;

            //플레이어 감지
            if (MonsterAttackCommon.IsPlayerInArea(target.transform.position, this.gameObject.transform.position, attackRange)){
                LayserAttack();
            }
            
        }
    }

    /// <summary>
    /// 돌 던지기
    /// </summary>
    private void ThrowStone()
    {
        //여기서 공격 범위 이펙트
        GameObject effectObj = effectFulling.MakeObj(0);
        effectObj.transform.position = target.transform.position;

        //투사체 발사
        Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
        GameObject projectileObject = Instantiate(attackObj[0], spawnPosition, Quaternion.identity);

        Vector3 dir = target.transform.position - this.gameObject.transform.position;
        Vector3 diry0 = new Vector3(dir.x, 4, dir.z);
        projectileObject.GetComponent<ProjectileMonster>().SetMagicDamage(monsterInfo.mobAttack);
        projectileObject.GetComponent<Rigidbody>().AddForce(diry0, ForceMode.Impulse);
    }

    /// <summary>
    /// 레이저 공격
    /// </summary>
    private void LayserAttack()
    {
        //투사체 발사
        GameObject layserObject = Instantiate(attackObj[1], transform.position+Vector3.up, Quaternion.identity);

        //해당 방향으로 발사
        Vector3 diff = target.transform.position - this.gameObject.transform.position;
        Vector3 dir = diff.normalized;
        Vector3 dirY0 = new Vector3(dir.x, 0, dir.z);
        layserObject.GetComponent<LayserStraitMonster>().SetMagicDamage(monsterInfo.mobAttack);
        layserObject.GetComponent<Rigidbody>().AddForce(5*dir, ForceMode.Impulse);

        //여기서 공격 이펙트
        GameObject effectObj = effectFulling.MakeObj(1);
        Vector3 avgPos = (this.gameObject.transform.position + target.transform.position) / 2;
        effectObj.transform.position = avgPos;
        effectObj.transform.LookAt(dir);
    }
}
