using Unity.VisualScripting;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public string mobName;
    public int mobLv;
    public float mobMaxHP = 50000;
    public float mobCurrentHP;
    public int mobExp;

    public int mobRequireAcc;//요구 명중률
    public int mobAttack;//몬스터 공격력
    public int mobDefence;//몬스터 방어력

    [SerializeField]
    public bool isAggro =false;//어그로 여부

    protected string MobName => mobName;
    protected int MobLv => mobLv;

    //보스 여부
    public bool isBoss;

    //UI
    [SerializeField] SpriteRenderer hpBarImage;
    [SerializeField] PlayerInfoUpdate playerInfo;

    /// <summary>
    /// HP바 세팅
    /// </summary>
    protected void InitHP_UISet()
    {
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfoUpdate>();
        mobCurrentHP = mobMaxHP;
        if (hpBarImage != null) hpBarImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }


    /// <summary>
    /// 몬스터 HP 감소
    /// </summary>
    /// <param name="damage"></param>
    public void DecreaseMonsterHP(float damage)
    {
        mobCurrentHP = Mathf.Max(0, mobCurrentHP - damage);
        float rate = mobCurrentHP / mobMaxHP;
        if (hpBarImage != null) hpBarImage.gameObject.transform.localScale = new Vector3(rate, 1, 1);

        //몬스터 사망
        if (mobCurrentHP <= 0)
        {
            MonsterDie();
        }
    }

    /// <summary>
    /// 몬스터 사망
    /// </summary>
    public void MonsterDie()
    {
        playerInfo.GetExp(mobExp);
        MonsterSpawn.activeMonster.Remove(this.gameObject);
        if(isBoss)
        {
            MapManage.isKillBoss = true;
        }
        this.gameObject.SetActive(false);
    }
}
