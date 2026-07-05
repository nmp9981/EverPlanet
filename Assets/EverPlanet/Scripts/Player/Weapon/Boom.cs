using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;

    GameObject player;
    GameObject target;
    public Vector3 moveVec;

    int targetCount = 0;
    float timer = 0;
    float destroyTimer = 9;

    void Awake()
    {
        player = GameObject.Find("Player");
        target = GameObject.Find("DragTarget");
    }
    private void OnEnable()
    {
        moveVec = (target.transform.position+Vector3.up - player.transform.position).normalized;
        rigid.AddForce(5*moveVec,ForceMode.Impulse);
        timer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //몬스터
        if (other.gameObject.tag.Contains("Monster"))
        {
            //공격 데미지 입히기
            PlayerAttackCommon.PlayerToMonsterAttack(other, 800, 2);

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
