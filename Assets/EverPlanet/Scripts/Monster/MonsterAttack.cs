using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    MonsterInfo monsterInfo;

    [SerializeField] List<GameObject> attackObj = new List<GameObject>();//공격 투사체
    [SerializeField] GameObject target;//타겟 범위
    [SerializeField] float attackRange;//사거리 범위
    [SerializeField] float wideAttackArea;//광역 스킬 공격 범위
    [SerializeField] EffectFulling effectFulling;

    

    private Bounds monsterBound;
    private bool firstAttack;

    private void OnEnable()
    {
        monsterInfo = this.gameObject.GetComponent<MonsterInfo>();
        effectFulling = GameObject.Find("ObjectFulling").GetComponent<EffectFulling>();
    }

    private void Start()
    {
        target = GameObject.Find("Player");
        monsterBound = this.gameObject.GetComponent<Collider>().bounds;
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
                int attackNum = Random.Range(0, 3);

                switch (attackNum)
                {
                    case 0:
                        ThrowStone();
                        break;
                    case 1:
                        LayserAttack();
                        break;
                    case 2:
                        var effectList = ShowWideTargetEffect();
                        yield return new WaitForSeconds(3f);//3초뒤 공격
                        RiseSpikes(effectList);
                        effectList.Clear();
                        break;
                    default:
                        break;
                }
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
        //여기서 공격 이펙트
        GameObject effectObj = Instantiate(attackObj[1], transform.position + Vector3.up, Quaternion.identity);
        effectObj.GetComponent<LayserStraitMonster>().SetMagicDamage(monsterInfo.mobAttack);
        ConnectTwoPoints(this.gameObject, target, effectObj, 0.5f);

    }

    /// <summary>
    /// 가시 돋기
    /// </summary>
    private void RiseSpikes(List<Vector3> targetPosList)
    {
        //공격 범위 이펙트 띄우기
        for (int i=0;i<targetPosList.Count; i++)
        {
            GameObject needleObject = Instantiate(attackObj[2], targetPosList[i], Quaternion.identity);
            needleObject.GetComponent<NeedleMonster>().SetMagicDamage(monsterInfo.mobAttack);
        }
    }

    /// <summary>
    /// 두 점을 연결하는 선분 생성
    /// size는 선분의 굵기 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <param name="pipeGM"></param>
    /// <param name="size"></param>
    public void ConnectTwoPoints(GameObject point1, GameObject point2,GameObject pipeGM, float size)
    {
        Vector3 p1 = point1.transform.position;
        Vector3 p2 = point2.transform.position;

        Vector3 dir = p2 - p1;
        float distance = dir.magnitude;

        if (distance <= Mathf.Epsilon) return; // 두 점이 같은 위치에 있을 때 예외 처리

        // 1. 위치 설정: 두 점의 중점
        pipeGM.transform.position = (p1 + p2) * 0.5f;

        // 2. 회전 설정: dir 방향을 바라보도록 설정
        // 기본 Unity Cylinder는 Y축이 길쭉한 방향이므로, 
        // dir 방향을 Y축(Up)으로 삼도록 LookRotation을 설정합니다.
        pipeGM.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90f, 0f, 0f);

        // 3. 스케일 설정 (Unity 기본 Cylinder 기준 높이가 2이므로 distance * 0.5f)
        pipeGM.transform.localScale = new Vector3(size, distance * 0.5f, size);
    }

    /// <summary>
    /// 광범위 이펙트 띄우기
    /// </summary>
    List<Vector3> ShowWideTargetEffect()
    {
        //공격 위치 저장
        List<Vector3> targetPosList = new List<Vector3>();
        for (int i = 0; i < 8; i++)
        {
            float theta = 45 * i;
            float xSize = Random.Range(3, wideAttackArea);
            float zSize = Random.Range(3, wideAttackArea);
            Vector3 center = transform.position - Vector3.down* monsterBound.size.y + new Vector3(xSize * Mathf.Cos(theta), 0, zSize * Mathf.Sin(theta));
            targetPosList.Add(center);
        }

        //공격 범위 이펙트 띄우기
        foreach (Vector3 pos in targetPosList)
        {
            GameObject effectObj = effectFulling.MakeObj(0);
            effectObj.transform.position = pos;
        }
        return targetPosList;
    }
}
