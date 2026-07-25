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
        _playerUI.UpdatePlayerExpInfo();

        //°æÇèÄ¡ UI
        PlayerXPUI playerXPUI = _uiFulling.MakeObj(0).GetComponent<PlayerXPUI>();
        playerXPUI.SetXP(amount);

        if(PlayerInfo.curExp > PlayerInfo.maxExp)
        {
            LevelUP();
        }
    }

    /// <summary>
    /// HP °¨¼Ò
    /// </summary>
    public void DecreasePlayerHP(int amount)
    {
        PlayerInfo.curHP = Mathf.Max(0, PlayerInfo.curHP - amount);
        _playerUI.UpdatePlayerHpInfo();
    }

    /// <summary>
    /// ·¹º§ ¾÷
    /// </summary>
    /// <param name="amount"></param>
    public static void LevelUP()
    {

    }
}
