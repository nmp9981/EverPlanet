using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //이동 관련
    public float playerSpeed = 4;
    public float playerJumpPower = 7;
    public int maxJumpCount;
    protected float playerRotateSpeed = 15;

    //공격 관련
    public static float attackPower = 2500;//공격력
    public static float workmanship = 30;//숙련도
    public static float criticalRate = 50;//크리티컬 확률
    public static int criticalDamage = 200;//크리티컬 데미지
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
