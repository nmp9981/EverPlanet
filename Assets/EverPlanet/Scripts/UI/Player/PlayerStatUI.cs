using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    
    /// <summary>
    /// 플레이어 스탯 정보 업데이트
    /// </summary>
    public void UpdatePlayerStat()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
