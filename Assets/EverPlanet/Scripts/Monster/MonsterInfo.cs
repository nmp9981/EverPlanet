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

    MonsterMove monsterMove;

    private void OnEnable()
    {
        mobCurrentHP = mobMaxHP;
        monsterMove = GetComponent<MonsterMove>();
        monsterMove.HPBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 몬스터 HP 감소
    /// </summary>
    /// <param name="damage"></param>
    public void DecreaseMonsterHP(float damage)
    {
        mobCurrentHP = Mathf.Max(0, mobCurrentHP - damage);
        float rate = mobCurrentHP / mobMaxHP;
        monsterMove.HPBar.fillAmount = rate;

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
        this.gameObject.SetActive(false);
    }
}
