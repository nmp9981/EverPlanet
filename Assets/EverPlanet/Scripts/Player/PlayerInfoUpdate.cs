using UnityEngine;

public class PlayerInfoUpdate : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private UIObjectFulling _uiFulling;

    /// <summary>
    /// °æÇèÄ¡ È¹µæ
    /// </summary>
    /// <param name="amount"></param>
    public void GetExp(int amount)
    {
        //°æÇèÄ¡ È¹µæ
        PlayerInfo.curExp += amount;
        _playerUI.UpdatePlayerInfo();

        //°æÇèÄ¡ UI
        PlayerXPUI playerXPUI = _uiFulling.MakeObj(0).GetComponent<PlayerXPUI>();
        playerXPUI.SetXP(amount);

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
