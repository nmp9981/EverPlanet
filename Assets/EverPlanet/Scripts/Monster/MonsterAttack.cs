using System.Collections;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    MonsterInfo monsterInfo;

    [SerializeField] GameObject attackObj;//공격 투사체
    [SerializeField] GameObject attackRange;//공격 범위

    private void OnEnable()
    {
        monsterInfo = this.gameObject.GetComponent<MonsterInfo>();
    }

    private void Start()
    {
        StartCoroutine(MonsterToPlayerAttack());
    }

    /// <summary>
    /// 몬스터 -> 플레이어 공격
    /// </summary>
    IEnumerator MonsterToPlayerAttack()
    {
        //공격 범위 그리기


        //투사체 발사

    }
}
