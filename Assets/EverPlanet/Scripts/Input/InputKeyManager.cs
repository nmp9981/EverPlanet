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
        if (Input.GetKey(KeyCode.LeftControl))//폭풍의 시
        {
            if (currentTime > 0.1f)
            {
                currentTime = 0;
                playerAttack.GeneralAttack();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))//휘두르기X
        {
            StartCoroutine(playerAttack.SwingAttackX());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))//휘두르기Y
        {
            StartCoroutine(playerAttack.SwingAttackY());
        }
        if (Input.GetKeyDown(KeyCode.X))//트리플스로우
        {
            StartCoroutine(playerAttack.ThrowAttack());
        }
        if (Input.GetKeyDown(KeyCode.C))//관통기
        {
            StartCoroutine(playerAttack.Penetration());
        }
        if (Input.GetKeyDown(KeyCode.V))//광역기, 범위기
        {
            StartCoroutine(playerAttack.Meteo());
        }
        if (Input.GetKeyDown(KeyCode.B))//찌르기
        {
            StartCoroutine(playerAttack.PierceAttack());
        }
        if (Input.GetKeyDown(KeyCode.N))//폭탄 공격
        {
            StartCoroutine(playerAttack.BoomAttack());
        }
        if (Input.GetKeyDown(KeyCode.M))//설치기
        {
            StartCoroutine(playerAttack.InstallAttack());
        }
        if (Input.GetKeyDown(KeyCode.F))//밀격
        {
            StartCoroutine(playerAttack.PushAttack());
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
