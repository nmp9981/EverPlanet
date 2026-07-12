using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] ObjectFulling objFulling;
    [SerializeField] Transform playerDirObjectTransform;

    [Header("이펙트")]
    [SerializeField] GameObject fireDemonEffect;
    [SerializeField] GameObject cloudEffect;
    [SerializeField] GameObject pushEffect;

    [Header("스킬")]
    [SerializeField] GameObject knifeStormObj;

    /// <summary>
    /// 일반 공격
    /// </summary>
    public void GeneralAttack()
    {
        GameObject projecTile = objFulling.MakeObj(0);
        Projectile proj = projecTile.GetComponent<Projectile>();
        projecTile.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        proj.hitNum = 2;
        proj.skillDamage = 100;
        proj.isPenetration = false;//관통 X
    }
    /// <summary>
    /// 던지기 공격
    /// </summary>
    public IEnumerator ThrowAttack()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject projecTile = objFulling.MakeObj(2);
            projecTile.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
            projecTile.GetComponent<Dagger>().hitNum = i + 2;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 휘두르기 공격
    /// </summary>
    public IEnumerator SwingAttackX()
    {
        GameObject swardObj = objFulling.MakeObj(1);
        Sward sward = swardObj.GetComponent<Sward>();
        sward.targetCount = 0;
        sward.maxTarget = 6;
        sward.skillDamage = 260;
        swardObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        swardObj.transform.rotation = transform.rotation;//캐릭터가 바라보는 위치로 회전

        float angleRotate = 15;
        float restRotate = 165;
        while (restRotate>0)
        {
            swardObj.transform.Rotate(0, angleRotate, 0);
            restRotate -= angleRotate;
            yield return new WaitForSeconds(0.05f);
        }
        swardObj.gameObject.SetActive(false);
    }
    /// <summary>
    /// 휘두르기 공격
    /// </summary>
    public IEnumerator SwingAttackY()
    {
        GameObject swardObj = objFulling.MakeObj(1);
        Sward sward = swardObj.GetComponent<Sward>();
        sward.targetCount = 0;
        sward.maxTarget = 3;
        sward.skillDamage = 320;
        swardObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        Quaternion baseRotation = transform.rotation;//캐릭터가 바라보는 위치로 회전

        float currentAngle = -105f; // 시작 각도
        float targetAngle = 30f;    // 끝 각도 (-105 + 135 = 30)
        float angleRotate = 10;
       
        while (currentAngle < targetAngle)
        {
            // 2. 매 프레임 [베이스 방향]에 [X축 각도]와 [Z축 90도(칼 세우기)]를 결합한 절대 회전값을 주입합니다.
            // Quaternion.Euler(X, Y, Z) 순서로 조합되므로 Y축 간섭을 막아줍니다.
            swardObj.transform.rotation = baseRotation * Quaternion.Euler(currentAngle, 0,-90);

            currentAngle += angleRotate;
            yield return new WaitForSeconds(0.03f);
        }
        swardObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 찌르기 공격
    /// </summary>
    public IEnumerator PierceAttack()
    {
        GameObject swardObj = objFulling.MakeObj(6);
        Sward sward = swardObj.GetComponent<Sward>();
        sward.targetCount = 0;
        sward.maxTarget = 1;
        sward.skillDamage = 170;

        Vector3 moveVec = (playerDirObjectTransform.transform.position - this.gameObject.transform.position).normalized;
        moveVec.y = 0f;

        //초기 위치
        swardObj.transform.position = transform.position-moveVec;//캐릭터 위치에서 날리기 시작
        swardObj.transform.rotation = transform.rotation;//캐릭터가 바라보는 위치로 회전

        float angleRotate = 30;
        float restRotate = 90;
        while (restRotate > 0)
        {
            swardObj.transform.position += moveVec;
            restRotate -= angleRotate;
            yield return new WaitForSeconds(0.04f);
        }
        swardObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 메테오
    /// </summary>
    /// <returns></returns>
    public IEnumerator Meteo()
    {
        //공격 대상 몬스터
        List<GameObject> targets = PlayerAttackCommon.TargetMonstersInRange(this.gameObject.transform.position,8,2,10);
    
        //원 위치는 몬스터 머리 위
        List<GameObject> circleList = new List<GameObject>();
        foreach(GameObject target in targets)
        {
            GameObject crystalCircleObj = objFulling.MakeObj(4);
            BoxCollider collider = target.GetComponent<BoxCollider>();
            crystalCircleObj.transform.position = target.transform.position+3*collider.size.y*Vector3.up;
            circleList.Add(crystalCircleObj);
        }
        yield return new WaitForSeconds(0.8f);
        //크리스탈 떨어드리기
        //크리스탈은 원 위치에 놓기
        for(int hit = 2; hit < 6; hit++)
        {
            foreach (GameObject circle in circleList)
            {
                GameObject crystalCircleObj = objFulling.MakeObj(3);
                crystalCircleObj.GetComponent<Crystal>().hitNum = hit;
                crystalCircleObj.transform.position = circle.transform.position;
                circle.SetActive(false);
            }
            yield return new WaitForSeconds(0.2f);
        }
      
        circleList.Clear();
        targets.Clear();
    }

    /// <summary>
    /// 관통기
    /// </summary>
    /// <returns></returns>
    public IEnumerator Penetration()
    {
        GameObject penetrateEffect = Instantiate(fireDemonEffect);
        penetrateEffect.transform.position = playerDirObjectTransform.position;
        penetrateEffect.transform.rotation = transform.rotation;

        yield return new WaitForSeconds(0.5f);
        GameObject projecTile = objFulling.MakeObj(5);
        projecTile.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        Projectile proj = projecTile.GetComponent<Projectile>();
        proj.hitNum = 2;
        proj.skillDamage = 300;
        proj.isPenetration = true;
        proj.maxTarget = 6;

        yield return new WaitForSeconds(0.25f);
        Destroy(penetrateEffect);
    }
    /// <summary>
    /// 폭탄 공격
    /// </summary>
    /// <returns></returns>
    public IEnumerator BoomAttack()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject boomObj = objFulling.MakeObj(7);
        boomObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
    }
    /// <summary>
    /// 설치기 공격
    /// </summary>
    /// <returns></returns>
    public IEnumerator InstallAttack()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject installObj = objFulling.MakeObj(8);
        installObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에 설치
    }
    /// <summary>
    /// 밀격
    /// </summary>
    /// <returns></returns>
    public IEnumerator PushAttack()
    {
        Vector3 lookDir = playerDirObjectTransform.position-transform.position;
        List<GameObject> mobList = PlayerAttackCommon.TargetMonstersFromPlayer(lookDir,gameObject.transform.position,3,2,10);
        
        //이펙트
        GameObject push = Instantiate(pushEffect);
        pushEffect.transform.position = playerDirObjectTransform.position;
        pushEffect.transform.LookAt(-lookDir);

        foreach (GameObject mobObj in mobList)
        {
            Rigidbody rb = mobObj.GetComponent<Rigidbody>();
            Collider collider = mobObj.GetComponent<Collider>();
            Vector3 forcePower = 3 * lookDir.normalized;
            forcePower.y = 0;
            rb.AddForce(forcePower,ForceMode.Impulse);
            PlayerAttackCommon.PlayerToMonsterAttack(collider,100,2);
        }
        yield return new WaitForSeconds(2f);
        Destroy(push);
    }
    /// <summary>
    /// 칼날 폭풍
    /// </summary>
    /// <returns></returns>
    public IEnumerator KnifeStorm()
    {
        //칼날 오브젝트 생성
        GameObject storm = Instantiate(knifeStormObj);
        storm.transform.position = transform.position;

        //중심 오브젝트 설정
        ObjectRotation rot = storm.GetComponent<ObjectRotation>();
        rot.centerObj = this.gameObject;

        //칼날 옵션 세팅
        for(int idx = 0; idx < 3; idx++)
        {
            Sward sward = storm.transform.GetChild(idx).GetComponent<Sward>();
            sward.skillDamage = 80;
            sward.maxTarget = 10000;
        }

        //지속 시간 이후 파괴
        yield return new WaitForSeconds(15f);
        Destroy(storm);
    }

    /// <summary>
    /// 다단 히트 어택
    /// </summary>
    /// <returns></returns>
    public IEnumerator MultiHitAttack(int hit)
    {
        //공격 대상 몬스터
        Vector3 lookDir = playerDirObjectTransform.position - transform.position;
        GameObject target = PlayerAttackCommon.NearMonserFromPlayer(lookDir, transform.position,3);

        //타겟이 있는가?
        if (target != null)
        {
            //N회 공격
            Collider tarGetCol = target.GetComponent<Collider>();
            for (int idx = 0; idx < hit; idx++)
            {
                PlayerAttackCommon.PlayerToMonsterAttack(tarGetCol, 100, idx + 2);
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
