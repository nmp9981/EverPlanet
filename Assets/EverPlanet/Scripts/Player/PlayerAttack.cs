using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] ObjectFulling objFulling;
    [SerializeField] Transform playerDirObjectTransform;

    [Header("이펙트")]
    [SerializeField] GameObject fireDemonEffect;
    [SerializeField] GameObject cloudEffect;

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
    public IEnumerator SwingAttack()
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
        GameObject boomObj = objFulling.MakeObj(7);
        boomObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        Boom bomb = boomObj.GetComponent<Boom>();
        yield return new WaitForSeconds(0.25f);
    }
}
