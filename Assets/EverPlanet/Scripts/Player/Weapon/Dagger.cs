using UnityEngine;

public class Dagger : MonoBehaviour
{
    public int hitNum = 2;
    public Vector3 moveVec;
    GameObject player;
    GameObject target;

    float timer = 0;
    float destroyTimer = 10;

    void Awake()
    {
        player = GameObject.Find("Player");
        target = GameObject.Find("DragTarget");
    }
    private void OnEnable()
    {
        moveVec = (target.transform.position - player.transform.position).normalized;
        moveVec.y = 0f;

        gameObject.transform.rotation = Quaternion.Euler(0, DotAngle(), 0);

        timer = 0;
    }
    void Update()
    {
        DragMove();
    }
    void DragMove()
    {
        gameObject.transform.position += moveVec * 5f * Time.deltaTime;
        timer += Time.deltaTime;

        //생성 후 10초뒤 파괴
        if (timer > destroyTimer) gameObject.SetActive(false);
    }
    //표창 y축 회전 정도
    float DotAngle()
    {
        float dot = -moveVec.x;
        float cosTheta = dot / moveVec.magnitude;
        float theta = -Mathf.Acos(cosTheta) * 180 / Mathf.PI;
        return theta;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            //공격 데미지 입히기
            PlayerAttackCommon.PlayerToMonsterAttack(other, 150, hitNum);

            //투사체 삭제
            gameObject.SetActive(false);
        }
    }
}
