using UnityEngine;

public class ProjectileMonster : MonoBehaviour
{
    //마법 데미지
    private int magicDamage;

    //무적시간
    private float spawnTime;

    private void Awake()
    {
        spawnTime = Time.time;
    }

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
            PlayerAttackCommon.ShowDamageAsHitSkin(magicDamage, other.gameObject, InputKeyManager.orderHitNum);
            InputKeyManager.orderHitNum += 1;
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Land"))
        {
            if(Time.time -  spawnTime > 0.5f)//생성후 무적시간
            {
                Destroy(this.gameObject);
            }
        }
        //충돌시 이펙트 off

    }
}
