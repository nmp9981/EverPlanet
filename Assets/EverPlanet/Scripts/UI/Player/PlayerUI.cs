using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Image mpBar;
    [SerializeField] TextMeshProUGUI mpText;

    [SerializeField] Image expBar;
    [SerializeField] TextMeshProUGUI expText;

    [SerializeField] TextMeshProUGUI lvText;

    private void Start()
    {
        UpdatePlayerHpInfo();
        UpdatePlayerMpInfo();
        UpdatePlayerExpInfo();
        UpdatePlayerLvInfo();
    }

    /// <summary>
    /// 플레이어 경험치 정보 업데이트
    /// </summary>
    public void UpdatePlayerExpInfo()
    {
        expBar.fillAmount = (float)PlayerInfo.curExp / PlayerInfo.maxExp;
        expText.text = $"{PlayerInfo.curExp} / {PlayerInfo.maxExp} [{expBar.fillAmount*100f:F1}%]";
    }
    /// <summary>
    /// 플레이어 HP 정보 업데이트
    /// </summary>
    public void UpdatePlayerHpInfo()
    {
        hpBar.fillAmount = (float)PlayerInfo.curHP / PlayerInfo.maxHP;
        hpText.text = $"{PlayerInfo.curHP} / {PlayerInfo.maxHP}";
    }
    /// <summary>
    /// 플레이어 MP 정보 업데이트
    /// </summary>
    public void UpdatePlayerMpInfo()
    {
        mpBar.fillAmount = (float)PlayerInfo.curMP / PlayerInfo.maxMP;
        mpText.text = $"{PlayerInfo.curMP} / {PlayerInfo.maxMP}";
    }
    /// <summary>
    /// 플레이어 Lv 정보 업데이트
    /// </summary>
    public void UpdatePlayerLvInfo()
    {
        UpdatePlayerExpInfo();
        lvText.text = $"{PlayerInfo.playerLv}";
    }
}
