using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    //데미지 UI순서
    public static int orderSortNum { get; set; }


    //플레이어 조작
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerAttack playerAttack;

    // Update is called once per frame
    void Update()
    {
        InputMove();
        InputAttack();
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerAttack.GeneralAttack();
        }
    }
}
