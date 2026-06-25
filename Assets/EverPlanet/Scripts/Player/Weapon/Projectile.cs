using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 moveVec;
    GameObject player;
    GameObject target;

    void Awake()
    {
        player = GameObject.Find("Player");
        target = GameObject.Find("DragTarget");
    }
    private void OnEnable()
    {
        moveVec = (target.transform.position - player.transform.position).normalized;
        moveVec.y = 0f;

        gameObject.transform.rotation = Quaternion.Euler(0, DotAngle()-90, 0);

    }
    void Update()
    {
        DragMove();
    }
    void DragMove()
    {
        gameObject.transform.position += moveVec*5f * Time.deltaTime;
       
    }
    //표창 y축 회전 정도
    float DotAngle()
    {
        float dot = -moveVec.x;
        float cosTheta = dot / moveVec.magnitude;
        float theta = -Mathf.Acos(cosTheta) * 180 / Mathf.PI;
        return theta;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag.Contains("Monster"))
        {
            int damage = Random.Range(77,308);
            Debug.Log(damage);
            PlayerAttackCommon.ShowDamageAsSkin(damage,collision.gameObject,1);

            gameObject.SetActive(false);
        }
    }
}
