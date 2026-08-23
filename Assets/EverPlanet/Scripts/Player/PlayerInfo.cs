using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //이동 관련
    public float playerSpeed = 6;
    public float playerJumpPower = 8;
    public int maxJumpCount;
    protected float playerRotateSpeed = 15;

    //공격 관련
    public static float attackPower = 150;//공격력
    public static float workmanship = 30;//숙련도
    public static float criticalRate = 50;//크리티컬 확률
    public static int criticalDamage = 200;//크리티컬 데미지

    //레벨 관련
    public static int playerLv;//플레이어 레벨
    public static int playerMaxLv=100;//플레이어 최대 레벨
    public static string playerJobString;//직업명

    //HP,MP 관련
    public static int curHP = 0;
    public static int maxHP = 8000;
    public static int curMP = 0;
    public static int maxMP = 2000;

    //경험치 관련
    public static int curExp = 0;//현재 경험치
    public static int maxExp = 1200;//최대 경험치

    //AP 관련
    public static int playerSTR;
    public static int playerDEX;
    public static int playerINT;
    public static int playerLUK;

    //상세 스탯
    public static int phyDEF = 1;//물리방어력
    public static int magicPower = 100;//마법공격력
    public static int magicDEF = 100;//마법방어력
    public static int accuracy = 100;//명중률
    public static int avoidance = 100;//회피율
    public static int moveSpeed = 100;//이동속도
    public static int jumpPower = 100;//점프력


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetInitInfo();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 초기 정보 설정
    /// </summary>
    void SetInitInfo()
    {
        curExp = 0;
        curHP = maxHP;
        curMP = maxMP;
        playerLv = 10;

        playerSTR = 25;
        playerDEX = 20;
        playerINT = 20;
        playerLUK = 5;

        CalDetailStat();
    }

    /// <summary>
    /// 상세 스탯 계산
    /// </summary>
    public static void CalDetailStat()
    {
        if (playerLv < 30)
        {
            playerJobString = "Warrior";
        }
        else if (playerLv >= 30 && playerLv < 70)
        {
            playerJobString = "Assassin";
        }
        else if (playerLv >= 70 && playerLv < 100)
        {
            playerJobString = "Knight";
        }else playerJobString = "Grand Master";

        attackPower = playerSTR*8+playerDEX*2;//공격력
        workmanship = workmanship + 5 * (playerLv / 10);//숙련도

        phyDEF = playerSTR * 4;
        magicPower = playerINT;
        magicDEF = playerINT * 3;
        accuracy = (playerDEX * 8 + playerLUK * 5) / 10;
        avoidance = playerLUK*3;
        moveSpeed = 100 + playerDEX /9;
        jumpPower = 100 + playerDEX /10;
    }
}
