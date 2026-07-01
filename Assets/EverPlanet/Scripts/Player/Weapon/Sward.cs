using UnityEngine;

public class Sward : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            //공격 데미지 입히기
            PlayerAttackCommon.PlayerToMonsterAttack(other, 130,1);
        }
    }
}
