using System.Collections;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    MonsterInfo monsterInfo;

    [SerializeField] GameObject attackObj;//공격 투사체
    [SerializeField] GameObject target;//타겟 범위
    [SerializeField] float attackRange;//사거리 범위

    private bool firstAttack;

    private void OnEnable()
    {
        monsterInfo = this.gameObject.GetComponent<MonsterInfo>();
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
                //투사체 발사
                Vector3 spawnPosition = transform.position + Vector3.up*1.5f;
                GameObject projectileObject = Instantiate(attackObj, spawnPosition, Quaternion.identity);

                Vector3 dir = target.transform.position - this.gameObject.transform.position;
                Vector3 diry0 = new Vector3(dir.x, 4, dir.z);
                projectileObject.GetComponent<ProjectileMonster>().SetMagicDamage(monsterInfo.mobAttack);
                projectileObject.GetComponent<Rigidbody>().AddForce(diry0, ForceMode.Impulse);
            }
            
        }
    }
}
