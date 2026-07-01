using UnityEngine;

public class Sward : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            //공격 데미지 입히기
            PlayerAttackCommon.PlayerToMonsterAttack(other);
        }
    }
}
