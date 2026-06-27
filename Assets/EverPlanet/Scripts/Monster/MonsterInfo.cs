using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public string mobName;
    public int mobLv;
    public float mobMaxHP;
    public float mobCurrentHP;
    public int mobExp;

    public int mobRequireAcc;//요구 명중률
    public int mobAttack;//몬스터 공격력
    public int mobDefence;//몬스터 방어력

    private void OnEnable()
    {
        mobCurrentHP = mobMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
