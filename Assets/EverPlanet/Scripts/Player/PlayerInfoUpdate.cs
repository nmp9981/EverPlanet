using UnityEngine;

public class PlayerInfoUpdate : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI;

    /// <summary>
    /// °æÇèÄ¡ È¹µæ
    /// </summary>
    /// <param name="amount"></param>
    public void GetExp(int amount)
    {
        PlayerInfo.curExp += amount;
        _playerUI.UpdatePlayerInfo();
        if(PlayerInfo.curExp > PlayerInfo.maxExp)
        {
            LevelUP();
        }
    }
    /// <summary>
    /// ·¹º§ ¾÷
    /// </summary>
    /// <param name="amount"></param>
    public static void LevelUP()
    {

    }
}
