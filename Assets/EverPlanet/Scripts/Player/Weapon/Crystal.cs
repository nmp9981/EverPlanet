using UnityEngine;

public class Crystal : MonoBehaviour
{
    public int hitNum;
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
        //몬스터
        if(other.gameObject.tag.Contains("Monster"))
        {
            //공격 데미지 입히기
            PlayerAttackCommon.PlayerToMonsterAttack(other, 180, hitNum);

            //투사체 삭제
            gameObject.SetActive(false);
        }
        //땅
        if (other.gameObject.tag.Contains("Land"))
        {
            //투사체 삭제
            gameObject.SetActive(false);
        }
    }
}
