using UnityEngine;

public class Projectile : MonoBehaviour
{
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

        gameObject.transform.rotation = Quaternion.Euler(0, DotAngle()-90, 0);

        timer = 0;
    }
    void Update()
    {
        DragMove();
    }
    void DragMove()
    {
        gameObject.transform.position += moveVec*5f * Time.deltaTime;
        timer += Time.deltaTime;

        //생성 후 10초뒤 파괴
        if(timer>destroyTimer) gameObject.SetActive(false);
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
        if(collision.gameObject.tag.Contains("Monster"))
        {
            int maxDamage = (int)PlayerInfo.attackPower;
            int minDamage = (int)(PlayerInfo.attackPower*PlayerInfo.workmanship*0.01);
            int damage = Random.Range(minDamage, maxDamage);
            PlayerAttackCommon.ShowDamageAsSkin(damage,collision.gameObject,1);

            gameObject.SetActive(false);
        }
    }
}
