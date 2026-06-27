using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    //데미지 UI순서
    public static int orderSortNum { get; set; }
    public const int maxOrderSortNum = 99999999;

    //플레이어 조작
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerAttack playerAttack;

    //타이머
    float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        InputMove();
        InputAttack();

        TimeFlow();
    }

    /// <summary>
    /// 움직임 제어
    /// </summary>
    void InputMove()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        bool yAxis = Input.GetKeyDown(KeyCode.LeftAlt);

        playerMove.SetingPlayerPos(hAxis, vAxis);
        playerMove.JumpPlayer(yAxis);
    }

    /// <summary>
    /// 공격 제어
    /// </summary>
    void InputAttack()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (currentTime > 0.1f)
            {
                currentTime = 0;
                playerAttack.GeneralAttack();
            }
        }
        if (Input.GetKey(KeyCode.Z))
        {

        }
    }

    /// <summary>
    /// 시간 흐름
    /// </summary>
    void TimeFlow()
    {
        currentTime += Time.deltaTime;
    }
}
