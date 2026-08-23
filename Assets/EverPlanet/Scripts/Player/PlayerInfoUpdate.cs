using UnityEngine;

public class PlayerInfoUpdate : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private UIObjectFulling _uiFulling;

    /// <summary>
    /// 경험치 획득
    /// </summary>
    /// <param name="amount"></param>
    public void GetExp(int amount)
    {
        //경험치 획득
        PlayerInfo.curExp += amount;
        _playerUI.UpdatePlayerExpInfo();

        //경험치 UI
        PlayerXPUI playerXPUI = _uiFulling.MakeObj(0).GetComponent<PlayerXPUI>();
        playerXPUI.SetXP(amount);

        if(PlayerInfo.curExp > PlayerInfo.maxExp)
        {
            LevelUP();
        }
    }

    /// <summary>
    /// HP 감소
    /// </summary>
    public void DecreasePlayerHP(int amount)
    {
        PlayerInfo.curHP = Mathf.Max(0, PlayerInfo.curHP - amount);
        _playerUI.UpdatePlayerHpInfo();
    }

    /// <summary>
    /// MP 감소
    /// </summary>
    public void DecreasePlayerMP(int amount)
    {
        PlayerInfo.curMP = Mathf.Max(0, PlayerInfo.curMP - amount);
        _playerUI.UpdatePlayerMpInfo();
    }
    /// <summary>
    /// HP 증가
    /// </summary>
    public void IncreasePlayerHP(int amount)
    {
        PlayerInfo.curHP = Mathf.Min(PlayerInfo.maxHP, PlayerInfo.curHP + amount);
        _playerUI.UpdatePlayerHpInfo();
    }

    /// <summary>
    /// MP 증가
    /// </summary>
    public void IncreasePlayerMP(int amount)
    {
        PlayerInfo.curMP = Mathf.Min(PlayerInfo.maxMP, PlayerInfo.curMP + amount);
        _playerUI.UpdatePlayerMpInfo();
    }

    /// <summary>
    /// 레벨 업
    /// </summary>
    /// <param name="amount"></param>
    public void LevelUP()
    {
        if(PlayerInfo.playerLv< PlayerInfo.playerMaxLv)
        {
            IncreasePlayerasicInfo();
            IncreaseStat();
            PlayerInfo.CalDetailStat();
            
            _playerUI.UpdatePlayerHpInfo();
            _playerUI.UpdatePlayerMpInfo();
            _playerUI.UpdatePlayerLvInfo();
        }
    }

    /// <summary>
    /// 플레이어 기본 정보 증가
    /// </summary>
    void IncreasePlayerasicInfo()
    {
        PlayerInfo.playerLv = PlayerInfo.playerLv + 1;
        PlayerInfo.curExp = Mathf.Max(0, PlayerInfo.curExp - PlayerInfo.maxExp);
        PlayerInfo.maxExp = (PlayerInfo.playerLv == PlayerInfo.playerMaxLv) ? 2100000000 : (PlayerInfo.maxExp * 104) / 100;
        PlayerInfo.maxHP = PlayerInfo.maxHP + Random.Range(140, 155);
        PlayerInfo.maxMP = PlayerInfo.maxMP + Random.Range(46, 60);
        PlayerInfo.curHP = PlayerInfo.maxHP;
        PlayerInfo.curMP = PlayerInfo.maxMP;
    }

    /// <summary>
    /// 스탯 증가
    /// </summary>
    void IncreaseStat()
    {
        PlayerInfo.playerSTR += 5;
        PlayerInfo.playerDEX += 2;
        PlayerInfo.playerINT += 5;
        PlayerInfo.playerLUK += 3;
    }
}
