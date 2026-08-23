using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    //주요 UI
    [Header("주요 UI")]
    [SerializeField] TextMeshProUGUI playerJobText;
    [SerializeField] TextMeshProUGUI playerLvText;
    [SerializeField] TextMeshProUGUI playerHPText;
    [SerializeField] TextMeshProUGUI playerMPText;
    [SerializeField] TextMeshProUGUI playerEXPText;

    [Header("AP UI")]
    [SerializeField] TextMeshProUGUI playerSTRText;
    [SerializeField] TextMeshProUGUI playerDEXText;
    [SerializeField] TextMeshProUGUI playerINTText;
    [SerializeField] TextMeshProUGUI playerLUKText;

    //상세 UI
    [Header("상세 UI")]
    [SerializeField] TextMeshProUGUI playerAttackText;
    [SerializeField] TextMeshProUGUI playerPhyDEFText;
    [SerializeField] TextMeshProUGUI playerMagicText;
    [SerializeField] TextMeshProUGUI playerMagicDEFext;
    [SerializeField] TextMeshProUGUI playerAccText;
    [SerializeField] TextMeshProUGUI playerAvoidText;
    [SerializeField] TextMeshProUGUI playerHandText;
    [SerializeField] TextMeshProUGUI playerMoveText;
    [SerializeField] TextMeshProUGUI playerJumpText;

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
            playerJobText.text = $"{PlayerInfo.playerJobString}";
            playerLvText.text = $"{PlayerInfo.playerLv}";
            playerHPText.text = $"{PlayerInfo.curHP} / {PlayerInfo.maxHP}";
            playerMPText.text = $"{PlayerInfo.curMP} / {PlayerInfo.maxMP}";

            int expRate = (int)((PlayerInfo.curExp *100)/ PlayerInfo.maxExp);
            playerEXPText.text = $"{PlayerInfo.curExp} ({expRate}%)";

            playerSTRText.text = $"{PlayerInfo.playerSTR}";
            playerDEXText.text = $"{PlayerInfo.playerDEX}";
            playerINTText.text = $"{PlayerInfo.playerINT}";
            playerLUKText.text = $"{PlayerInfo.playerLUK}";

            playerAttackText.text = $"{PlayerInfo.attackPower}";
            playerPhyDEFText.text = $"{PlayerInfo.phyDEF}";
            playerMagicText.text = $"{PlayerInfo.magicPower}";
            playerMagicDEFext.text = $"{PlayerInfo.magicDEF}";
            playerAccText.text = $"{PlayerInfo.accuracy}";
            playerAvoidText.text = $"{PlayerInfo.avoidance}";
            playerHandText.text = $"{PlayerInfo.workmanship}";
            playerMoveText.text = $"{PlayerInfo.moveSpeed}";
            playerJumpText.text = $"{PlayerInfo.jumpPower}";
        }
    }
}
