using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] bool isJumping;

    /// <summary>
    /// 위치 세팅
    /// </summary>
    /// <param name="xAmount"></param>
    /// <param name="zamount"></param>
    public void SetingPlayerPos(float xAmount, float zAmount)
    {
        Vector3 dir = new Vector3(xAmount, 0, zAmount);
        transform.position += dir.normalized * 4 * Time.deltaTime;
    }

    /// <summary>
    /// 플레이어 점프
    /// </summary>
    public void JumpPlayer(bool isJumpKey)
    {
        if (!isJumpKey) return;

        if (isJumping == false)//점프상태가 아니라면
        {
            isJumping = true;//점프중으로 바꾼다.
            rigid.AddForce(new Vector2(0, 7), ForceMode.Impulse);//y축 방향으로 힘을 실어준다.(점프)
        }

    }
    /// <summary>
    /// 충돌 처리
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Land")//땅에 닿으면
        {
            isJumping = false;//다시 점프를 할 수 있다.
        }
    }
}
