using UnityEngine;

public class NeedleMonster : MonoBehaviour
{
    //마법 데미지
    private int magicDamage;

    //무적시간
    private float spawnTime;

    private void Awake()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime > 1f)//5초뒤 자동 삭제
        {
            Destroy(this.gameObject);
        }

        gameObject.transform.position += 2*Vector3.up*Time.deltaTime;
    }

    /// <summary>
    /// 마공 세팅
    /// </summary>
    /// <param name="magic"></param>
    public void SetMagicDamage(int mobAttack)
    {
        magicDamage = Random.Range(mobAttack, mobAttack * 130 / 100);
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
    }
}
