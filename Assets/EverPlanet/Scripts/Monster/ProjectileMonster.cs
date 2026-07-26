using UnityEngine;

public class ProjectileMonster : MonoBehaviour
{
    //마법 데미지
    private int magicDamage;
    
    /// <summary>
    /// 마공 세팅
    /// </summary>
    /// <param name="magic"></param>
    public void SetMagicDamage(int mobAttack)
    {
        magicDamage = Random.Range(mobAttack * 90 / 100, mobAttack * 110 / 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//플레이어 피격
        {
            other.GetComponent<PlayerInfoUpdate>().DecreasePlayerHP(magicDamage);
            PlayerAttackCommon.ShowDamageAsSkin(magicDamage, other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Land"))
        {
            Destroy(this.gameObject);
        }
    }
}
