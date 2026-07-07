using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Cloud cloudEffect;

    GameObject player;
    GameObject target;
    public Vector3 moveVec;
    bool isCollide = false;//충돌 플래그, 물리 충돌 미세 진동 버그

    void Awake()
    {
        player = GameObject.Find("Player");
        target = GameObject.Find("DragTarget");
    }
    private void OnEnable()
    {
        isCollide = false;
        moveVec = (target.transform.position+Vector3.up - player.transform.position).normalized;
        rigid.AddForce(5*moveVec,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {       
        //몬스터
        if (collision.gameObject.tag.Contains("Land") && !isCollide)
        {
            isCollide = true;
            //연기 생성
            GameObject cloudEff = Instantiate(cloudEffect.gameObject);
            cloudEff.transform.position = this.transform.position;

            //투사체 삭제
            gameObject.SetActive(false);
        }
    }
}
