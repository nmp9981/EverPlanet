using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Cloud cloudEffect;

    GameObject player;
    GameObject target;
    public Vector3 moveVec;

    void Awake()
    {
        player = GameObject.Find("Player");
        target = GameObject.Find("DragTarget");
    }
    private void OnEnable()
    {
        moveVec = (target.transform.position+Vector3.up - player.transform.position).normalized;
        rigid.AddForce(5*moveVec,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //몬스터
        if (collision.gameObject.tag.Contains("Land"))
        {
            //연기 생성
            GameObject cloudEff = Instantiate(cloudEffect.gameObject);
            cloudEff.transform.position = this.transform.position;

            //투사체 삭제
            gameObject.SetActive(false);
        }
    }
}
