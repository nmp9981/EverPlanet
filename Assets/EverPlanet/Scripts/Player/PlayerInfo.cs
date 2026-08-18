using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //이동 관련
    public float playerSpeed = 5;
    public float playerJumpPower = 8;
    public int maxJumpCount;
    protected float playerRotateSpeed = 15;

    //공격 관련
    public static float attackPower = 2500;//공격력
    public static float workmanship = 30;//숙련도
    public static float criticalRate = 50;//크리티컬 확률
    public static int criticalDamage = 200;//크리티컬 데미지

    //레벨 관련
    public static int playerLv;//플레이어 레벨
    public static int playerMaxLv=100;//플레이어 최대 레벨

    //HP,MP 관련
    public static int curHP = 0;
    public static int maxHP = 18227;
    public static int curMP = 0;
    public static int maxMP = 7713;

    //경험치 관련
    public static int curExp = 0;//현재 경험치
    public static int maxExp = 100000;//최대 경험치

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
    }
}
