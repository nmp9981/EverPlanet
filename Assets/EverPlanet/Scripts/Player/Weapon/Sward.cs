using UnityEngine;

public class Sward : MonoBehaviour
{
    public int targetCount;
    public int maxTarget;
    public int skillDamage;
    public bool isSwing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            //타겟수 이하
            if (targetCount < maxTarget)
            {
                //공격 데미지 입히기
                PlayerAttackCommon.PlayerToMonsterAttack(other, skillDamage, 2);

                targetCount++;
            }
        }
    }
}
