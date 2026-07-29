using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    //데미지 UI순서
    public static int orderSortNum { get; set; }
    public static int orderHitNum { get; set; }
    public const int maxOrderSortNum = 99999999;

    //플레이어 조작
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] PlayerInfoUpdate playerInfoUpdate;

    //타이머
    float currentTime = 0;

    //데미지 시간 
    float curDamageTime = 0;

    // Update is called once per frame
    void Update()
    {
        InputMove();
        InputAttack();

        TimeFlow();

        HitDamageInit();
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
                playerInfoUpdate.DecreasePlayerMP(0);
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
            playerInfoUpdate.DecreasePlayerMP(17);
            StartCoroutine(playerAttack.ThrowAttack());
        }
        if (Input.GetKeyDown(KeyCode.C))//관통기
        {
            playerInfoUpdate.DecreasePlayerMP(30);
            StartCoroutine(playerAttack.Penetration());
        }
        if (Input.GetKeyDown(KeyCode.V))//광역기, 범위기
        {
            playerInfoUpdate.DecreasePlayerMP(80);
            StartCoroutine(playerAttack.Meteo());
        }
        if (Input.GetKeyDown(KeyCode.B))//찌르기
        {
            StartCoroutine(playerAttack.PierceAttack());
        }
        if (Input.GetKeyDown(KeyCode.N))//폭탄 공격
        {
            playerInfoUpdate.DecreasePlayerMP(50);
            StartCoroutine(playerAttack.BoomAttack());
        }
        if (Input.GetKeyDown(KeyCode.M))//설치기
        {
            playerInfoUpdate.DecreasePlayerMP(180);
            StartCoroutine(playerAttack.InstallAttack());
        }
        if (Input.GetKeyDown(KeyCode.F))//밀격
        {
            playerInfoUpdate.DecreasePlayerMP(15);
            StartCoroutine(playerAttack.PushAttack());
        }
        if (Input.GetKeyDown(KeyCode.G))//칼날 폭풍
        {
            playerInfoUpdate.DecreasePlayerMP(55);
            StartCoroutine(playerAttack.KnifeStorm());
        }
        if (Input.GetKeyDown(KeyCode.H))//다단 히트
        {
            playerInfoUpdate.DecreasePlayerMP(27);
            StartCoroutine(playerAttack.MultiHitAttack(8));
        }
    }

    /// <summary>
    /// 시간 흐름
    /// </summary>
    void TimeFlow()
    {
        currentTime += Time.deltaTime;
        curDamageTime += Time.deltaTime;
    }
    /// <summary>
    /// 피격 데미지 위치 초기화
    /// </summary>
    void HitDamageInit()
    {
        if (curDamageTime > 0.5f)
        {
            orderHitNum = 0;
            curDamageTime = 0;
        }
    }
}
