using UnityEngine;

public class Sward : MonoBehaviour
{
    public int targetCount;
    public int maxTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            //타겟수 이하
            if (targetCount < maxTarget)
            {
                //공격 데미지 입히기
                PlayerAttackCommon.PlayerToMonsterAttack(other, 260, 1);

                targetCount++;
            }
        }
    }
}
